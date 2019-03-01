[CmdletBinding()]
Param(
    [string] $version = $null
)

	
if($version)
{
    $version  = "$version"
    git add .
    git commit -m "prepare hotfix $version"
    git tag "$version"
	git push -f --tags
}