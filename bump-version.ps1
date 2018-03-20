<#
.SYNOPSIS
    Script to increment project versions for beta, minor releases and hotfixes.

.DESCRIPTION
    Script will increment either the patch or minor version for all projects under a ./src folder. Intelligently infers the next beta, release, or hotfix version. 

.PARAMETER mode 
    Specify 'beta', 'release' or 'hotfix'

.EXAMPLE 
    publish.ps1 release

#>

[cmdletbinding()]
param(
    [Parameter(Mandatory=$true)][string]$mode,
    [Parameter(Mandatory=$false)][string]$fixVersion
     )

if($mode -ne "release" -and $mode -ne "hotfix" -and $mode -ne "beta"){
    Write-Output "Syntax:  publish.ps1 [[-mode] <String>]"
    Write-Output "Please specify a mode; 'beta', 'release' or 'hotfix'."
    Write-Output "Get-Help ./publish.ps1 for more info"
    exit 0;
}
Write-Output "mode = $mode"



#$srcDir = Get-ChildItem ./src 
$srcDir = Get-ChildItem ./MicroservicesSDK/src/* -include ('*.csproj', '*.scriptversion') -recurse
$srcDir += Get-ChildItem ./DeviceSDK/MQTT/src/* -include ('*.csproj', '*.scriptversion') -recurse
$srcDir += Get-ChildItem ./Examples/BuildingScripts/* -include ('*.csproj', '*.scriptversion') -recurse

$save = $true;
foreach ($folder in $srcDir) {
    #$p = Join-Path -Path $folder.FullName -ChildPath '*.csproj';
	$p = $folder.FullName
    # only src project folders -> folders with a csproj file 
    if (Test-Path $p -PathType Leaf) {
        #$projectFolders += $folder.FullName
        $projFile =  Get-ChildItem -Path $p | Select-Object -last 1
        $proj = [xml](get-content $projFile)
        $proj.GetElementsByTagName("Version") | ForEach-Object{
            $origVer = $_."#text"
            $verArray = $_."#text".Split(".")
            $majorInt = [convert]::ToInt32( $verArray[0], 10)
            $minorInt = [convert]::ToInt32( $verArray[1], 10)
           
            $patchParts =  $verArray[2].Split("-")
            
            
           if($patchParts.Length -eq 3){ 
                $betaInt = [convert]::ToInt32($patchParts[2], 10)
           }
           else{
                $betaInt = 0
           }

            $patchInt = [convert]::ToInt32( $patchParts[0], 10)

            switch($mode) {
                "release"{
                    if($betaInt -eq 0)
                    {
                        $minorInt = $minorInt + 1
                        $patchInt = 0
                    }
                    else{
                        $betaInt = 0
                    }
                }
                "hotfix"{
                    if($betaInt -ne 0)
                    {
                        Write-Output "Cannot hotfix a beta package, please update versions manually"
                        $save = $false
                    }
                    else{
                        $patchInt = $patchInt + 1
                       
                    }
                    
                }
                "beta" {

                    if($betaInt -eq 0)
                    {
                        #$minorInt = $minorInt + 1
                        #$patchInt = 0
                    }
                   
                    $betaInt = $betaInt + 1

                }
            }

            if($save){
                $buildVersion =  $majorInt.ToString() + "." + $minorInt.ToString() + "." + $patchInt.ToString()
                if($mode -eq "beta"){
                     #$betaString = "-beta-{0:D5}" -f [convert]::ToInt32($betaInt, 10)
                    $betaString = "-SNAPSHOT" -f [convert]::ToInt32($betaInt, 10)
                    $buildVersion = $buildVersion + $betaString
                }

                if($fixVersion.length -gt 4){
                    $buildVersion = $fixVersion
                }

                $_."#text" = $buildVersion
                Write-Output "Incrementing version for: $($projFile.Name)"
                Write-Output "    $origVer --> $buildVersion"
            
                $proj.Save($projFile)
            }
        }
    }
}
