using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnManager : MonoBehaviour
{
    #region Prefab references
    [SerializeField] private GameObject _player;
    #endregion

    private void Awake()
    {
        GameObject player = PhotonNetwork.Instantiate(_player.name, Vector3.zero, Quaternion.identity);
        player.name = $"Player {PhotonNetwork.LocalPlayer.ActorNumber}";
        Debug.Log($"{player.name}");
    }

    void Start()
    {
        
    }
}
