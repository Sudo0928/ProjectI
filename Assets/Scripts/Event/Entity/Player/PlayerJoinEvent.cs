using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoinEvent : Event
{
    public string PlayerName { get; }

    public PlayerJoinEvent(string playerName)
    {
        PlayerName = playerName;
    }
}
