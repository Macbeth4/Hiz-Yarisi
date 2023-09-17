using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arabakontrol : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speedForce;
    public float yavashız;
    public float hiz;
    Rigidbody2D rb;
    Vector2 move;

    public GameObject kazandinpanel;
    public GameObject kaybettinpanel;

   private void Start() {
     rb = GetComponent<Rigidbody2D>();
     
   }

   private void Update() {
        
    if(kazandinpanel.activeSelf)
    {
        speedForce = 0; hiz = 0;
        kaybettinpanel.SetActive(false);
        GameObject joy = GameObject.Find("Fixed Joystick");
        if(joy != null){joy.SetActive(false);}
        GameObject buton = GameObject.Find("anamenuButton");
        if(buton != null){buton.SetActive(false);}
        
    }

    if(kaybettinpanel.activeSelf)
    {
        speedForce = 0; hiz = 0; 
        kazandinpanel.SetActive(false);
        GameObject joy = GameObject.Find("Fixed Joystick");
        if(joy != null){joy.SetActive(false);}
        GameObject buton = GameObject.Find("anamenuButton");
        if(buton != null){buton.SetActive(false);}
    }



     if (alanIcerisinde == true)
        {
            speedForce = yavashız;
        }
        else
        {
           speedForce = hiz;
        }
   
   }

private void FixedUpdate() {
    if(GameManager.instance.GetGameStates()== GameStates.countdown){return;}


        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis,vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f,0f,-zAxis);
   
     rb.MovePosition(rb.position + move * speedForce * Time.fixedDeltaTime);
     
}
     private bool alanIcerisinde = false;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "kum"){
            alanIcerisinde = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "kum"){
            alanIcerisinde = false;
        }
    }

}
