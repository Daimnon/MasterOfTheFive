using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destruction Card", menuName = "Old Card/Destruction")]
public class DestructionAspect : AspectData
{
	public DestructionAspect()
	{
		PrimodialPower = PowerType.Destruction;
	}

	public void Action(PlayerData playerData)
	{
		Debug.Log("Perform Destruction Ability");

		// fix needed
		playerData.IsDestroying = true;
	}

	public void SupremeAction()
	{
		Debug.Log("Do Destruction Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Destruction Secondary Action");
	}
}
