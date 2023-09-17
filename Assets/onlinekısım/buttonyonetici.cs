using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class buttonyonetici : MonoBehaviour
{
    public TMPro.TextMeshProUGUI iceriktext;
    public TMPro.TMP_InputField icerikinput;
    public PhotonView pw;

    public void lobiyegit(){
        PhotonNetwork.LoadLevel("onlinegiris");
    }

    public void yazigonder(){
        string mesaj = icerikinput.text;

        pw.RPC("yazigonder",RpcTarget.All,mesaj);

        icerikinput.text = null;

    }

    [PunRPC]
    public void yazigonder(string mesaj){
        iceriktext.text = mesaj;

        Invoke("yazi_sil",4.0f);
    }

    void yazi_sil(){iceriktext.text = null;}
}
