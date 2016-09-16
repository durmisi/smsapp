cls

if not exist "C:\Deploy" mkdir C:\Deploy
if not exist "C:\Deploy\Mitto.SmsApp.Backend" mkdir C:\Deploy\Mitto.SmsApp.Backend

"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "C:\Projects\smsapp\Mitto.SmsApp.sln"  /m:4 /p:BuildInParallel=true /t:Clean,Build 
"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" "C:\Projects\smsapp\Mitto.SmsApp.Backend.csproj"  /m:4 /p:BuildInParallel=true /p:DeployOnBuild=true /p:PublishProfile=LOCAL


cd "C:\Deploy"
rem del /s /q *.config
del /s /q *.pdb
del /s /q *.pfx
del /s /q *.txt

cls