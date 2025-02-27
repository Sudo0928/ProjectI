public class TearDestroyEvent : Event
{
    public BaseTear tear;

    public TearDestroyEvent(BaseTear tear)
    {
        this.tear = tear;
    }
}