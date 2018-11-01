$JAVA_VERSION='8u191'
$JAVA_OJDKBUILD_VERSION='1.8.0.191-1'
$JAVA_OJDKBUILD_ZIP='java-1.8.0-openjdk-1.8.0.191-1.b12.ojdkbuild.windows.x86_64.zip'
$JAVA_OJDKBUILD_SHA256='d52167198912e239bba19e059870c5742558096ad7b82fe73d51461361b149bd'

$JAVA_HOME='C:\\ojdkbuild'



[System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -Uri https://github.com/ojdkbuild/ojdkbuild/releases/download/1.8.0.191-1/java-1.8.0-openjdk-1.8.0.191-1.b12.ojdkbuild.windows.x86_64.zip -OutFile 'ojdkbuild.zip'
Expand-Archive ojdkbuild.zip -DestinationPath C:\
Move-Item  -Path ('C:\{0}' -f ($JAVA_OJDKBUILD_ZIP -Replace '.zip$', ''))  -Destination $JAVA_HOME 
Remove-Item ojdkbuild.zip -Force

[Environment]::SetEnvironmentVariable('PATH', $env:path+';'+$JAVA_HOME + '\bin;', [EnvironmentVariableTarget]::Machine)


$SLAVE_FILENAME='slave.jar'
$REMOTING_VERSION='3.15'

$SLAVE_HASH_FILENAME=$SLAVE_FILENAME.sha1

Invoke-WebRequest "https://repo.jenkins-ci.org/public/org/jenkins-ci/main/remoting/$REMOTING_VERSION/remoting-$REMOTING_VERSION.jar" -OutFile $SLAVE_FILENAME -UseBasicParsing
Invoke-WebRequest "https://repo.jenkins-ci.org/public/org/jenkins-ci/main/remoting/$REMOTING_VERSION/remoting-$REMOTING_VERSION.jar.sha1" -OutFile $SLAVE_HASH_FILENAME -UseBasicParsing




    
$GIT_VERSION='2.19.1'
$GIT_TAG='v2.19.1.windows.1'

$GIT_FILENAME="MinGit-$GIT_VERSION-64-bit.zip"
$GIT_HASH_FILENAME="$GIT_FILENAME.sha256"
$GIT_HOME="c:\Program Files\git"
Invoke-WebRequest "https://github.com/git-for-windows/git/releases/download/$GIT_TAG/$GIT_FILENAME" -OutFile $GIT_FILENAME -UseBasicParsing

Expand-Archive $GIT_FILENAME $GIT_HOME

Remove-Item $GIT_FILENAME -Force

[Environment]::SetEnvironmentVariable('PATH',  $env:path+';'+$JAVA_HOME + '\bin;' + $GIT_HOME +'\cmd;' + $GIT_HOME +'\usr\bin;', [EnvironmentVariableTarget]::Machine)
