namespace Cake.Putty
{
    /// <summary>
    /// PLink protocol.
    /// </summary>
    public enum PlinkProtocol
    {
        /// <summary>
        /// SSH
        /// </summary>
        [Parameter("ssh")]
        Ssh,
        /// <summary>
        /// Telnet
        /// </summary>
        [Parameter("telnet")]
        Telnet,
        /// <summary>
        /// RLogin
        /// </summary>
        [Parameter("rlogin")]
        RLogin,
        /// <summary>
        /// Raw
        /// </summary>
        [Parameter("raw")]
        Raw,
        /// <summary>
        /// Serial
        /// </summary>
        [Parameter("serial")]
        Serial
    }
}
