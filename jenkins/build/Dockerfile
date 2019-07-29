FROM microsoft/dotnet:2.2-sdk

RUN apt update -y
RUN apt install -y \
        bash \
        mercurial \
        mono-devel \
        openssh-client \
        gnupg \
        apt-transport-https


RUN  apt-key adv --no-tty --batch --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN  apt install apt-transport-https
RUN  apt-get purge -y libmono-security4.0-cil
RUN  apt-get install -y -f
RUN  echo "deb https://download.mono-project.com/repo/ubuntu stable-xenial main" | tee /etc/apt/sources.list.d/mono-official-stable.list
RUN  apt update -y
RUN  apt install -y  mono-devel libmono-system-drawing4.0-cil

RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
RUN sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-debian-stretch-prod stretch main" > /etc/apt/sources.list.d/microsoft.list'
RUN apt update -y
RUN apt install -y powershell
