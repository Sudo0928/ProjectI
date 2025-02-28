public class ItemPickUpEvent : Event
{
    PlayerController playerController;

    public ItemPickUpEvent(PlayerController playerController)
    {
        this.playerController = playerController;
    }
}