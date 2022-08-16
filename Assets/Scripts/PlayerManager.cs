using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerManager : MonoBehaviour
{

    PhotonView PV;
    // Start is called before the first frame update
    void Awake()
    {
        PV = GetComponent<PhotonView>();
        PV.TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
        Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        Debug.Log(PV.IsMine);
        Debug.Log(PV.Owner);
    }

    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
