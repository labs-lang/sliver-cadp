
              SLiVER 2.0
              October 2021

The SLiVER LAbS VERification tool

    * Package contents *

atlas/, cadp/     Files used by cadp and cadp-monitor backend

examples/         LAbS example specifications

labs/             LAbS parser and translator

HISTORY           SLiVER changelog

README.txt        this file

sliver.py         SLiVER command-line front-end

(Other files)     Libraries used by SLiVER

    * Installation *

To install SLiVER, please follow the steps below:

    1. install Python 3.8 or higher

    2. create a directory, suppose this is called /workspace

    3. extract the entire package contents in /workspace
    
    4. set execution permissions (chmod +x) for sliver.py and labs/LabsTranslate

    * Usage *

To try SLiVER, please use the following command:

    ./sliver.py --backend cadp examples/leader.labs n=3 --property LeaderIs0

which should report that no property is violated.

The following command should instead report that a property is violated:

    ./sliver.py --backend cadp examples/leader.labs n=3 --property LeaderNot0

Please keep in mind that CADP is not part of this package and must be obtained separately

Invoking the tool without options:

    ./sliver.py

will provide further usage directions.

