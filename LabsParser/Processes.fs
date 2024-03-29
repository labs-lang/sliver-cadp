module internal Processes

open FParsec
open LabsCore.Grammar
open LabsCore.Tokens
open Expressions    

let pexpr = makeExprParser simpleRef (skipString tID .>> notInIdentifier)

/// Parses elementary processes ("actions")
let paction =
    let parseArrow =
        skipChar '<' >>. choice [
            followedBy (skipString "--") >>. stringReturn "--" Location.E; 
            charReturn '-' I;
            charReturn '~' (L("",0))
        ]
    tuple3 
        (ws (simpleRef pexpr) |> sepbycommas)
        (ws parseArrow) 
        (ws pexpr |> sepbycommas)
    >>= (fun (refs, action, exprs) -> 
        try {ActionType=action; Updates=List.zip refs exprs} |> preturn with
        | :? System.ArgumentException -> 
            fail "A multiple assignment should contain the same number of variables and expressions.")


// We need a recursive parser for LAbS processes
// (see pGuarded and pParen)
let pproc, pprocRef = createParserForwardedToRef()

do pprocRef :=
    let doBase (pos, stmt) = {Def=stmt; Pos=pos; Source=""; Name=string stmt} |> BaseProcess
    let compose a b = Comp(a, b)

    let pGuarded = 
        let pguard = makeBExprParser pexpr
        followedBy ((ws pguard) >>. (ws GUARD))
        >>. pipe3
            getPosition (ws pguard) ((ws GUARD) >>. pproc)
            (fun pos g p -> Guard({Pos=pos; Name=""; Source=""; Def=(g,p)}))
        <!> "Guard"
    let pBase =
        let pNil = getPosition .>>. safeStrReturn "Nil" Nil |>> doBase
        let pSkip = getPosition .>>. safeStrReturn tSKIP Skip |>> doBase
        let pParen = followedBy (skipChar '(') >>. (betweenParen pproc)
        
        choice [
             attempt pGuarded <!> "Guarded"
             attempt pNil <!> "Nil"
             attempt pSkip <!> "Skip"
             attempt (getPosition  .>>. (IDENTIFIER |>> Name)) |>> doBase <!> "Name"
             attempt (getPosition .>>. (paction |>> Act)) |>> doBase <!> "Action"
             pParen <!> "Paren"
         ] <!> "BASE"

    let pseq = 
        sepBy1 (ws pBase) (ws SEQ) |>> (compose Seq) <!> "SEQ"
    let pchoice = 
        sepBy (ws pseq) (ws CHOICE) |>> (compose Choice) <!> "CHOICE"

    sepBy (ws pchoice) (ws PAR) |>> (compose Par) <!> "PAR" 

let processes = 
    let pdef =
        pipe3
            ((followedBy IDENTIFIER) >>. getPosition) 
            (ws IDENTIFIER)
            ((ws EQ) >>. (ws pproc) <!> "PPROC")
            (fun pos name proc -> {Name=name; Pos=pos; Source=""; Def=proc})
    ws (many pdef)