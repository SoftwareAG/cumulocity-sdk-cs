#!/usr/bin/env bash

whoami
pwd

cd Examples/BuildingScripts/
mkdir test

cp -r microservicesdk-win-dev/ test/microservicesdk-win-test/
cp -r microservicesdk-lin-dev/ test/microservicesdk-lin-test/

cd test/microservicesdk-win-test/
chmod +x ./create.ps1
pwsh ./create.ps1 demo api
cd ../../../..

chmod +x ./Examples/BuildingScripts/test/microservicesdk-lin-test/create.sh
cd Examples/BuildingScripts/test/microservicesdk-lin-test/
pwd
./create.sh demo api

# cd ../../..
# pwd
# cd Examples/BuildingScripts/
# chmod +x ./build.sh
# ./build.sh


