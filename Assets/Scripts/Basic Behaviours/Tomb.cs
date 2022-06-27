using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class Tomb : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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

    [Header("Game Object References")]
    [SerializeField] private GameObject _tombWindow, _tombWindowContent;

    [Header("AspectList")]
    public List<AspectData> CardsInTomb;

    [Header("CurrentAspects")]
    public Aspect CurrentCardInHand;
    public AspectData CurrentCardDataInTomb;

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void CardToSacrifice(PointerEventData eventData)
    {
        //get current card
        AspectData cardToTomb = eventData.pointerDrag.GetComponent<AspectDisplayData>().CardData;

        //add current card to tomb
        _playerData.Tomb.CardsInTomb.Add(cardToTomb);

        //check if works
        print(cardToTomb.Name);

        //remove placed cards from hand
        _playerData.Hand.CardsInHand.Remove(cardToTomb);

        eventData.pointerDrag.transform.SetParent(_tombWindowContent.transform);
        eventData.pointerDrag.AddComponent<Button>();
        Button cardBtn = eventData.pointerDrag.GetComponent<Button>();
        cardBtn.onClick.AddListener(Revive);

        _playerData.IsSacrificing = false;
        _playerData.SacrificeOverlay.SetActive(false);


    }

    public void CardToDestroy(PointerEventData eventData)
    {
        //_myEventHandler.TargetLine.SetPosition(0, _myDataHandler.LastPlacedCardOnBattelfield.transform.position);
        //_myEventHandler.TargetLine.SetPosition(1, eventData.position);
        print("Tried drawing line");
        //get current card
        AspectData cardToTomb = eventData.pointerDrag.GetComponent<AspectDisplayData>().CardData;

        //add current card to tomb
        _playerData.Tomb.CardsInTomb.Add(cardToTomb);
        //_opponentDataHandler.Tomb.CardsInTomb.Add(cardToTomb);

        //check if works
        print(cardToTomb.Name);

        //remove placed cards from battlefield
        _playerData.Battlefield.CardsInField.Remove(cardToTomb);
        //_opponentDataHandler.Battlefield.CardsInField.Remove(cardToTomb);

        eventData.pointerDrag.transform.SetParent(_tombWindowContent.transform);
        eventData.pointerDrag.AddComponent<Button>();
        Button cardBtn = eventData.pointerDrag.GetComponent<Button>();
        cardBtn.onClick.AddListener(Revive);


        _playerData.IsDestroying = false;
    }

    public void Revive()
    {
        Debug.Log($"Attemting Revive: {EventSystem.current.currentSelectedGameObject.name}");

        if (_playerData.IsReviving)
        {
            GameObject currentCard = EventSystem.current.currentSelectedGameObject;
            AspectData cardToHand = currentCard.GetComponent<AspectDisplayData>().CardData;
            currentCard.transform.SetParent(_playerData.Hand.transform);
            _playerData.Hand.CardsInHand.Add(cardToHand);
            CardsInTomb.Remove(cardToHand);
            Destroy(currentCard.GetComponent<Button>());

            _playerData.IsReviving = false;
            CloseSearchTomb();

            Debug.Log($"Revived: {cardToHand.name}");
            return;
        }
    }

    public void SearchTomb()
    {
        _tombWindow.SetActive(true);
    }

    public void CloseSearchTomb()
    {
        _tombWindow.SetActive(false);
    }
}
