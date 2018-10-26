# escape=` (backtick)
FROM microsoft/windowsservercore:ltsc2016

SHELL ["powershell"]



RUN wget 'http://javadl.oracle.com/webapps/download/AutoDL?BundleId=210185' -Outfile 'C:\jreinstaller.exe' 
RUN Start-Process -filepath C:\jreinstaller.exe -passthru -wait -argumentlist "/s,INSTALLDIR=c:\Java\jre1.8.0_91"
RUN del C:\jreinstaller.exe

RUN [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest 'https://github.com/git-for-windows/git/releases/download/v2.12.2.windows.2/MinGit-2.12.2.2-64-bit.zip' -OutFile 'c:\MinGit.zip'
RUN Expand-Archive c:\MinGit.zip -DestinationPath c:\MinGit
RUN del c:\MinGit.zip


RUN [Environment]::SetEnvironmentVariable('PATH', $env:path+';c:\Java\jre1.8.0_91\bin;C:\MinGit\cmd\;C:\MinGit\cmd;', [EnvironmentVariableTarget]::Machine)
 
RUN Import-Module servermanager
RUN Add-WindowsFeature telnet-client

ENTRYPOINT ping -t localhost
