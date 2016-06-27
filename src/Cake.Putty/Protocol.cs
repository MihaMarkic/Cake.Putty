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
        [Parameter("sftp")]
        Sftp,
        /// <summary>
        /// SCP
        /// </summary>
        [Parameter("scp")]
        Scp
    }
}
