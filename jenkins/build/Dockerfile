# FROM microsoft/dotnet
FROM ubuntu:18.04

RUN apt update -y
RUN apt install -y \
        bash \
        curl \
        mercurial \
        git \
        openssh-client \
        gnupg \
        apt-transport-https \
        software-properties-common \
        wget
        # mono-devel \

# RUN  apt-key adv --no-tty --batch --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
# RUN  apt-get purge -y libmono-security4.0-cil
# RUN  apt-get install -y -f
# RUN  echo "deb https://download.mono-project.com/repo/ubuntu stable-xenial main" | tee /etc/apt/sources.list.d/mono-official-stable.list
# RUN  apt update -y
# RUN  apt install -y mono-devel libmono-system-drawing4.0-cil

RUN wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb
RUN apt update -y && add-apt-repository universe
RUN apt install -y dotnet-sdk-3.1 powershell
RUN dotnet --list-sdks
