using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PlayerData : MonoBehaviour
{
    #region Photon
    [Header("Photon")]
    [SerializeField] private PhotonView _photonView;
    public PhotonView PhotonView => _photonView;

    private Player[] _playersInRoom;
    public Player[] PlayersInRoom => _playersInRoom;
    #endregion

    #region Game Components
    private GameMode _currentGameMode;
    [SerializeField] private EventHandler _playerEventHandler;

    private Hand _hand;
    private Battlefield _battlefield;
    private Deck _deck;
    private Tomb _tomb;

    public GameMode CurrentGameMode => _currentGameMode;
    public Hand Hand => _hand;
    public Battlefield Battlefield => _battlefield;
    public Deck Deck => _deck;
    public Tomb Tomb => _tomb;
    #endregion

    #region GameObjects References
    private GameObject _gameCanvas, _playerUI, _sacrificeOverlay;
    private GameObject _handGO, _battlefieldGO, _deckGO, _tombGO;
    private GameObject _lastAspectPlacedOnBattelfield;

    public GameObject GameCanvas => _gameCanvas;
    public GameObject PlayerUI => _playerUI;
    public GameObject SacrificeOverlay => _sacrificeOverlay;
    public GameObject HandGO => _handGO;
    public GameObject BattlefieldGO => _battlefieldGO;
    public GameObject DeckGO => _deckGO;
    public GameObject TombGO => _tombGO;
    public GameObject LastAspectPlacedOnBattelfield { get => _lastAspectPlacedOnBattelfield; set => _lastAspectPlacedOnBattelfield = value; }
    #endregion

    public bool IsReviving = false;
    public bool IsSacrificing = false;
    public bool IsDestroying = false;
    //public bool IsDrawingWithLight = false;

    #region Monobehavior Callbacks
    private void Start()
    {
        //PhotonNetwork.LocalPlayer.NickName = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
        GameManager.Instance.PlayerList.Add(this);
        InitializeMyComponents();
    }

    private void Update()
    {
        // Update PlayerList as long as not all of the expected players are logged in
        if (PhotonNetwork.PlayerList.Length < 4)
        {
            if (_currentGameMode != GameMode.Duel)
            {
                _playersInRoom = PhotonNetwork.PlayerList;
            }
            else if (PhotonNetwork.PlayerList.Length < 2)
            {
                _playersInRoom = PhotonNetwork.PlayerList;
            }
        }
    }
    #endregion

    #region Methods
    private void InitializeMyComponents()
    {
        _gameCanvas = GameObject.Find("Game Canvas");

        // Set PlayerUI & SacrificeOverlay by ActorNumber
        if (_photonView.OwnerActorNr == 1)
        {
            _playerUI = _gameCanvas.transform.GetChild(0).gameObject;
            _sacrificeOverlay = _gameCanvas.transform.GetChild(3).gameObject;
        }
        else if (_photonView.OwnerActorNr != 1)
        {
            _playerUI = _gameCanvas.transform.GetChild(1).gameObject;
            _sacrificeOverlay = _gameCanvas.transform.GetChild(4).gameObject;
        }

        // Set GameObjects
        _handGO = _playerUI.transform.GetChild(0).gameObject;
        _battlefieldGO = _playerUI.transform.GetChild(1).gameObject;
        _deckGO = _playerUI.transform.GetChild(2).gameObject;
        _tombGO = _playerUI.transform.GetChild(3).gameObject;

        // Set Scripts
        _hand = _handGO.GetComponent<Hand>();
        _battlefield = _battlefieldGO.GetComponent<Battlefield>();
        _deck = _deckGO.GetComponent<Deck>();
        _tomb = _tombGO.GetComponent<Tomb>();

        // Set PlayerData
        _hand.PlayerData = this;
        _battlefield.PlayerData = this;
        _deck.PlayerData = this;
        _tomb.PlayerData = this;

        // Set PlayerEventHandler
        _battlefield.PlayerEventHandler = _playerEventHandler;
        _tomb.PlayerEventHandler = _playerEventHandler;

        return;
    }

    public void InitializePlayersDuelComponents()
    {
        _gameCanvas = GameObject.Find("Game Canvas");

        // Set PlayerUI & SacrificeOverlay by ActorNumber
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            _playerUI = _gameCanvas.transform.GetChild(0).gameObject;
            _sacrificeOverlay = _gameCanvas.transform.GetChild(3).gameObject;
        }
        else if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            _playerUI = _gameCanvas.transform.GetChild(1).gameObject;
            _sacrificeOverlay = _gameCanvas.transform.GetChild(4).gameObject;
        }

        // Set GameObjects
        _handGO = _playerUI.transform.GetChild(0).gameObject;
        _battlefieldGO = _playerUI.transform.GetChild(1).gameObject;
        _deckGO = _playerUI.transform.GetChild(2).gameObject;
        _tombGO = _playerUI.transform.GetChild(3).gameObject;

        // Set Scripts
        _hand = _handGO.GetComponent<Hand>();
        _battlefield = _battlefieldGO.GetComponent<Battlefield>();
        _deck = _deckGO.GetComponent<Deck>();
        _tomb = _tombGO.GetComponent<Tomb>();

        // Set PlayerData
        _hand.PlayerData = this;
        _battlefield.PlayerData = this;
        _deck.PlayerData = this;
        _tomb.PlayerData = this;

        // Set PlayerEventHandler
        _battlefield.PlayerEventHandler = _playerEventHandler;
        _tomb.PlayerEventHandler = _playerEventHandler;

        return;
    }
    #endregion
}