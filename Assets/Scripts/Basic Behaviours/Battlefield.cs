using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;

public class Battlefield : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView { get => _photonView; set => _photonView = value; }
    #endregion

    [Header("Data Script")]
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }

    private EventHandler _playerEventHandler;
    public EventHandler PlayerEventHandler { get => _playerEventHandler; set => _playerEventHandler = value; }

    [Header("AspectList")]
    public List<AspectData> CardsInField;

    [Header("CurrentAspects")]
    public Aspect CurrentCardInBattlefield;
    public AspectData CurrentCardDataInBattlefield;

    public PointerEventData ClickEventData;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Aspect currentCard = eventData.pointerDrag.GetComponent<Aspect>();

        CurrentCardInBattlefield = currentCard;
        CurrentCardDataInBattlefield = currentCard.GetComponent<AspectDisplayData>().CardData;

        if (CurrentCardInBattlefield != null)
        {
            CurrentCardInBattlefield.ParentToReturnPlaceholder = transform;
            CurrentCardInBattlefield.IsCardInHand = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (CurrentCardInBattlefield != null)
        {
            CurrentCardInBattlefield.ParentToReturn = transform;
            CurrentCardInBattlefield.IsCardInHand = false;
            _playerEventHandler.BattlefieldPlaceCard(CurrentCardInBattlefield);
            CurrentCardInBattlefield = null;
            CurrentCardDataInBattlefield = null;

            print("card Placed");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        if (CurrentCardInBattlefield != null && CurrentCardInBattlefield.ParentToReturnPlaceholder == transform)
        {
            CurrentCardInBattlefield.ParentToReturnPlaceholder = transform;
            CurrentCardInBattlefield.IsCardInHand = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ClickEventData = eventData;

        if (!_playerData.IsDestroying)
            return;

        // need opponent eventData
        else if (_playerData.IsDestroying)
            _playerData.Tomb.CardToDestroy(eventData);
    }
}
