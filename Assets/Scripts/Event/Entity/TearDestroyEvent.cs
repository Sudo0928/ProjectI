public class TearDestroyEvent : Event
{
    public Tear tear;

    public TearDestroyEvent(Tear tear)
    {
        this.tear = tear;
    }
}