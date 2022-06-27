using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviourPun
{
    private PhotonView _photonView;
    private PhotonView _photonView2;
    private Camera _playerCam;

    // Start is called before the first frame update
    void Start()
    {
        _photonView = gameObject.GetPhotonView();
        _photonView2 = GetComponent<PhotonView>();
        _playerCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_photonView.IsMine)
            return;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * 0.1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * 0.1f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * 0.1f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * 0.1f);
        }
    }
}