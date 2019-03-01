[CmdletBinding()]
Param(
    [string] $version = $null
)

	
if($version)
{
    git checkout -b "$version"
    git add .
    git commit -m "prepare release $version"
	git push -f
}