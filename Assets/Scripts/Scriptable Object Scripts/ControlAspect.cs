using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Control Card", menuName = "Old Card/Control")]
public class ControlAspect : AspectData
{
	public ControlAspect()
    {
		PrimodialPower = PowerType.Control;
	}

	public void Action(PlayerData playerData)
	{
		Debug.Log("Preform Control Ability");
	}

	public void SupremeAction()
	{
		Debug.Log("Do Control Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Control Secondary Action");
	}
}
