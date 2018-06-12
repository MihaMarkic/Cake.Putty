var Project = Directory("./Cake.Putty/");
var TestProject = Directory("./Cake.PuttyTests/");
var CakePuttyProj = Project + File("Cake.Putty.csproj");
var CakeTestPuttyProj = TestProject + File("Cake.PuttyTests.csproj");
var CakeTestPuttyAssembly = TestProject + Directory("bin") + Directory("Release") + Directory("netcoreapp2.0") + File("Cake.PuttyTests.dll");
var AssemblyInfo = Project + File("Properties/AssemblyInfo.cs");
var CakePuttySln = File("./Cake.Putty.sln");
var CakePuttyNuspec = File("./Cake.Putty.nuspec");
var Nupkg = Directory("./nupkg");

var target = Argument("target", "Default");
var version = "";

Task("Default")
	.Does (() =>
	{
		DotNetCoreClean(CakePuttySln);
		DotNetCoreBuild (CakePuttySln, new DotNetCoreBuildSettings {
			Configuration = "Release"
		});
});

Task("UnitTest")
	.IsDependentOn("Default")
	.Does(() =>
	{
		DotNetCoreTest(CakeTestPuttyProj, new DotNetCoreTestSettings
		 {
			 Configuration = "Release"
		 });
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
