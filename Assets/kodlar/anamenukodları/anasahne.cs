using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class anasahne : MonoBehaviour
{
    public GameObject oynapaneli;
    public GameObject menupanel;
    public GameObject inspaneli;
    public GameObject levellerpaneli;
    public GameObject cokkisilikpanel;
    private void Awake() {
        menupanel.SetActive(true);
        oynapaneli.SetActive(false);
        inspaneli.SetActive(false);
        levellerpaneli.SetActive(false);
        cokkisilikpanel.SetActive(false);
    }
    public void oynamabutton(){
        oynapaneli.SetActive(true);
        menupanel.SetActive(false);
    }

    public void anamenupanel(){
        menupanel.SetActive(true);
        oynapaneli.SetActive(false);
        inspaneli.SetActive(false);
    }

    public void levellerpanel(){
        levellerpaneli.SetActive(true);
        oynapaneli.SetActive(false);
    }
    public void gerioynamapanel(){
        oynapaneli.SetActive(true);
        levellerpaneli.SetActive(false);
        cokkisilikpanel.SetActive(false);
    }
    public void cokkisilikoyna(){
        cokkisilikpanel.SetActive(true);
        oynapaneli.SetActive(false);
    }
    public void insayarlar(){
        inspaneli.SetActive(true);
        menupanel.SetActive(false);
    }



}
