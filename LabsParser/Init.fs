﻿module internal Init

open FParsec
open LabsCore.Grammar
open LabsCore.Tokens
open Common
open Expressions


//let rec pconstexpr:Parser<Expr<unit,unit>> =
//    let errorMsg (var: string) =
//        if var.StartsWith "_" then
//            sprintf "undefined external variable %s" var
//        else
//            sprintf "unexpected variable %s in constant expression" var
//    
//    let toUnitExpr = Expr.map id (fun _ o -> {var=(); offset=o})
//    
//    makeExprParser simpleRef (skipString tID .>> notInIdentifier)
//    >>= fun expr ->
//        let vars = Expr.getVars expr
//        if vars.IsEmpty then
//            Set.map errorMsg vars
//            |> String.concat "\n"
//            |> fail
//        else preturn (toUnitExpr expr)

let pconstexpr:Parser<Expr<unit,unit>> = 
    makeExprParser 
        (fun _ -> fail "unexpected variable in constant expression") 
        (skipString "id" >>. notFollowedBy (skipSatisfy isAlphanum))
        
let pvar loc = 
    pipe3 (followedBy KEYNAME >>. getPosition) KEYNAME (opt (betweenBrackets pconstexpr))
        (fun pos name -> 
            let v = {Vartype=Scalar; Name=name; Location=loc; Init=Undef}
            function
            | Some e -> {Pos=pos; Name=name; Source=""; Def={v with Vartype=Array(e)}}
            | None -> {Pos=pos; Name=name; Source=""; Def=v})

let pinit = 
    let pChoose = 
        (sepbycommas pconstexpr) |> betweenBrackets |>> Choose
    let pRange = 
        followedBy ((ws pconstexpr) >>. RANGE)
        >>. ((ws pconstexpr) .>>. (ws RANGE >>. (ws pconstexpr)) 
        |>> Range)
    let pSingle = (ws pconstexpr) |>> (Choose << List.singleton)
    let UNDEF = stringReturn tUNDEF Undef
    choice [pChoose; pRange; pSingle; UNDEF] |> ws

/// Parses a single init definition.
let pinitdef loc =
    (pvar loc) .>>. ((ws COLON) >>. pinit)
    |>> fun (var, init) -> {var with Def={var.Def with Init=init}}

let pkeys loc = 
    ws (sepbysemis (ws (pinitdef loc)))
//    >>= toSet byName byName 