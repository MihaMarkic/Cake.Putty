# Cake.Pscp

A Cake AddIn that extends Cake with [Putty](http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html/) command tools.

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)
[![NuGet](https://img.shields.io/nuget/v/Cake.Putty.svg)](https://www.nuget.org/packages/Cake.Putty)

## Supported tools

- Pscp v1.0.0

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

Task("Default")
    .Does(() => 
    {
        Pscp("FILENAME", "USERNAME@YOURSERVER:FILENAME");
    });

Task("WithSettings")
    .Does(() => 
    {
        Pscp("FILENAME", "YOURSERVER:FILENAME", new PscpSettings{ SshVersion = SshVersion.V2, User="USERNAME" });
    });
```

# General Notes
**This is an initial version and not tested thoroughly**.
Contributions welcome.

Tested only on Windows at this time. Ensure that Putty command line tools (pspc, plink) can be located using the PATH (e.g. check that it can be found with `which pscp`).

[![Follow @mihamarkic](https://img.shields.io/badge/Twitter-Follow%20%40mihamarkic-blue.svg)](https://twitter.com/intent/follow?screen_name=mihamarkic)
