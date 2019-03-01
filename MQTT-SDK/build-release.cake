
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var buildDir = Directory("./publish");

using Path = System.IO.Path; 
using IO = System.IO;
using XML = System.Xml;

enum Version { Beta, Release, Hotfix }; 

string currentBranch;
string lastTagCommit;
string releaseBranch;
string readCommitCountInReleaseBranch;
string defaultBranchName="develop";  //#default #develop
string releaseBranchName="release";
string buildVersion;

Task("CreateReleaseBranchAndDeploy").Does(()=> {
		checkGitVersion();
		readVersionProps();

		var canCreateVersion =
		canCreateRelease();

		if(canCreateVersion){
		    checkNumberCommitsAfterLastTag();
			buildCsProjects();
			bumpVersionProjects(Version.Release,lastTagCommit.Remove(0,1));
			readBuildVersionProps();
			packCsProject();
			deployCsProject();
			createReleaseBranch("release/" + lastTagCommit); 
			cleanDirectories();
		}else
		{
			var canCreateHotfix =
			canNextDevelopIterationOnRelease();

			if(canCreateHotfix){
				buildCsProjects();
				bumpVersionProjects(Version.Hotfix, string.Empty);
				readBuildVersionProps();	
				packCsProject();
				deployCsProject();
				createHotFixInRelease("r" + buildVersion);
				System.Console.WriteLine(buildVersion);
				cleanDirectories();
			}
		}	
});

Task("CreateNextDevelopIteration").Does(()=> {
		checkGitVersion();
		readVersionProps();
		var canCreate =
		canNextDevelopIterationOnRelease();

		if(canCreate){
			buildCsProjects();
			bumpVersionProjects(Version.Hotfix, string.Empty);
			packCsProject();		
		}
});


void checkNumberCommitsAfterLastTag()
{	
 	var settings = new ProcessSettings
	{
		Arguments = new ProcessArgumentBuilder().Append("check-commits.ps1 -tag " + lastTagCommit)
	};
	StartProcess("pwsh", settings);	
}
void checkGitVersion(){
 		var settings = new ProcessSettings
		{
		   Arguments = new ProcessArgumentBuilder().Append("check-version.ps1  -local false")
		};
		StartProcess("pwsh", settings);
}

void readVersionProps()
{
		if (FileExists("./version.props"))
		{
			
			string[] lines = System.IO.File.ReadAllLines("./version.props");
			
			foreach (string line in lines)
			{
					if (line.StartsWith("CurrentBranch"))
					{
						currentBranch = line.Substring("CurrentBranch:".Length).Trim();
					}
					else if (line.StartsWith("LastTagCommit"))
					{
						lastTagCommit = line.Substring("LastTagCommit:".Length).Trim();
					}
					else if (line.StartsWith("ReleaseBranch"))
					{
						releaseBranch = line.Substring("ReleaseBranch:".Length).Trim();
					}
					else if (line.StartsWith("ReadCommitCountInReleaseBranch"))
					{
						readCommitCountInReleaseBranch = line.Substring("ReadCommitCountInReleaseBranch:".Length).Trim();
					}
			}
	}
}

void readBuildVersionProps()
{
		if (FileExists("./buildVersion.props"))
		{
			
			string[] lines = System.IO.File.ReadAllLines("./buildVersion.props");
			
			foreach (string line in lines)
			{
					if (line.StartsWith("buildVersion"))
					{
						buildVersion = line.Substring("buildVersion:".Length).Trim();
					}
			}
		}
}


bool canCreateRelease()
{
	    System.Console.WriteLine("CanCreateRelease");
		System.Console.WriteLine(currentBranch);
		System.Console.WriteLine(lastTagCommit);
		System.Console.WriteLine(readCommitCountInReleaseBranch);
		
		if(currentBranch.Equals(defaultBranchName) && !lastTagCommit.Equals("r0.0.0") && readCommitCountInReleaseBranch.Equals("0") )
		{
			System.Console.WriteLine("Yes, you can create a release branch.");
			return true;
		}else{
			System.Console.WriteLine("No, you can not create a release branch.");
			return false;
		}
}

bool canNextDevelopIterationOnRelease()
{
	    System.Console.WriteLine("CanNextDevelopIterationOnRelease");
		System.Console.WriteLine("split" + currentBranch.Split('/')[0]);
		System.Console.WriteLine("releaseBranchName" + releaseBranchName);
		System.Console.WriteLine("readCommitCountInReleaseBranch" + readCommitCountInReleaseBranch);

		if(currentBranch.Split('/')[0].Equals(releaseBranchName) && !readCommitCountInReleaseBranch.Equals("0") )
		{
			System.Console.WriteLine("Yes, you can create a next develop iteration.");
			return true;
		}else{
			System.Console.WriteLine("No, you can not create  a next develop iteration.");
			return false;
		}
}

void buildCsProjects(){

     
		var buildSettings = new DotNetCoreBuildSettings
		{
			Configuration = configuration
		};
	 
	 	var projects = GetFiles("./src/**/*.csproj");
		foreach (var project in projects)
		{
		   Console.WriteLine(project.FullPath);
		   DotNetCoreBuild(project.FullPath, buildSettings);
		}
}


void bumpVersionProjects(Version version,string fixVersion)
{

	    string command = "";

		switch(version)
		{	
			case Version.Beta:
				    command = "bump-version.ps1 -mode beta";
			break;			
			case Version.Release:
				    command = "bump-version.ps1 -mode release";
			break;
			case Version.Hotfix:
				    command = "bump-version.ps1 -mode hotfix";
			break;			
			default:
			break;
		}
       if(fixVersion.Length > 4){
		   command = command + " -fixVersion " + fixVersion; 
	   }
	   Information("bumpVersionProjects: {0}", command);
	
 		var settings = new ProcessSettings
		{
		   Arguments = new ProcessArgumentBuilder().Append(command)
		};
		StartProcess("pwsh", settings);
}
void createReleaseBranch(string version)
{
		Information("Create Release Branch {0}", DateTime.Now);

		//var command = "create-releasebranch.ps1"; 
        var command = "create-releasebranch.ps1 -version " + version;

	 	var settings = new ProcessSettings
		{
		   Arguments = new ProcessArgumentBuilder().Append(command)
		};
		StartProcess("pwsh", settings);
}
void createHotFixInRelease(string version)
{
		Information("Create Hot Fix {0}", DateTime.Now);

        var command = "create-hotfix.ps1 -version " + version;

	 	var settings = new ProcessSettings
		{
		   Arguments = new ProcessArgumentBuilder().Append(command)
		};
		StartProcess("pwsh", settings);
}
void cleanDirectories(){

	var pathPublish = "./publish/";
	if (DirectoryExists(pathPublish))
	{
		CleanDirectory(pathPublish);
	}

	if(FileExists("version.props"))
	{
		DeleteFile("./version.props");
	}

	if(FileExists("buildVersion.props"))
	{
		DeleteFile("./buildVersion.props");
	}
	
	if(FileExists("commits.props"))
	{
		DeleteFile("./commits.props");
	}
}

void deployCsProject()
{
	Information("The deployment was started!");
  
    var command = "deploy.sh";
	
	var settings = new ProcessSettings
	{
	   Arguments = new ProcessArgumentBuilder().Append(command)
	};
	StartProcess("bash", settings);
}
void packCsProject()
{
	var path =  "./publish/";
	if (DirectoryExists("./publish/"))
	{
		CleanDirectory("./publish/");
	}else
	{		
			CreateDirectory(path);
	}

	var buildSettings = new DotNetCoreMSBuildSettings();
	var packSettings = new DotNetCorePackSettings  
	{              OutputDirectory = path,
					NoBuild = true,
					Configuration = configuration,
					MSBuildSettings = buildSettings
	};  

	var projects = GetFiles("./src/**/*.csproj");
	foreach (var project in projects)
	{
		Console.WriteLine(project.FullPath);
		DotNetCorePack(project.FullPath, packSettings);
	}

}

void publishNugets()
{
    var nugetFiles = GetFiles(buildDir.Path.FullPath+"/**/*.nupkg");
	var source = EnvironmentVariable("PRIVATE_FEED_SOURCE");
    var accessToken = EnvironmentVariable("PRIVATE_FEED_ACCESSTOKEN");
	
    foreach(var file in nugetFiles)
    {				
        //var settings = new DotNetCoreNuGetPushSettings()
        //{
        //    Source = source,
        //    ApiKey = accessToken
        //};
		
		// Read the settings from Nuget.Config where DefaultPushSource must be defined.
		// ~/.config/NuGet/NuGet.Config or 
        // ~/.nuget/NuGet/NuGet.Config (varies by OS distribution)
		
        DotNetCoreNuGetPush(file.FullPath);
    }
}

Task("Default")
  .IsDependentOn("CreateReleaseBranchAndDeploy")
  .Does(() =>
{
});

RunTarget(target);