namespace Cake.Putty
{
    /// <summary>
    /// Transfer protocol.
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// SFTP
        /// </summary>
        [ParameterAttribute("sftp")]
        Sftp,
        /// <summary>
        /// SCP
        /// </summary>
        [ParameterAttribute("scp")]
        Scp
    }
}
