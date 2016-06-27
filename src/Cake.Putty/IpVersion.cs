using Cake.Putty;

namespace Cake.Putty
{
    /// <summary>
    /// IP version
    /// </summary>
    public enum IpVersion
    {
        /// <summary>
        /// IPv4
        /// </summary>
        [ParameterAttribute("4")]
        V4,
        /// <summary>
        /// IPv6
        /// </summary>
        [ParameterAttribute("6")]
        V6
    }
}
