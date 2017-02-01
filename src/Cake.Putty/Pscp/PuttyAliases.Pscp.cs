using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Putty
{
    /// <summary>
    /// Contains functionality for working with PSCP.
    /// </summary>
    [CakeAliasCategory("Communication")]
    public static partial class PuttyAliases
    {
        /// <summary>
        /// Invokes Pscp with a single from argument and <paramref name="settings"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string from, string to, PscpSettings settings)
        {
            context.Pscp(new string[] { from }, to, settings);
        }
        /// <summary>
        /// Invokes Pscp with a single from argument.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string from, string to)
        {
            context.Pscp(new string[] { from }, to);
        }
        /// <summary>
        /// Invokes Pscp with array of from arguments without settings..
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        [CakeMethodAlias]
        public static void Pscp(this ICakeContext context, string[] from, string to)
        {
            context.Pscp(from, to, null);
        }
        /// <summary>
        /// Invokes Pscp with array of from arguments.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="settings">The settings.</param>
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
            var runner = new GenericPscpRunner<PscpSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            List<string> additional = new List<string>(from);
            additional.Add(to);
            runner.Run(settings ?? new PscpSettings(), additional);
        }
    }
}
