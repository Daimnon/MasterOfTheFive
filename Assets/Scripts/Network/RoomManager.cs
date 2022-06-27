using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _roomName;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}