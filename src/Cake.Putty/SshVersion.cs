using Cake.Putty;

namespace Cake.Putty
{
    /// <summary>
    /// SSH version.
    /// </summary>
    public enum SshVersion
    {
        /// <summary>
        /// SSH v1
        /// </summary>
        [ParameterAttribute("1")]
        V1,
        /// <summary>
        /// SSH v2
        /// </summary>
        [ParameterAttribute("2")]
        V2
    }
}
