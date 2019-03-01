[CmdletBinding()]
Param(
    [string] $version = $null
)

	
if($version)
{
    git tag "$version"
	#git push
}