public class AddItemEvent : Event
{
    public PlayerControl player;
    public int itemID;

    public AddItemEvent(PlayerControl player, int itemID)
    {
        this.player = player;
        this.itemID = itemID;
    }
}