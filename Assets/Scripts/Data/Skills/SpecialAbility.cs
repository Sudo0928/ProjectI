using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbility : MonoBehaviour
{
    public abstract void OnAbility(PlayerController player);
    public abstract void RemoveSkill();
}
