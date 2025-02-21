using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPassiveSkill
{
	public void OnPassive();
	public void OffPassive();
}

public interface IActiveSkill
{
	public void Action();
}


public class Skill : MonoBehaviour
{
	public string name;
	public string desc;
}

