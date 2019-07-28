﻿using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Putty;
using Cake.Testing.Fixtures;
using System;

namespace Cake.PuttyTests.Plink
{
    public class PlinkFixture : ToolFixture<PlinkSettings>, ICakeContext
    {
        public string Host { get; set; }
        public string Command { get; set; }
        IFileSystem ICakeContext.FileSystem => FileSystem;
        ICakeEnvironment ICakeContext.Environment => Environment;
        public ICakeLog Log => Log;
        ICakeArguments ICakeContext.Arguments => throw new NotImplementedException();
        IProcessRunner ICakeContext.ProcessRunner => ProcessRunner;
        public IRegistry Registry => Registry;
        public ICakeDataResolver Data => throw new NotImplementedException();
        ICakeConfiguration ICakeContext.Configuration => throw new NotImplementedException();

        public PlinkFixture() : base("plink")
        {
            ProcessRunner.Process.SetStandardOutput(new string[] { });
        }
        protected override void RunTool()
        {
            this.Plink(Host, Command, Settings);
        }
    }
}
