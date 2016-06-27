using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Putty
{
    /// <summary>
    /// Alias for PSCP
    /// </summary>
    [CakeAliasCategory("File Operations")]
    public static partial class PuttyAliases
    {
        /// <summary>
        /// Invokes Pscp with a single from argument.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string from, string to, PscpSettings settings)
        {
            context.Pscp(new string[] { from }, to, settings);
        }
        /// <summary>
        /// Invokes Pscp with a single from argument and settings.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string from, string to)
        {
            context.Pscp(new string[] { from }, to);
        }
        /// <summary>
        /// Invokes Pscp with array of from arguments.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string[] from, string to)
        {
            context.Pscp(from, to, null);
        }
        /// <summary>
        /// Invokes Pscp with array of from arguments.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="settings"></param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string[] from, string to, PscpSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (from?.Length < 1)
            {
                throw new ArgumentNullException("from", "from has to contain at least one entry");
            }
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException("to");
            }
            var runner = new GenericPscpRunner<PscpSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            List<string> additional = new List<string>(from);
            additional.Add(to);
            runner.Run(settings ?? new PscpSettings(), additional);
        }
    }
}
