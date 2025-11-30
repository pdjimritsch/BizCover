    namespace BizCover.Cars.Network.Abstraction
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISoapRequest<T> where T : class, new()
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        ISoapHeader? Header { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ISoapBody<T>? Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ISoapTrailer? Trailer { get; set; }

        #endregion
    }
}
