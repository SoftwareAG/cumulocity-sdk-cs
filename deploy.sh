#!/bin/sh
deploy() {

YUM_USR=hudson
YUM_SRV=yum.cumulocity.com
YUM_DEST_DIR=/var/www/resources/cssdk/releases

for package in "$@"
do
    echo "deploy $package to $YUM_SRV:${YUM_DEST_DIR}"
    scp -o StrictHostKeyChecking=no  -Cr $package ${YUM_USR}@${YUM_SRV}:${YUM_DEST_DIR}
 #   dotnet nuget push $package  -k <token>  -s < source https://api.nuget.org/v3/index.json>  
done

}
pwd
deploy publish/*.nupkg
deploy publish/*.zip