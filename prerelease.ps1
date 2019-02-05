<#
.SYNOPSIS
    Script to increment project versions and to add a new tag.

.DESCRIPTION
    Script will increment either the patch or minor version for all projects under a ./src folder. 

.PARAMETER fixVersion 
    Specify a version number.

.EXAMPLE 
    prerelease.ps1 '1.1.0'

#>

[cmdletbinding()]
param(
    [Parameter(Mandatory=$false)][string]$fixVersion
     )
	 
& ((Split-Path $MyInvocation.InvocationName) + "\bump-version.ps1") -mode "beta"  -fixVersion "$fixVersion"
(& git tag "prerelease$fixVersion")      

