using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

enum AspectOfElement { Light = 0, Death = 1, Destruction = 3, Life = 4, Control = 5 }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //#region Photon
    //[SerializeField] private PhotonView _photonView;
    //#endregion

    public List<PlayerData> PlayerList;
    public int PlayersInRoom = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        PlayersInRoom = PlayerList.Count;
    }

    private void Start()
    {
        //_gameCanvas.position = Vector2.zero;
        //Duel();
    }

    private void Duel()
    {
        //InitializeDuel();
    }

    private void Brawl()
    {
        // Run [1 vs 1 vs 1] & [1 vs 1 vs 1 vs 1] envirnoments & rules
    }

    private void TeamFight()
    {
        // Run [2 vs 2] envirnoment & rules
    }

    public void ForceTurn(int actorNumber)
    {
        switch (actorNumber)
        {
            case 1:
                PlayerList[0].GetComponent<PlayerController>().IsMyTurn = true;
                break;

            case 2:
                PlayerList[1].GetComponent<PlayerController>().IsMyTurn = true;
                break;

            default:
                Debug.Log($"Something went wrong with 'ForceTurn'");
                break;
        }
    }
}
