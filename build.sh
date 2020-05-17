#!/bin/bash
target="kllngii/a-little-square:unity"

cd "${0%/*}"
~/Library/Application\ Support/itch/apps/butler/butler push ./alittlesquare/ ${target}
#Status abfragen
sleep 5
~/Library/Application\ Support/itch/apps/butler/butler status ${target}
#Dauer des Builds ausgeben
echo Dauer des Builds: ${SECONDS}s
