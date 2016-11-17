using Cake.Core;
using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.Putty
{
    /// <summary>
    /// PLink resolver
    /// </summary>
    internal static class PlinkResolver
    {
        public static FilePath GetPlinkPath(IFileSystem fileSystem, ICakeEnvironment environment)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException("fileSystem");
            }

            if (environment == null)
            {
                throw new ArgumentNullException("environment");
            }

            // Cake already searches the PATH for the executable tool names.
            // Check for other known locations.
            return !environment.IsUnix() 
                ? CheckCommonWindowsPaths(fileSystem)
                : null;
        }

        /// <summary>
        /// Check common Plink client locations.
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        private static FilePath CheckCommonWindowsPaths(IFileSystem fileSystem)
        {
            return GetDefaultWindowsPaths(fileSystem)
                .Select(path => path.CombineWithFilePath("plink.exe"))
                .FirstOrDefault(plinkExecutable => fileSystem.GetFile(plinkExecutable).Exists);
        }

        /// <summary>
        /// Get default paths for common Plink client installations.
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <returns></returns>
        private static DirectoryPath[] GetDefaultWindowsPaths(IFileSystem fileSystem)
        {
            var paths = new List<DirectoryPath>();

            DirectoryPath programFiles;
            programFiles = new DirectoryPath(Environment.GetEnvironmentVariable("ProgramFiles(x86)"));
            var defaultPuttyPath = programFiles.Combine("PuTTY");
            if (fileSystem.GetDirectory(defaultPuttyPath).Exists)
            {
                paths.Add(defaultPuttyPath);
            }

            return paths.ToArray();
        }
    }
}
