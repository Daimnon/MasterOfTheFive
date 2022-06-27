using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _roomsListPanel;
    [SerializeField] private TextMeshProUGUI roomList;
    [SerializeField] private TMP_InputField roomToCreate;
    private int _roomCounter = 0;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "V.0.5";
    }

    public void UIJoinRoom()
    {
        string roomName = roomToCreate.text;
        PhotonNetwork.JoinRoom(roomName);
    }

    public void UICreateRoom()
    {
        string roomName = roomToCreate.text;
        PhotonNetwork.CreateRoom(roomName);
        roomList.text += roomName + System.Environment.NewLine;
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("connected to master");

        //PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("joined lobby");
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("room was crearted");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("room creation was failed: " + returnCode + "," + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //get list of available rooms

        base.OnRoomListUpdate(roomList);
        foreach (RoomInfo ri in roomList)
        {
            Debug.Log(ri.Name);
        }

        //todo:
        //update the room list in the UI
    }
}