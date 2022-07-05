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

    //private EventHandler _playerEventHandler;
    //public EventHandler PlayerEventHandler { get => _playerEventHandler; set => _playerEventHandler = value; }

    [Header("AspectList")]
    public List<AspectData> CardsInField;

    [Header("CurrentAspects")]
    public Aspect LastCardInBattlefield;
    public AspectData LastCardDataInBattlefield;

    public PointerEventData ClickEventData;

    private int ifFiveIWin = 0;
    private bool didIWin = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Aspect currentCard = eventData.pointerDrag.GetComponent<Aspect>();

        LastCardInBattlefield = currentCard;
        LastCardDataInBattlefield = currentCard.GetComponent<AspectDisplayData>().CardData;

        if (LastCardInBattlefield != null)
        {
            LastCardInBattlefield.ParentToReturnPlaceholder = transform;
            LastCardInBattlefield.IsCardInHand = false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (LastCardInBattlefield != null)
        {
            LastCardInBattlefield.ParentToReturn = transform;
            LastCardInBattlefield.IsCardInHand = false;
            BattlefieldPlaceCard(LastCardInBattlefield);
            LastCardInBattlefield = null;
            LastCardDataInBattlefield = null;

            Debug.Log("card Placed");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        if (LastCardInBattlefield != null && LastCardInBattlefield.ParentToReturnPlaceholder == transform)
        {
            LastCardInBattlefield.ParentToReturnPlaceholder = transform;
            LastCardInBattlefield.IsCardInHand = false;
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

    [PunRPC]
    private void BattlefieldPlaceCard(Aspect currentTarget)
    {
        //get current card
        AspectData cardToField = currentTarget.gameObject.GetComponent<AspectDisplayData>().CardData;

        //add current card to battlefield
        _playerData.Battlefield.CardsInField.Add(cardToField);

        //check if works
        Debug.Log(cardToField.Name);

        //remove placed cards from hand
        _playerData.Hand.CardsInHand.Remove(cardToField);

        Action(cardToField);

        currentTarget.IsOnBattlefield = true;

        // get last placed card on field
        _playerData.LastAspectPlacedOnBattelfield = currentTarget.gameObject;

        // addintional code here ----- V

        for (int i = 0; i < _playerData.Battlefield.CardsInField.Count; i++)
        {
            if (_playerData.Battlefield.CardsInField[i].PrimodialPower == PowerType.Light)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _playerData.Battlefield.CardsInField.Count; i++)
        {
            if (_playerData.Battlefield.CardsInField[i].PrimodialPower == PowerType.Death)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _playerData.Battlefield.CardsInField.Count; i++)
        {
            if (_playerData.Battlefield.CardsInField[i].PrimodialPower == PowerType.Control)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _playerData.Battlefield.CardsInField.Count; i++)
        {
            if (_playerData.Battlefield.CardsInField[i].PrimodialPower == PowerType.Destruction)
            {
                ifFiveIWin++;
                break;
            }
        }

        for (int i = 0; i < _playerData.Battlefield.CardsInField.Count; i++)
        {
            if (_playerData.Battlefield.CardsInField[i].PrimodialPower == PowerType.Life)
            {
                ifFiveIWin++;
                break;
            }
        }

        if (ifFiveIWin < 5)
        {
            Debug.Log("Counted " + ifFiveIWin);
            ifFiveIWin = 0;
        }
        else
        {
            didIWin = true;
        }
    }

    public void Action(AspectData card)
    {
        if (card is LightAspect)
            (card as LightAspect).Action(_playerData);
        else if (card is DeathAspect)
            (card as DeathAspect).Action(_playerData);
        else if (card is DestructionAspect)
            (card as DestructionAspect).Action(_playerData);
        else if (card is LifeAspect)
            (card as LifeAspect).Action(_playerData);
        else if (card is ControlAspect)
            (card as ControlAspect).Action(_playerData);
    }

    public void SupremeAction(AspectData card)
    {
        if (card is LightAspect)
            (card as LightAspect).SupremeAction();
        else if (card is DeathAspect)
            (card as DeathAspect).SupremeAction();
        else if (card is DestructionAspect)
            (card as DestructionAspect).SupremeAction();
        else if (card is LifeAspect)
            (card as LifeAspect).SupremeAction();
        else if (card is ControlAspect)
            (card as ControlAspect).SupremeAction();
    }

    public void SecondaryAction(AspectData card)
    {
        if (card is LightAspect)
            (card as LightAspect).SecondaryAction();
        else if (card is DeathAspect)
            (card as DeathAspect).SecondaryAction();
        else if (card is DestructionAspect)
            (card as DestructionAspect).SecondaryAction();
        else if (card is LifeAspect)
            (card as LifeAspect).SecondaryAction();
        else if (card is ControlAspect)
            (card as ControlAspect).SecondaryAction();
    }
}
