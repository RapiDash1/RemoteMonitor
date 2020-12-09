namespace RemoteMonitor.Usage
{
    public interface ResourceUsage
    {
        /// <summary>
        /// Get Current Total Resource Usage
        /// </summary>
        /// <returns>
        /// A floating point number containing current resource usage
        /// </returns>
        float Current();
    }   
}