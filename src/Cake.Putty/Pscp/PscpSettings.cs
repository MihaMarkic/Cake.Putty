using Cake.Core.IO;

namespace Cake.Putty
{
    /// <summary>
    /// Settings for Pscp build.
    /// </summary>
    public sealed class PscpSettings: AutoToolSettings
    {
        /// <summary>
        /// Preserve file attributes.
        /// </summary>
        [Parameter("p")]
        public bool PreserveFiltAttributes { get; set; }
        /// <summary>
        /// Copy directories recursively.
        /// </summary>
        [Parameter("r")]
        public bool CopyDirectoriesRecursively { get; set; }
        /// <summary>
        /// Load settings from saved session.
        /// </summary>
        [Parameter("load")]
        public string LoadSettingsFromSavedSession { get; set; }
        /// <summary>
        /// Connect to specified port.
        /// </summary>
        [Parameter("P")]
        public int Port { get; set; }
        /// <summary>
        /// Connect with specified username.
        /// </summary>
        [Parameter("l")]
        public string User { get; set; }
        /// <summary>
        /// Login with specified password.
        /// </summary>
        [Parameter("pw")]
        public string Password { get; set; }
        /// <summary>
        /// Force use of particular SSH protocol version.
        /// </summary>
        public SshVersion? SshVersion { get; set; }
        /// <summary>
        /// Force use of IPv4 or IPv6.
        /// </summary>
        public IpVersion? IpVersion { get; set; }
        /// <summary>
        /// Enable compression.
        /// </summary>
        [Parameter("C")]
        public bool Compression { get; set; }
        /// <summary>
        /// Private key file for user authentication.
        /// </summary>
        [Parameter("i")]
        public FilePath KeyFileForUserAuthentication { get; set; }
        /// <summary>
        /// Enables or disabled use of Pageant.
        /// </summary>
        [BoolParameterAttribute("agent", "noagent")]
        public bool? EnablePageant { get; set; }
        /// <summary>
        /// Manually specify a host key (may be repeated).
        /// </summary>
        [Parameter("hostkey")]
        public string Hostkey { get; set; }
        /// <summary>
        /// Disable all interactive prompts.
        /// </summary>
        [Parameter("batch")]
        public bool Batch { get; set; }
        /// <summary>
        /// Allow server-side wildcards (DANGEROUS).
        /// </summary>
        [Parameter("unsafe")]
        public bool Unsafe { get; set; }
        /// <summary>
        /// Force use of SFTP or SCP protocol.
        /// </summary>
        public Protocol? Protocol { get; set; }
    }
}
