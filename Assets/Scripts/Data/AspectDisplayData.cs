using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class AspectDisplayData : MonoBehaviour
{
	public AspectData CardData;
	public Image ArtworkImage, CardTemplate;
    public string NameText = "Name auto-generated";

	// Use this for initialization
	void Start()
	{
		ArtworkImage.sprite = CardData.Artwork;

        switch (CardData.PrimodialPower)
        {
            case PowerType.Light:
                NameText = "Light";
                break;

            case PowerType.Death:
                NameText = "Death";
                break;

            case PowerType.Destruction:
                NameText = "Destruction";
                break;

            case PowerType.Life:
                NameText = "Life";
                break;

            case PowerType.Control:
                NameText = "Control";
                break;

            default:
                CardTemplate.color = Color.white;
                break;
        }

        NameText = CardData.Name;
    }
}
