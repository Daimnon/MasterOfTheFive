using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Light Card", menuName = "Old Card/Light")]
public class LightAspect : AspectData
{
	public LightAspect()
	{
		PrimodialPower = PowerType.Light;
	}

	public void Action(EventHandler eventHandler)
	{
		eventHandler.DrawCard();
	}

	public void SupremeAction()
	{
		Debug.Log("Do Light Supreme Action");
	}

	public void SecondaryAction()
	{
		Debug.Log("Do Light Secondary Action");
	}
}
