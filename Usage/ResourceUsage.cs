namespace RemoteMonitor.Usage
{
    public interface ResourceUsage
    {
        string ResourceType();

        float Current();
    }   
}