//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
 
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
  
//////////////////////////////////////////////////////////////////////
///    Build Variables
/////////////////////////////////////////////////////////////////////
var binDir = "";       //Destination Binary File Directory name i.e. bin
var solutionFile = ""; // Solution file if needed
var outputDir = Directory("./publish") + Directory(configuration);  // The output directory the build artefacts saved too


var testFailed = false;
var solutionDir = System.IO.Directory.GetCurrentDirectory();
var testResultDir = System.IO.Path.Combine(solutionDir, "test-results");
var artifactDir = "./artifacts";
 
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////


Information("Solution Directory: {0}", solutionDir);
Information("Test Results Directory: {0}", testResultDir);

Task("PrepareDirectories")
	.Does(() =>
	{
		EnsureDirectoryExists(testResultDir);
		EnsureDirectoryExists(artifactDir);
	});
	
Task("Clean")
    .IsDependentOn("PrepareDirectories")
    .Does(() =>
	{		
		if (DirectoryExists(outputDir))
			{
				CleanDirectory(outputDir);
			}
	});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => {
	    
	    var projects = GetFiles("./src/*.NetStandard/*.csproj");
		foreach (var project in projects)
		{
		   Information(project.FullPath);
		   DotNetCoreRestore(project.FullPath);
		}
});
 
Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
	
	var buildSettings = new DotNetCoreBuildSettings
     {
         Configuration = configuration
		//, OutputDirectory = outputDir
     };
	 
    if(IsRunningOnWindows())
    {
		// Use MSBuild
		// MSBuild(solutionFile , settings => settings.SetConfiguration(configuration));	 
	  	var projects = GetFiles("./src/*.NetStandard/*.csproj");
		foreach (var project in projects)
		{
		   DotNetCoreBuild(project.FullPath, buildSettings);
		}
    }
    else
    {
		// Use XBuild
	 	var projects = GetFiles("./src/*.NetStandard/*.csproj");
		foreach (var project in projects)
		{
		   DotNetCoreBuild(project.FullPath, buildSettings);
		}
    }
});

Task("Test")
.IsDependentOn("Clean")
.IsDependentOn("Restore")
.IsDependentOn("Build")
	.ContinueOnError()
	.Does(() =>
	{
	    //Workaround Fix Asset file project.assets.json error not found in dotnet core
		var projects = GetFiles("./test/*.NetStandard/*.csproj");
		foreach (var project in projects)
		{
		   Information(project.FullPath);
		   DotNetCoreRestore(project.FullPath);
		}

        //Normal solution
		var tests = GetFiles("./test/MQTT.Client.XUnitTest/*.csproj");
		
		foreach(var test in tests)
		{
			var projectFolder = System.IO.Path.GetDirectoryName(test.FullPath);
			try
			{
			    Information("Solution Directory: {0} {1}", solutionDir,test.FullPath);
				DotNetCoreTest(test.FullPath, new DotNetCoreTestSettings
				{
					ArgumentCustomization = args => args.Append("-l \"trx;LogFileName=Result.xml\""),
					WorkingDirectory = projectFolder
				});
			}
			catch(Exception e)
			{
				testFailed = true;
				Error(e.Message.ToString());
			}
		}

		// Copy test result files.
		var tmpTestResultFiles = GetFiles("./**/Result.xml");
		CopyFiles(tmpTestResultFiles, testResultDir);
		XmlTransform("./tools/MsUnit.xslt", testResultDir +"/Result.xml", testResultDir +"/JUnit.Result.xml");	
	});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
 
Task("Default")
    .IsDependentOn("Test");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
 
RunTarget(target);