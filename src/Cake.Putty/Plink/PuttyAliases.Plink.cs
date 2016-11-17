using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Putty
{
    /// <summary>
    /// Contains functionality for working with Plink.
    /// </summary>
    [CakeAliasCategory("Communication")]
    public static partial class PuttyAliases
    {
        /// <summary>
        /// Invokes Plink without settings
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="host">The host.</param>
        /// <param name="command">The command.</param>
        [CakeMethodAlias]
        public static void Plink(this ICakeContext context, string host, string command)
        {
            context.Plink(host, command, null);
        }
        /// <summary>
        /// Invokes Plink using given <paramref name="settings"/>.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="host">The host.</param>
        /// <param name="command">The command.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void Plink(this ICakeContext context, string host, string command, PlinkSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentNullException(nameof(host), $"{nameof(host)} has to contain at least one entry");
            }
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }
            var runner = new GenericPlinkRunner<PlinkSettings>(context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            runner.Run(host, command, settings ?? new PlinkSettings());
        }
    }
}
