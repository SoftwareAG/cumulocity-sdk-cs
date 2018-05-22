[CmdletBinding()]
Param(
    [string] $version = $null
)

	
if($version)
{
    hg tag "$version"
	#hg push
}