using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arabakontrol1 : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speedForce;
    public float yavashız;
    public float hiz;
    Rigidbody2D rb;
    Vector2 move;

    public GameObject birkazandi;
    public GameObject ikikazandi;
    public GameObject uckazandi;
    public GameObject dortkazandi;

   private void Start() {
     rb = GetComponent<Rigidbody2D>();
     
   }

   private void Update() {
    if(birkazandi != null){ 
    if(birkazandi.activeSelf)
    {
        speedForce = 0; hiz = 0;
        if(ikikazandi != null){ikikazandi.SetActive(false);}
        if(uckazandi != null){uckazandi.SetActive(false);}
        if(dortkazandi != null){ dortkazandi.SetActive(false);}
       
        GameObject joy = GameObject.Find("Fixed Joystick");
        if(joy != null){joy.SetActive(false);}
        GameObject buton = GameObject.Find("anamenuButton");
        if(buton != null){buton.SetActive(false);}
        
    }
    }

    if(ikikazandi != null){
    if(ikikazandi.activeSelf)
    {
        speedForce = 0; hiz = 0; 
        if(birkazandi != null){birkazandi.SetActive(false);}
        if(uckazandi != null){uckazandi.SetActive(false);}
        if(dortkazandi != null){ dortkazandi.SetActive(false);}
        GameObject joy = GameObject.Find("Fixed Joystick");
        if(joy != null){joy.SetActive(false);}
        GameObject buton = GameObject.Find("anamenuButton");
        if(buton != null){buton.SetActive(false);}
    }
    }
    if(uckazandi != null){

        if(uckazandi.activeSelf)
        {
          speedForce = 0; hiz = 0;
          if(ikikazandi != null){ikikazandi.SetActive(false);}
          if(birkazandi != null){birkazandi.SetActive(false);}
          if(dortkazandi != null){ dortkazandi.SetActive(false);}
          GameObject joy = GameObject.Find("Fixed Joystick");
          if(joy != null){joy.SetActive(false);}
          GameObject buton = GameObject.Find("anamenuButton");
          if(buton != null){buton.SetActive(false);}
        
        }
    }

    if(dortkazandi != null){

        if(dortkazandi.activeSelf)
        {
          speedForce = 0; hiz = 0;
          if(ikikazandi != null){ikikazandi.SetActive(false);}
          if(uckazandi != null){uckazandi.SetActive(false);}
          if(birkazandi != null){birkazandi.SetActive(false);}
          GameObject joy = GameObject.Find("Fixed Joystick");
          if(joy != null){joy.SetActive(false);}
          GameObject buton = GameObject.Find("anamenuButton");
          if(buton != null){buton.SetActive(false);}
        
        }
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
