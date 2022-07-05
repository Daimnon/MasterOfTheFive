using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Death Card", menuName = "Old Card/Death")]
public class DeathAspect : AspectData {

	public DeathAspect()
	{
		PrimodialPower = PowerType.Death;
	}

	public void Action(PlayerData playerData)
	{
		Debug.Log("Perform Death Ability");

		playerData.IsSacrificing = true;
		playerData.SacrificeOverlay.SetActive(true);
	}

	public void SupremeAction()
	{
		Debug.Log("Do Death Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Death Secondary Action");
	}
}
