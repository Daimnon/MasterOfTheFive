using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public enum GameMode { Duel, Brawl, TeamFight}

public class PlayerController : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    #region Data Reference
    [Header("Data Reference")]
    [SerializeField] private PlayerData _myData;
    #endregion

    #region Indicators
    private GameObject _currentTarget;
    public GameObject CurrentTarget { set => _currentTarget = value; }

    private Button _endPhaseBtn;

    private int _playerId = 0;
    public int PlayerID { get => _playerId; set => _playerId = value; }

    private bool _playerReady = false;
    private bool _isMyTurn, _isPhaseDone, _isNegating, _isOnStandby, _isOnDraw, _isOnAction, _isOnNegate, _isOnReaction, _isOnEnd, _tryAction;
    public bool IsMyTurn { get => _isMyTurn; set => _isMyTurn = value; }
    public bool IsNegating => _isNegating;
    #endregion

    #region State Machine
    private delegate void State();
    private State _currentState;
    #endregion

    #region Monobehavior Callbacks
    private void Start()
    {
        
        _currentState = StandbyPhase;

        //_endPhaseBtn = _myData.PlayerUI.transform.GetChild(4).GetComponent<Button>();
        //_endPhaseBtn.onClick.AddListener(ChangePhase);
    }

    private void Update()
    {
        _playerId = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log($"Turn Start: {name}");
        _currentState.Invoke();

        // if (_opponePlayerController.TryAction)
        // {
        //      _isNegating = true;
        //      _currentState = NegationPhase;
        // }
    }
    #endregion

    #region States

    private void StandbyPhase()
    {
        Debug.Log($"{name} inisiated: Standby Phase");

        // not active
        _isOnEnd = false;
        _isOnStandby = true;

        if (GameManager.Instance.PlayersInRoom == 2 && _myData.CurrentGameMode == GameMode.Duel && !_playerReady)
        {
            _currentState = DuelInitializationPhase;
        }

        if (_isMyTurn && _myData.CurrentGameMode == GameMode.Duel && _playerReady)
        {
            _currentState = DrawPhase;
        }
    }

    private void DuelInitializationPhase()
    {
        Debug.Log($"{name} inisiated: Initialization Phase");

        _myData.InitializePlayersDuelComponents();
        _endPhaseBtn = _myData.PlayerUI.transform.GetChild(4).GetComponent<Button>();
        _endPhaseBtn.onClick.AddListener(ChangePhase);
        _playerReady = true;

        _currentState = StandbyPhase;
    }

    private void DrawPhase()
    {
        Debug.Log($"{name} inisiated: Draw Phase");

        _isOnStandby = false;
        _isOnDraw = true;

        _myData.Deck.PhotonView.RPC("DrawCard", RpcTarget.All);
        _currentState = ActionPhase;
    }

    private void ActionPhase()
    {
        Debug.Log($"{name} inisiated: Action Phase");

        _isOnDraw = false;
        _isOnAction = true;
        // use card from hand to the battlefield

        // if (PlacedCardOnBattlefield())
        // {
        //     _tryAction = true;
        // }

        if (_isPhaseDone)
        {
            _isPhaseDone = false;
            _currentState = ReactionPhase;
        }
    }

    private void NegatePhase()
    {
        Debug.Log($"{name} inisiated: Negation Phase");

        if (_isNegating)
        {
            // if (requirments Met)
            // {
            //      negation phase action
            //      _currentState = NegatePhase;
            // }
            // else
            // {
            //      _isNegating = false;
            //      _currentState = NegatePhase;
            // }
        }
        else
        {
            if (_isMyTurn)
            {
                _currentState = ReactionPhase;
            }
            else
            {
                _currentState = StandbyPhase;
            }

        }
    }

    private void ReactionPhase()
    {
        Debug.Log($"{name} inisiated: Reaction Phase");

        _isOnAction = false;
        _isOnReaction = true;
        // if not negated play action effect

        _currentState = EndPhase;
    }

    private void EndPhase()
    {
        Debug.Log($"{name} inisiated: End Phase");

        _isOnReaction = false;
        _isOnEnd = true;

        _isMyTurn = false;
        _isPhaseDone = false;
        _currentState = StandbyPhase;
    }
    #endregion

    #region UI Events
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Set Target");
        eventData.pointerEnter = _currentTarget;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _currentTarget = null;
    }
    #endregion

    #region Methods
    public void ChangePhase()
    {
        if (!_isPhaseDone)
        {
            _isPhaseDone = true;
        }

        Debug.Log("Changed Phase");
    }
    #endregion
}
