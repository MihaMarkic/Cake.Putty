var Project = Directory("./Cake.Putty/");
var TestProject = Directory("./Cake.PuttyTests/");
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
		DotNetCoreBuild (CakePuttySln, new DotNetCoreBuildSettings {
			Configuration = "Release"
		});
});

Task("UnitTest")
	.IsDependentOn("Default")
	.Does(() =>
	{
		NUnit3(CakeTestPuttyAssembly);
	});

Task("NuGetPack")
	.IsDependentOn("Default")
	.IsDependentOn("UnitTest")
	.Does (() =>
{
	CreateDirectory(Nupkg);
	DotNetCorePack (CakePuttyProj, new DotNetCorePackSettings
     {
         Configuration = "Release",
         OutputDirectory = "./nupkg/"
     });
});

RunTarget (target);
