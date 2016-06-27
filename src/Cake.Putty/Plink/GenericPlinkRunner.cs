using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Putty
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    public class GenericPlinkRunner<TSettings> : PlinkTool<TSettings>
        where TSettings: AutoToolSettings, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="environment"></param>
        /// <param name="processRunner"></param>
        /// <param name="globber"></param>
        public GenericPlinkRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IGlobber globber) 
            : base(fileSystem, environment, processRunner, globber)
        {
        }

        /// <summary>
        /// Runs given <paramref name="host"/> and <paramref name="command"/> using given <paramref name=" settings"/>.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="command"></param>
        /// <param name="settings"></param>
        public void Run(string host, string command, TSettings settings)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host));
            }
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            Run(settings, GetArguments(host, command, settings));
        }

        private ProcessArgumentBuilder GetArguments(string host, string command, TSettings settings)
        {
            var builder = new ProcessArgumentBuilder();
            builder.AppendAll(new string[] { host, command }, settings, new string[0]);
            return builder;
        }

        /// <summary>
        /// Runs a command and returns a result based on processed output.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <param name="command"></param>
        /// <param name="settings"></param>
        /// <param name="processOutput"></param>
        /// <returns></returns>
        public T[] RunWithResult<T>(string host, string command, TSettings settings, 
            Func<IEnumerable<string>, T[]> processOutput)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            if (processOutput == null)
            {
                throw new ArgumentNullException("processOutput");
            }
            T[] result = new T[0];
            Run(settings, GetArguments(host, command, settings), 
                new ProcessSettings { RedirectStandardOutput = true }, 
                proc => {
                    result = processOutput(proc.GetStandardOutput());
                });
            return result;
        }
    }
}
