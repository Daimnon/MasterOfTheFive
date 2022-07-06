using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "Light Card", menuName = "Old Card/Light")]
public class LightAspect : AspectData
{
	public LightAspect()
	{
		PrimodialPower = PowerType.Light;
	}

	public void Action(PlayerData _playerData)
	{
		Debug.Log("Perform Light Ability");
		//_playerData.Deck.DrawCard();
		_playerData.Deck.PhotonView.RPC("DrawCard", RpcTarget.All);
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
