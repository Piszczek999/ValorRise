@echo off
title AuthenticateServer
cd "Authenticate"
dotnet build
"C:/Users/ognis/AppData/Roaming/Godot/Godot_v4.3.exe" --headless
