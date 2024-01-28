# Cake.Putty

A Cake AddIn that extends Cake with [Putty](http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html/) command tools.

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)
[![NuGet](https://img.shields.io/nuget/v/Cake.Putty.svg)](https://www.nuget.org/packages/Cake.Putty)

## Requirements

- since 1.7.0 references Cake 4.0.0, targets .net6+
- since 1.6.0 references Cake 1.0.0
- since 1.5.0 references Cake 0.33
- since 1.4.0 references Cake 0.28
- since 1.3.0 references Cake 0.26
- since 1.2.0 supports .netstandard (adds Linux and MacOS support)
- since 1.1.3 references Cake 0.22

## Supported tools

- PLink v1.1.0
- Pscp v1.0.0

## Cake dependency
- Cake v0.13 up to v1.1.1
- Cake v0.17 v1.1.2+
- Cake v0.26 v1.3.0+

## Including addin
Including addin in cake script is easy.
```
#addin "Cake.Putty"
```

## Usage

To use the addin just add it to Cake call the aliases and configure any settings you want.

```csharp
#addin "Cake.Putty"

...

Task("Pscp")
    .Does(() => 
    {
        Pscp("FILENAME", "USERNAME@YOURSERVER:FILENAME");
    });

Task("PscpSettings")
    .Does(() => 
    {
        Pscp("FILENAME", "YOURSERVER:FILENAME", new PscpSettings{ SshVersion = SshVersion.V2, User="USERNAME" });
    });

Task("Plink")
    .Does(() =>
    {
        Plink("USERNAME@YOURSERVER", "ls");
    });
Task("PlinkSettings")
    .Does(() =>
    {
        Plink("YOURSERVER", "ls", new PlinkSettings { User="USERNAME", Protocol = PlinkProtocol.Ssh, SshVersion = SshVersion.V2 });
    });
```

# General Notes
**This is an initial version and not tested thoroughly**.
Contributions welcome.

Tested only on Windows at this time. Ensure that Putty command line tools (pspc, plink) can be located using the PATH (e.g. check that it can be found with `which pscp`).

[![Follow @mihamarkic](https://img.shields.io/badge/Twitter-Follow%20%40mihamarkic-blue.svg)](https://twitter.com/intent/follow?screen_name=mihamarkic)
