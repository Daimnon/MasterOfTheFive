using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Deck : MonoBehaviour
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView { get => _photonView; set => _photonView = value; }
    #endregion

    #region Data References
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; set => _playerData = value; }

    [Header("AspectList")]
    [SerializeField] private List<AspectData> _deckList = new List<AspectData>(25);
    [SerializeField] public List<AspectData> DeckList => _deckList;

    [Header("AspectPrefab")]
    [SerializeField] private GameObject _aspectPrefab;
    [SerializeField] private GameObject _aspectBackPrefab;

    [Header("AspectData")]
    [SerializeField] private AspectData _lightAspectData;
    [SerializeField] private AspectData _deathAspectData, _destructionAspectData, _lifeAspectData, _controlAspectData;
    #endregion

    private int _maxDeckSize = 25, _currentDeckSize;
    public int CurrentDeckSize { get => _currentDeckSize; set => _currentDeckSize = value; }

    private void Start()
    {
        // empty list
        _deckList.Clear();

        // Initialize _deckList
        for (int i = 0; i < 5; i++)
        {
            _deckList.Add(_lightAspectData);
            _deckList.Add(_deathAspectData);
            _deckList.Add(_destructionAspectData);
            _deckList.Add(_lifeAspectData);
            _deckList.Add(_controlAspectData);
        }

        // randomize _aspectsInDeck list
        for (int i = 0; i < _deckList.Count; i++)
        {
            AspectData temp = _deckList[i];
            int randomIndex = UnityEngine.Random.Range(i, _deckList.Count);
            _deckList[i] = _deckList[randomIndex];
            _deckList[randomIndex] = temp;
        }
    }

    private void Update()
    {
        if (_currentDeckSize > _maxDeckSize)
        {
            _currentDeckSize = _maxDeckSize;
        }
    }

    [PunRPC]
    private void InitializeGame()
    {
        // Get the top 4 cards in deck
        List<AspectData> cardsToHand = _deckList.GetRange(0, 4);

        // Add said cards to hand
        _playerData.Hand.CardsInHand.AddRange(cardsToHand);

        // loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (AspectData card in cardsToHand)
        {
            _aspectPrefab.GetComponent<AspectDisplayData>().CardData = card;
            Instantiate(_aspectPrefab, _playerData.Hand.transform);
        }

        // Remove drawn cards from deck
        _deckList.RemoveRange(0, 4);
        _currentDeckSize -= 4;
    }

    [PunRPC]
    private void InitializeGameShowCase()
    {
        List<AspectData> cardsToHand = new List<AspectData>(4);

        // Get specific starting hand on InitializeGameShowCase
        for (int i = 0; i < _deckList.Count; i++)
        {
            // Get Light Aspect
            if (_deckList[i].Name == "Light" && !cardsToHand.Contains(_deckList[i]))
            {
                cardsToHand.Add(_deckList[i]);
                continue;
            }

            // Get Life Aspect
            if (_deckList[i].Name == "Life" && !cardsToHand.Contains(_deckList[i]))
            {
                cardsToHand.Add(_deckList[i]);
                continue;
            }

            // Get Death Aspect
            if (_deckList[i].Name == "Death" && !cardsToHand.Contains(_deckList[i]))
            {
                cardsToHand.Add(_deckList[i]);
                continue;
            }

            // Get Control Aspect
            if (_deckList[i].Name == "Control" && !cardsToHand.Contains(_deckList[i]))
            {
                cardsToHand.Add(_deckList[i]);
                break;
            }
        }

        // Add Aspects to hand
        _playerData.Hand.CardsInHand = cardsToHand;

        // loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (AspectData card in cardsToHand)
        {
            _aspectPrefab.GetComponent<AspectDisplayData>().CardData = card;
            Instantiate(_aspectPrefab, _playerData.Hand.transform);

            //check if works (update: it does)
            print(card.Name);
        }

        // Remove drawn cards from deck
        _deckList.RemoveRange(0, 4);
        _currentDeckSize -= 4;
    }

    public void DrawCard()
    {
        if (_photonView.IsMine)
        {
            Debug.Log("I draw card");

            //get top card in deck & adds it to the hand
            _playerData.Hand.CardsInHand.Add(_deckList[0]);

            //reads said card data and creates a prefab based on that data in the hand
            _aspectPrefab.GetComponent<AspectDisplayData>().CardData = _deckList[0];
            GameObject aspectToHand = PhotonNetwork.Instantiate(_aspectPrefab.name, Vector2.zero, Quaternion.identity);
            aspectToHand.transform.parent = _playerData.Hand.transform;

            //check if works (update: it does)
            Debug.Log(_deckList[0].Name);

            _deckList.RemoveAt(0);
            _currentDeckSize--;
        }

        _photonView.RPC("DrawCardRPC", RpcTarget.Others);
    }

    [PunRPC]
    private void DrawCardRPC()
    {
        if (!_photonView.IsMine)
        {
            Debug.Log("I draw card");
            PlayerData opponentData = _playerData.gameObject.GetComponent<PlayerController>().OpponentData;

            GameObject aspectToHand = PhotonNetwork.Instantiate(_aspectBackPrefab.name, Vector2.zero, Quaternion.identity);
            aspectToHand.transform.parent = opponentData.Hand.transform;
        }
    }

    [PunRPC]
    private void DrawTwo()
    {
        //get top 2 cards in deck
        List<AspectData> cardsToHand = _deckList.GetRange(0, 2);

        //add said cards to hand
        _playerData.Hand.CardsInHand.AddRange(cardsToHand);

        //loops through said cards's data, reads it and creates a prefab based on that data in the hand
        foreach (AspectData card in cardsToHand)
        {
            _aspectPrefab.GetComponent<AspectDisplayData>().CardData = card;
            Instantiate(_aspectPrefab, _playerData.Hand.transform);

            //check if works (update: it does)
            Debug.Log(card.Name);
        }

        //remove drawn cards from deck
        _deckList.RemoveRange(0, 2);
        _currentDeckSize -= 2;
    }
}
