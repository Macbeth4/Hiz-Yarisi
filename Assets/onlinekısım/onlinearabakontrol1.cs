using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Photon.Pun;
using TMPro;
using System;

public class onlinearabakontrol1 : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speedForce;
    public float yavashız;
    public float hiz;
    Rigidbody2D rb;
    Vector2 move;

    public TextMeshProUGUI yazi;
    PhotonView pw;

    public GameObject sprite;
    public GameObject gerisayim;
   private void Start() 
   {
     move = transform.position;
     
     //gerisayim = GameObject.Find("sayacseyi").GetComponent<gerisayim1>();
     
     yazi = GameObject.Find("Canvas/bekleniyor").GetComponent<TMPro.TextMeshProUGUI>();
     pw = GetComponent<PhotonView>();
     rb = GetComponent<Rigidbody2D>();

     

     if(pw.IsMine)
     {
        joystick = FindObjectOfType<FixedJoystick>();
        joystick.gameObject.SetActive(false);
        sprite.GetComponent<SpriteRenderer>().color = Color.red;

        

        if(PhotonNetwork.IsMasterClient){
            transform.position = new Vector3(-7.5f,-0.5f,0);  
            InvokeRepeating("oyuncukontrol",0,0.5f);  
        }
        else if(!PhotonNetwork.IsMasterClient){
            transform.position = new Vector3(-6.5f,-0.5f,0);
             InvokeRepeating("oyuncukontrol1",0,0.5f);  
        }

     }
     if(!pw.IsMine)
     {
        sprite.GetComponent<SpriteRenderer>().color = Color.blue;

     }
     
   }


   void oyuncukontrol()
   {

    if(PhotonNetwork.PlayerList.Length == 2)
    {
        pw.RPC("yazisil",RpcTarget.All,null);
        
        GameObject.Find("sayacseyi").GetComponent<PhotonView>().RPC("gerisaymayabasla",RpcTarget.All,null);

        CancelInvoke("oyuncukontrol");
    }
    if(PhotonNetwork.PlayerList.Length == 1){
        
    }
   }

   void oyuncukontrol1()
   {
    if(PhotonNetwork.PlayerList.Length == 2)
    {
        pw.RPC("yazisil2",RpcTarget.All,null);

         CancelInvoke("oyuncukontrol1");
    }
   }



   [PunRPC]
   public void yazisil()
   { 
     yazi.text = "";
     yazisilllme(); 
   }
   [PunRPC]
   public void yazisil2()
   { 
     yazi.text = "";
     yazisilllme(); 
   }

   public async void yazisilllme(){
     await Task.Delay(TimeSpan.FromSeconds(4.0f));
     joystick.gameObject.SetActive(true);
   }

   [PunRPC]
   public void oyundan_kacti(){
    InvokeRepeating("oyuncukontrol",0,0.5f);
    yazi.text = "OYUNCU BEKLENİYOR...";
    
   }

   private void Update() {
        
    if(pw.IsMine){
        if (alanIcerisinde == true)
        {
            speedForce = yavashız;
        }
        else
        {
           speedForce = hiz;
        }
    }
     
   
   }

private void FixedUpdate() {
    

    if(pw.IsMine){
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis,vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f,0f,-zAxis);
   
        rb.MovePosition(rb.position + move * speedForce * Time.fixedDeltaTime);
     
    }
       
}
     private bool alanIcerisinde = false;
    private void OnTriggerStay2D(Collider2D other) {
        if(pw.IsMine){
            if (other.gameObject.tag == "kum"){
                alanIcerisinde = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(pw.IsMine){
            if (other.gameObject.tag == "kum"){
                alanIcerisinde = false;
            }
        }
    }

}
