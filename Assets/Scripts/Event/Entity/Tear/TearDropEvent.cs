public class TearDropEvent : Event
{
    public BaseTear tear;

    public TearDropEvent(BaseTear tear)
    {
        this.tear = tear;
    }
}