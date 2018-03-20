
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

using Path = System.IO.Path; 
using IO = System.IO;
using XML = System.Xml;

enum Version { Beta, Release, Hotfix }; 

string currentBranch;
string lastTagCommit;
string releaseBranch;
string readCommitCountInReleaseBranch;
string defaultBranchName="default";
string releaseBranchName="Release";

//+budowanie skryptów
//+pakowanie z wersji która mam podniesiony numer
//-deploy

//-Commits
//-new branch - hg new release/r8 -> kommit
//-new version - Usuwan Snaphot -> kommit

//-Hundson - teraz wszyskim POM zmienia wersje na 8.19.0 commits (release) //bump-version.ps1 -mode release
//-Hundson ustawia Taga copy for tag c8y-agents-8.19.0 commits (release) 
//-Hudson ustawia 8.19.1-SNAPSHOT //bump-version.ps1 -mode beta

Task("CreateReleaseBranch").Does(()=> {
		checkGitVersion();
		readVersionProps();

		var canCreate = 
		canCreateRelease();	

		if(canCreate){
			buildCsProjects();
			bumpVersionProjects(Version.Release,lastTagCommit.Remove(0,1));
			packProject();
			
			//Deploy
			cleanDirectories();
			//Commit?! //hg new release/r8  + Usuwan Snaphot			
		}else{
			Console.WriteLine("nomanm, can not create a branch.");
		}
});

Task("CreateReleaseBranchAndDeploy").Does(()=> {
		checkHgVersion();
		readVersionProps();

		var canCreate =
		canCreateRelease();

		if(canCreate){
			buildCsProjects();
			bumpVersionProjects(Version.Release,lastTagCommit.Remove(0,1));
			packProject();
		}	
});

Task("CreateNextDevelopIteration").Does(()=> {
		checkHgVersion();
		readVersionProps();
		var canCreate =
		canNextDevelopIterationOnRelease();

		if(canCreate){
			buildCsProjects();
			bumpVersionProjects(Version.Hotfix, string.Empty);
			packProject();		
		}
});

// Task("Publish")  
//   .IsDependentOn("CreateReleaseBranch")
//   .Does(() => 
// {
// 	packProject();
// });


void checkHgVersion(){
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


bool canCreateRelease()
{
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
		if(currentBranch.Equals(releaseBranchName) && !readCommitCountInReleaseBranch.Equals("0") )
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
	 
	 	var projects = GetFiles("./MicroservicesSDK/src/**/*.csproj");
		foreach (var project in projects)
		{
		   Console.WriteLine(project.FullPath);
		   DotNetCoreBuild(project.FullPath, buildSettings);
		}

		projects = GetFiles("./DeviceSDK/MQTT/src/MQTT.Client.NetStandard/*.csproj");
		foreach (var project in projects)
		{
		   Console.WriteLine(project.FullPath);
		   DotNetCoreBuild(project.FullPath, buildSettings);
		}
}

void buildScriptProject(){

		var command = "build-scripts.sh";
 		var settings = new ProcessSettings
		{
		   Arguments = new ProcessArgumentBuilder().Append(command)
		};
		StartProcess("sh", settings);
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
	
 		var settings = new ProcessSettings
		{
		   Arguments = new ProcessArgumentBuilder().Append(command)
		};
		StartProcess("pwsh", settings);
}
void createReleaseBranch()
{
		Information("Create Release Branch {0}", DateTime.Now);

		var command = "create-releasebranch.ps1"; // "createrelease.ps1 -version " + version

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

	var pathBuildingScripts = "./Examples/BuildingScripts/test";
	if (DirectoryExists(pathBuildingScripts))
	{
		IO.Directory.Delete(pathBuildingScripts, true);
	}

	if(FileExists("version.props"))
	{
		IO.File.DeleteFile("./version.props");
	}
}
void packProject()
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
	{             OutputDirectory = path,
					NoBuild = true,
					Configuration = configuration,
					MSBuildSettings = buildSettings
	};  

    //MQTT
	projects = GetFiles("./src/MQTT.Client.NetStandard/*.csproj");
	foreach (var project in projects)
	{
		Console.WriteLine(project.FullPath);
		DotNetCorePack(project.FullPath, packSettings);
	}
}

Task("Default")
  .IsDependentOn("CreateReleaseBranch")
  .Does(() =>
{
});

RunTarget(target);