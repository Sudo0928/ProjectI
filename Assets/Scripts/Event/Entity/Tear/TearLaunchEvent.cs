public class TearLaunchEvent : Event
{
    public BaseTear tear;

    public TearLaunchEvent(BaseTear tear)
    {
        this.tear = tear;
    }
}