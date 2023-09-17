using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class menubuttonyonet : MonoBehaviour
{

    public void oyunsahnesi(){
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinOrCreateRoom("yenioda", new RoomOptions{ MaxPlayers = 2 , IsOpen = true , IsVisible = true },TypedLobby.Default);
            SceneManager.LoadScene("onlinesahne");       
        }
    }



}
