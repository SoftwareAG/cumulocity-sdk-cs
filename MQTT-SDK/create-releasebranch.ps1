[CmdletBinding()]
Param(
    [string] $version = $null
)

	
if($version)
{
	git branch "$version"
    git tag "$version"
	#git push
}