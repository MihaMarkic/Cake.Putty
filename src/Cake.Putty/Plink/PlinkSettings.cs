using Cake.Core.IO;

namespace Cake.Putty
{
    /// <summary>
    /// Settings for Plink build.
    /// </summary>
    public sealed class PlinkSettings : AutoToolSettings
    {
        /// <summary>
        /// configuration-string (e.g. 19200,8,n,1,X)
        /// </summary>
        /// <remarks>
        /// Specify the serial configuration(serial only.
        /// </remarks>
        [Parameter("sercfg")]
        public string ConfigurationString { get; set; }
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
        /// [listen-IP:]listen-port
        /// Dynamic SOCKS-based port forwarding.
        /// </summary>
        [Parameter("D")]
        public string DynamicPortForwarding { get; set; }
        /// <summary>
        /// [listen-IP:]listen-port:host:port
        /// Forward local port to remote address.
        /// </summary>
        [Parameter("L")]
        public string LocalPortForwarding { get; set; }
        /// <summary>
        /// [listen-IP:]listen-port:host:port
        /// Forward remote port to local address.
        /// </summary>
        [Parameter("R")]
        public string RemotePortForwarding { get; set; }
        /// <summary>
        /// Enable / disable X11 forwarding.
        /// </summary>
        [BoolParameter("X", "x")]
        public bool? X11Forwarding { get; set; }
        /// <summary>
        /// Enable / disable agent forwarding.
        /// </summary>
        [BoolParameter("A", "a")]
        public bool? AgentForwarding { get; set; }
        /// <summary>
        /// Enable / disable pty allocation.
        /// </summary>
        [BoolParameter("T", "t")]
        public bool? PtyAllocation { get; set; }
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
        [BoolParameter("agent", "noagent")]
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
        /// Force use of a particular protocol.
        /// </summary>
        public PlinkProtocol? Protocol { get; set; }
        /// <summary>
        /// Remote command is an SSH subsystem (SSH-2 only).
        /// </summary>
        [Parameter("m")]
        public string RemoteCommandsFile { get; set; }
        /// <summary>
        /// Don't start a shell/command (SSH-2 only).
        /// </summary>
        [Parameter("N")]
        public bool DontStartShellCommand { get; set; }
        /// <summary>
        /// host:port
        /// Open tunnel in place of session(SSH-2 only).
        /// </summary>
        [Parameter("nc")]
        public string Tunnel { get; set; }
    }
}
