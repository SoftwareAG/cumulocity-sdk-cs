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


RUN  apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN  sh -c 'echo "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" > /etc/apt/sources.list.d/mono-official-stable.list'
RUN  apt update -y
RUN apt install mono-complete 


RUN wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb
RUN apt update -y && add-apt-repository universe
RUN apt install -y dotnet-sdk-3.1 powershell
RUN dotnet --list-sdks