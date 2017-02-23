#!/bin/sh
#------------------------------------------------------------------------------------------
# iLogScanner.sh
#  - This is start-up script to Java application for Linux.
# Copyright(c) Information-technology Promotion Agency, Japan. All rights reserve
#------------------------------------------------------------------------------------------

cd `dirname $0`

BASEDIR=.
CP=$BASEDIR
for name in `ls *.jar`; do
  CP=$CP:$BASEDIR/$name
done

MAIN_CLASS="jp.go.ipa.ilogscanner.ui.StandaloneMain"

java -classpath $CP $MAIN_CLASS $@
exit $?
