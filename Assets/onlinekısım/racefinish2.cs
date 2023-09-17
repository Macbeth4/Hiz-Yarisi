using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class racefinish2 : MonoBehaviour
{
   [Header("Points")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints = new GameObject[6]; // 6 elemanlı bir dizi tanımladık

    [Header("Settings")]
    public float laps = 1;

    [Header("Information")]
    private float currentCheckpoint;
    private float currentLap;
    private bool started;
    private bool finished;

    private float currentLapTime;
    private float bestLapTime;
    private float bestLap;

    [Header("ONLİNE KISIM")]
    PhotonView pw;

    public GameObject kazandinpanel;
    public GameObject kaybettinpanel;
    private void Start()
    {
        start = GameObject.Find("1"); // StartPoint isimli objeyi bulup start değişkenine atar
        end = GameObject.Find("1"); // EndPoint isimli objeyi bulup end değişkenine atar

        checkpoints[0] = GameObject.Find("1");
        checkpoints[1] = GameObject.Find("2");
        checkpoints[2] = GameObject.Find("3");
        checkpoints[3] = GameObject.Find("4");
        checkpoints[4] = GameObject.Find("5");
        checkpoints[5] = GameObject.Find("6");


        currentCheckpoint = 0;
        currentLap = 1;

        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
        bestLap = 0;

        pw = GetComponent<PhotonView>();

        // Canvas objesini bul
        GameObject canvas = GameObject.Find("Canvas");

        // Canvas objesinin altındaki panelleri bul
        kazandinpanel = canvas.transform.Find("kazandinpanel").gameObject;
        kaybettinpanel = canvas.transform.Find("kaybettinpanel").gameObject;
        
    }

    
    public FixedJoystick joystick;

    [PunRPC]
    public void ShowLosePanel()
    {
         kaybettinpanel.SetActive(true);
         Time.timeScale = 0;
         joystick = FindObjectOfType<FixedJoystick>();
         joystick.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;
            
            if (bestLap == 0)
            {
                bestLap = 1;
            }
        }

        if (started)
        {
            if (bestLap == currentLap)
            {
                bestLapTime = currentLapTime;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="CheckPoint")
        {
            GameObject thisCheckpoint = other.gameObject;

            // Started the race
            if (thisCheckpoint == start && !started)
            {
                started = true;
            }
            // Ended the lap or race
            else if (thisCheckpoint == end && started)
            {
                // If all the laps are finished, end the race
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                        }

                        finished = true;  

                        if(pw.IsMine){
                            kazandinpanel.SetActive(true);
                            Time.timeScale = 0;
                            joystick = FindObjectOfType<FixedJoystick>();
                            joystick.gameObject.SetActive(false);
                            pw.RPC("ShowLosePanel", RpcTarget.Others);
                        }
                        
                    }
                    
                }
                // If all laps are not finished, start a new lap
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                            bestLapTime = currentLapTime; // Because the update function has already run this frame, we need to add this line or it won't work
                        }

                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        
                    }
                    
                }
            }

            // Loop through the checkpoints to compare and check which one the player touched
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                    return;

                // If the checkpoint is correct
                if (thisCheckpoint == checkpoints[i] && i + 1 == currentCheckpoint + 1)
                {
                  
                    currentCheckpoint++;
                }
                // If the checkpoint is incorrect
                else if (thisCheckpoint == checkpoints[i] && i + 1 != currentCheckpoint + 1)
                {
                   
                }
            }
        }
    }

}
