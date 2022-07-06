using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;

public class Hand : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView { get => _photonView; set => _photonView = value; }
    #endregion

    [Header("Data Script")]
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }

    [Header("AspectList")]
    public List<AspectData> CardsInHand;

    [Header("CurrentAspects")]
    public Aspect CurrentCardInHand;
    public AspectData CurrentCardDataInHand;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        // this calls the OnPointerEnter of the player controller scripts sucsessfully
        // Need to change all pointer event's to occur on player controller (the opposite of what's happening here)
        _playerData.GetComponent<PlayerController>().OnPointerEnter(eventData);

        Aspect currentCard = eventData.pointerDrag.GetComponent<Aspect>();

        CurrentCardInHand = currentCard;
        CurrentCardDataInHand = currentCard.GetComponent<AspectDisplayData>().CardData;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_playerData.IsSacrificing)
            return;

        else if (_playerData.IsSacrificing)
            _playerData.Tomb.CardToSacrifice(eventData);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Debug.Log("card Placed");

        if (CurrentCardInHand.IsCardInHand)
            CurrentCardInHand.ParentToReturn = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
