# Cumulocity C# Library #

Cumulocity C# Library is an asynchronous, event-driven networking library to ease your development for connecting to the Cumulocity cloud. The library uses Cumulocity's self invented SmartREST protocol for M2M communication which targets any device that are capable of running embedded Linux or Windows.
### Dependencies ### 
* [MQTTnet](https://github.com/chkr1011/MQTTnet/blob/master/LICENSE)
* [Serilog](https://github.com/serilog/serilog/blob/dev/LICENSE)
* [Sonaranalyzer-dotnet](https://github.com/SonarSource/sonaranalyzer-dotnet/blob/master/LICENSE)

### Prerequisites ###

* *.NET Core SDK 2.X*

### .NET Core 2.0 - Supported OS versions

The tables below provide OS version information supported by .NET Core 2.0.

#### Keys used in the tables

* **Bold numbers** indicate additions to this release.
* A '+' indicates the minimum supported version.
* Where possible, links to Distribution-owned lifecycle documentation is provided.

#### Windows

OS                            | Version                       | Architectures  | Notes
------------------------------|-------------------------------|----------------|-----
Windows Client                | 7 SP1+, 8.1                  | x64, x86       |
Windows 10 Client             | Version 1607+                 | x64, x86       |
Windows Server                | 2008 R2 SP1+                  | x64, x86       |

See the [Windows Lifecycle Fact Sheet](https://support.microsoft.com/en-us/help/13853/windows-lifecycle-fact-sheet) for details regarding each Windows release lifecycle.

#### macOS

OS                            | Version                       | Architectures  | Notes
------------------------------|-------------------------------|----------------|-----
Mac OS X                      | 10.12+                        | x64            | [Apple Support Sitemap](https://support.apple.com/sitemap) <br> [Apple Security Updates](https://support.apple.com/en-us/HT201222)

#### Linux

OS                            | Version                       | Architectures  | Notes
------------------------------|-------------------------------|----------------|-----
Red Hat Enterprise Linux <br> CentOS <br> Oracle Linux | 7    | x64            | [Red Hat support policy](https://access.redhat.com/support/policy/updates/errata/) <br> [CentOS lifecycle](https://wiki.centos.org/FAQ/General#head-fe8a0be91ee3e7dea812e8694491e1dde5b75e6d) <br> [Oracle Linux lifecycle](http://www.oracle.com/us/support/library/elsp-lifetime-069338.pdf)
Fedora                        | 26, 27                        | x64            | [Fedora lifecycle](https://fedoraproject.org/wiki/End_of_life)
Debian                        | 9, 8.7+                       | x64            | [Debian lifecycle](https://wiki.debian.org/DebianReleases)
Ubuntu <br> Linux Mint        | 17.10, 16.04, 14.04 <br> 18, 17  | x64         | [Ubuntu lifecycle](https://wiki.ubuntu.com/Releases) <br> [Linux Mint end of life announcements](https://forums.linuxmint.com/search.php?keywords=%22end+of+life%22&terms=all&author=&sc=1&sf=titleonly&sr=posts&sk=t&sd=d&st=0&ch=300&t=0&submit=Search)
openSUSE                      | 42.2+                         | x64            | [OpenSUSE lifecycle](https://en.opensuse.org/Lifetime)
SUSE Enterprise Linux (SLES)  | 12                            | x64            | [SUSE lifecycle](https://www.suse.com/lifecycle/)

* **Bold numbers** indicate additions to this release.
* '+' indicates the minimum supported version.

#### Out of support OS versions

Support for the following versions was ended by the distribution owners and are [not supported by .NET Core 2](https://github.com/dotnet/core/blob/master/os-lifecycle-policy.md).

OS                            | Version                       | Architectures  | End of Life
------------------------------|-------------------------------|----------------|-----
Ubuntu                        | 17.04                         | x64            | [January 2018](https://lists.ubuntu.com/archives/ubuntu-announce/2018-January.txt)
openSUSE                      | 42.2                          | x64            | [January 2018](https://lists.opensuse.org/opensuse-security-announce/2017-11/msg00066.html)
Fedora                        | 25                            | x64            | [December 2017](https://fedoramagazine.org/fedora-25-end-life/)
Fedora                        | 24                            | x64            | [August 2017](https://fedoramagazine.org/fedora-24-eol/)
Ubuntu                        | 16.10                         | x64            | [July 2017](https://lists.ubuntu.com/archives/ubuntu-announce/2017-July/000223.html)
openSUSE                      | 42.1                          | x64            | [May 2017](https://lists.opensuse.org/opensuse-security-announce/2017-05/msg00053.html)

### How to build the library? ###

* Download the source code:

```
#!bash
git clone git@github.com:SoftwareAG/cumulocity-sdk-cs.git
```

* Customize the *build.cake* to your needs.
* Build the library in *debug* mode:

```
#!bash

./build.sh -target=BuildDebug
```

* Build the library in *release* mode for production release:

```
#!bash

./build.sh -target=Build

```


### FAQ ###
* An error occurred when executing task "Build" 
  Try removing all intermediate build files first and start a new task "Clean" :

```
#!bash

./build.sh -target=Clean
```

* How can I contact Cumulocity in case I have questions?  
  You can reach us by email at support@cumulocity.com
