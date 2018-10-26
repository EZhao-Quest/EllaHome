# escape=` (backtick)
FROM microsoft/windowsservercore:ltsc2016 as openjdk

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ENV JAVA_VERSION 8u191
ENV JAVA_OJDKBUILD_VERSION 1.8.0.191-1
ENV JAVA_OJDKBUILD_ZIP java-1.8.0-openjdk-1.8.0.191-1.b12.ojdkbuild.windows.x86_64.zip
ENV JAVA_OJDKBUILD_SHA256 d52167198912e239bba19e059870c5742558096ad7b82fe73d51461361b149bd

ENV JAVA_HOME C:\\ojdkbuild
ENV SLAVE_FILENAME=slave.jar `
    REMOTING_VERSION=3.15

ENV SLAVE_HASH_FILENAME=$SLAVE_FILENAME.sha1

RUN $tls12RegBase = 'HKLM:\\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2'; `
	if (Test-Path $tls12RegBase) { throw ('"{0}" already exists!' -f $tls12RegBase) }; `
	New-Item -Path ('{0}/Client' -f $tls12RegBase) -Force; `
	New-Item -Path ('{0}/Server' -f $tls12RegBase) -Force; `
	New-ItemProperty -Path ('{0}/Client' -f $tls12RegBase) -Name 'DisabledByDefault' -PropertyType DWORD -Value 0 -Force; `
	New-ItemProperty -Path ('{0}/Client' -f $tls12RegBase) -Name 'Enabled' -PropertyType DWORD -Value 1 -Force; `
	New-ItemProperty -Path ('{0}/Server' -f $tls12RegBase) -Name 'DisabledByDefault' -PropertyType DWORD -Value 0 -Force; `
	New-ItemProperty -Path ('{0}/Server' -f $tls12RegBase) -Name 'Enabled' -PropertyType DWORD -Value 1 -Force

RUN  [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; `
     Invoke-WebRequest -Uri https://github.com/ojdkbuild/ojdkbuild/releases/download/1.8.0.191-1/java-1.8.0-openjdk-1.8.0.191-1.b12.ojdkbuild.windows.x86_64.zip -OutFile 'ojdkbuild.zip'; `
     Expand-Archive ojdkbuild.zip -DestinationPath C:\; `
     Move-Item  -Path ('C:\{0}' -f ($env:JAVA_OJDKBUILD_ZIP -Replace '.zip$', ''))  -Destination $env:JAVA_HOME; ` 
     Remove-Item ojdkbuild.zip -Force



# Get the Slave from the Jenkins Artifacts Repository
RUN [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest "https://repo.jenkins-ci.org/public/org/jenkins-ci/main/remoting/$env:REMOTING_VERSION/remoting-$env:REMOTING_VERSION.jar" -OutFile $env:SLAVE_FILENAME -UseBasicParsing; `
    Invoke-WebRequest "https://repo.jenkins-ci.org/public/org/jenkins-ci/main/remoting/$env:REMOTING_VERSION/remoting-$env:REMOTING_VERSION.jar.sha1" -OutFile $env:SLAVE_HASH_FILENAME -UseBasicParsing; `
    if ((Get-FileHash $env:SLAVE_FILENAME -Algorithm SHA1).Hash -ne $(Get-Content $env:SLAVE_HASH_FILENAME)) {exit 1};


	
FROM microsoft/windowsservercore:ltsc2016 as git
	
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ENV GIT_VERSION=2.15.1.2 `
    GIT_TAG=v2.15.1.windows.2

ENV GIT_FILENAME=MinGit-$GIT_VERSION-64-bit.zip `
    GIT_HASH_FILENAME=$GIT_FILENAME.sha256 `
    GIT_RELEASE_NOTES_FILENAME=releaseNotes.html
	
# Get Git
RUN [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest "https://github.com/git-for-windows/git/releases/download/$env:GIT_TAG/$env:GIT_FILENAME" -OutFile $env:GIT_FILENAME -UseBasicParsing;`
    Invoke-WebRequest "https://github.com/git-for-windows/git/releases/tag/$env:GIT_TAG" -OutFile $env:GIT_RELEASE_NOTES_FILENAME -UseBasicParsing; `
    Select-String $env:GIT_RELEASE_NOTES_FILENAME -Pattern "\"<td>$env:GIT_FILENAME</td>\"" -Context 1 `
    | Select-Object -ExpandProperty Context `
    | Select-Object -ExpandProperty DisplayPostContext `
    | Select-String -Pattern '[a-f0-9]{64}' `
    | % { $_.Matches } `
    | % { $_.Value } `
    > $env:GIT_HASH_FILENAME; `
    if ((Get-FileHash $env:GIT_FILENAME -Algorithm SHA256).Hash -ne $(Get-Content $env:GIT_HASH_FILENAME)) {exit 1};

RUN Expand-Archive $env:GIT_FILENAME .\git;



FROM microsoft/windowsservercore:ltsc2016
	
SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

ENV JAVA_HOME="c:\Program Files\openjdk" `
    JENKINS_HOME="c:\Program Files\jenkins" `
    GIT_HOME="c:\Program Files\git"

# Copy java into the container
COPY --from=openjdk "C:\ojdkbuild" "$JAVA_HOME"

#Copy launch script used by entry point
COPY "slave-launch.ps1" ".\slave-launch.ps1"

# Copy Jenkins JNLP Slave into container
COPY --from=openjdk ".\slave.jar" ".\slave.jar"

# Copy Jenkins JNLP Slave into container
COPY --from=git ".\git" "$GIT_HOME"
	
RUN [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest 'https://aka.ms/vs/15/release/vs_buildtools.exe'  -Outfile 'C:\vs_buildtools.exe' 
RUN Start-Process -filepath C:\vs_buildtools.exe -passthru -wait -argumentlist '--add Microsoft.VisualStudio.Workload.NetCoreBuildTools --add Microsoft.VisualStudio.Workload.VCTools --add Microsoft.VisualStudio.Workload.WebBuildTools --quiet --nocache --wait'
RUN del C:\vs_buildtools.exe 


RUN [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest 'https://github.com/wixtoolset/wix3/releases/download/wix3111rtm/wix311-binaries.zip' -OutFile 'C:\wix311.zip'
RUN Expand-Archive c:\wix311.zip -DestinationPath c:\wix311
RUN del c:\wix311.zip


RUN Install-PackageProvider -Name chocolatey -RequiredVersion 2.8.5.130 -Force
RUN Install-Package dotnet4.5.2  -Force
RUN Install-Package netfx-4.5.2-devpack  -Force
RUN Install-Package windows-sdk-10 -Force
RUN Install-Package nuget.commandline  -Force


RUN [Environment]::SetEnvironmentVariable('PATH', $env:path+';'+$env:JAVA_HOME + '\bin;' + $env:GIT_HOME +'\cmd;' + $env:GIT_HOME +'\usr\bin;'+'C:\Nuget;c:\wix311;C:\Program Files (x86)\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin;C:\\Program Files (x86)\\Windows Kits\\10\\bin\\10.0.17134.0\\x64;C:\ProgramData\chocolatey\bin;', [EnvironmentVariableTarget]::Machine)
RUN [Environment]::SetEnvironmentVariable('MSBuildSDKsPath', 'C:\Program Files (x86)\Refnuerence As$semblies\Microsoft\Framework\.NETFramework\v4.5.2', [EnvironmentVariableTarget]::Machine)


ENTRYPOINT .\slave-launch.ps1


LABEL application-min-version.jenkins="2.85.0" `
    application-min-version.jenkins-lts="2.89.2" `
    application-version.jenkins-remoting="3.15" `
    application-version.windows="ltsc2016" `
    application-version.jdk="1.8" `
    application-version.chocolatey="2.8.5" `
    application-version.msbuild="15" `
    application-version.wix="3.11" `
    application-version.dotnet="4.5.2" `
    application-version.netfx="4.5.2" `
    application-version.sdk="10" `
    application-version.git="2.15.1.2"
