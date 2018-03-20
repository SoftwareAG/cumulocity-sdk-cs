function ReadCommitCountInBranch
(
    [string] $branch
)
{

	$hglog = (& git rev-parse --verify --quiet $branch)
	Write-Host "ReadCommitCountInBranch: $hglog"
	
	if ($hglog -eq $null) 
	{
		return 0;
	}else
	{	
		$commitCountInBranch = (& git rev-list --count HEAD ^$branch)
		if($commitCountInBranch -eq 0)
		{
		  $commitCountInBranch  = 1;
		}
		return $commitCountInBranch ;
	}
}
function ReadBranchName
{
    $branchName = (& git rev-parse --abbrev-ref HEAD).Trim()
    return $branchName
}
function ReadCommitCount
{
    $count = (& hg id --num --rev tip).Trim()
    return $count
}

function ReadIsLastTagCommit
(
    [string] $branch
)
{
    Try
    {
		$lasttag = (& git describe --abbrev=0 --tags).Trim()
				
		if($lasttag.Length -eq 0)
		{
			return "r0.0.0"   
		}
		return $lasttag.Substring(0,$lasttag.Length)
	}
	Catch
    {
	   return "r0.0.0"
	}
}

function CreateReleaseBranch
{
	$versionFileName = ".\version.props"
	$VersionExist = Test-Path -Path $versionFileName

	if ($VersionExist -eq $true) {
		Write-Host "Yes The version file exists"
		Remove-Item $versionFileName
	}

	New-Item $versionFileName -ItemType file

    $CurrentBranch = ReadBranchName
	$LastTagCommit = ReadIsLastTagCommit -branch $ReadBranchName
	$ReleaseBranch = "release/$LastTagCommit"	
	$CommitCountInBranch = ReadCommitCountInBranch  -branch $ReleaseBranch
	
	Write-Host "ReleaseBranch: $ReleaseBranch"
	Write-Host "LastTagCommit: $LastTagCommit"
	Write-Host "ReadCommitCountInBranch: $CommitCountInBranch"
	
	"CurrentBranch:$CurrentBranch" | Add-Content $versionFileName
	"LastTagCommit:$LastTagCommit" | Add-Content $versionFileName
	"ReleaseBranch:$ReleaseBranch" | Add-Content $versionFileName
	"ReadCommitCountInReleaseBranch:$CommitCountInBranch" | Add-Content $versionFileName

	if( ($CurrentBranch -eq "default") -And ( $CommitCountInBranch -eq  0) -And ( $LastTagCommit -ne  "r0.0.0")  )
	{
		# hg up $LastTagCommit 
	    # hg flow release start $LastTagCommit
		# hg push
	}
	elseif($CurrentBranch.StartsWith("release/") )
	{
		$major,$minor,$rev = $LastTagCommit.split('.')
		if($LastTagCommit -eq  "r0.0.0" )
		{
		  $branch, $tag = $CurrentBranch.split('/') 
		  $LastTagCommit =$tag 
		  $major,$minor,$rev = $LastTagCommit.split('.')
		}
		$rev = $rev/1 + 1
		

		# hg tag "$major.$minor.$rev"
		# hg push
	}

}

CreateReleaseBranch