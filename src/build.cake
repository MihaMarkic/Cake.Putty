#addin "Cake.FileHelpers"

var Project = Directory("./Cake.Putty/");
var TestProject = Directory("./Cake.Putty.Tests/");
var CakePuttyProj = Project + File("Cake.Putty.csproj");
var CakeTestPuttyProj = TestProject + File("Cake.Putty.Test.csproj");
var CakeTestPuttyAssembly = TestProject + Directory("bin/Release") + File("Cake.Putty.Tests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var CakePuttySln = File("./Cake.Putty.sln");
var CakePuttyNuspec = File("./Cake.Putty.nuspec");
var Nupkg = Directory("./nupkg");

var target = Argument("target", "Default");
var version = "";

Task("Default")
	.Does (() =>
	{
		NuGetRestore (CakePuttySln);
		DotNetBuild (CakePuttySln, c => {
			c.Configuration = "Release";
			c.Verbosity = Verbosity.Minimal;
		});
});

Task("UnitTest")
	.IsDependentOn("Default")
	.Does(() =>
	{
		NUnit3(CakeTestPuttyAssembly);
	});

Task("NuGetPack")
	.IsDependentOn("GetVersion")
	.IsDependentOn("Default")
	.IsDependentOn("UnitTest")
	.Does (() =>
{
	CreateDirectory(Nupkg);
	NuGetPack (CakePuttyNuspec, new NuGetPackSettings { 
		Version = version,
		Verbosity = NuGetVerbosity.Detailed,
		OutputDirectory = Nupkg,
		BasePath = "./",
	});	
});

Task("GetVersion")
	.Does(() => {
		var assemblyInfo = ParseAssemblyInfo(AssemblyInfo);
		var semVersion = string.Join(".", assemblyInfo.AssemblyVersion.Split('.').Take(3));
		Information("Version {0}", semVersion);
		version = semVersion;
	});

RunTarget (target);
