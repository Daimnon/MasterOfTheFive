using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Life Card", menuName = "Old Card/Life")]
public class LifeAspect : AspectData
{
	public LifeAspect()
	{
		PrimodialPower = PowerType.Life;
	}

	public void Action(PlayerData playerData)
	{
		Debug.Log("Perform Life Ability");
		playerData.IsReviving = true;
		playerData.Tomb.SearchTomb();
	}

	public void SupremeAction()
	{
		Debug.Log("Do Life Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Life Secondary Action");
	}
}
