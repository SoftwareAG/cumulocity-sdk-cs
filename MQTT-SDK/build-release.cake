
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
string defaultBranchName="develop";
string releaseBranchName="release";
string buildVersion;

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
			createReleaseBranch("release/" + lastTagCommit); 
			cleanDirectories();		
		}else{
			Console.WriteLine("nomanm, can not create a branch.");
		}
});

Task("CreateReleaseBranchAndDeploy").Does(()=> {
		checkGitVersion();
		readVersionProps();

		var canCreateVersion =
		canCreateRelease();

		if(canCreateVersion){
			buildCsProjects();
			bumpVersionProjects(Version.Release,lastTagCommit.Remove(0,1));
			packProject();
			//Deploy
			createReleaseBranch("release/" + lastTagCommit); 
			cleanDirectories();
		}else
		{
			var canCreateHotfix =
			canNextDevelopIterationOnRelease();

			if(canCreateHotfix){
				buildCsProjects();
				bumpVersionProjects(Version.Hotfix, string.Empty);
				packProject();
				readBuildVersionProps();	
				//Deploy
				createHotFixInRelease(buildVersion);
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
			packProject();		
		}
});



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
		System.Console.WriteLine(currentBranch.Split('/')[0]);
		System.Console.WriteLine(releaseBranchName);
		System.Console.WriteLine(readCommitCountInReleaseBranch);

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

	var pathBuildingScripts = "./Examples/BuildingScripts/test";
	if (DirectoryExists(pathBuildingScripts))
	{
		IO.Directory.Delete(pathBuildingScripts, true);
	}

	if(FileExists("version.props"))
	{
		DeleteFile("./version.props");
	}

	if(FileExists("buildVersion.props"))
	{
		DeleteFile("./buildVersion.props");
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
	var projects = GetFiles("./src/MQTT.Client.NetStandard/*.csproj");
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