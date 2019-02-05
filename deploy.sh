#!/bin/sh
deploy() {

YUM_USR=hudson
YUM_USR_KEY=~/.ssh/id_rsa
YUM_SRV=yum.cumulocity.com
YUM_DEST_DIR=/var/www/resources/cssdk/releases

for var in "$@"
do
    echo "deploy $var to $YUM_SRV:${YUM_DEST_DIR}"
    scp -Cr -i ${YUM_USR_KEY} $var ${YUM_USR}@${YUM_SRV}:${YUM_DEST_DIR}
done

}
pwd
deploy publish/*.nupkg
deploy publish/*.zip