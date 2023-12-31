using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CarLapCounter : MonoBehaviour
{

    public TextMeshProUGUI carPositionText;

    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapsToComplete = 2;

    bool isRaceCompleted = false;

    int carPosition = 0;

    bool isHideRoutineRunning = false;
    float hideUIDelayTime;

    public event Action<CarLapCounter> OnPassCheckpoint;

    public void SetCarPosition(int position){
        carPosition = position;
    }

    public int GetNumberOfCheckPointsPassed(){
        return numberOfPassedCheckpoints;
    }

    public float GetTimeAtLastCheckPoint(){
        return timeAtLastPassedCheckPoint;
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        hideUIDelayTime += delayUntilHidePosition;

        carPositionText.text =carPosition.ToString();

        carPositionText.gameObject.SetActive(true);

        if(!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;
            yield return new WaitForSeconds(hideUIDelayTime);
            carPositionText.gameObject.SetActive(false);

            isHideRoutineRunning = false; 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("CheckPoint"))
        {
            if(isRaceCompleted){return;}

            CheckPoint checkPoint = other.GetComponent<CheckPoint>();

            if(passedCheckPointNumber +1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                timeAtLastPassedCheckPoint = Time.time;

                if(checkPoint.isFinishLine){
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if(lapsCompleted >= lapsToComplete){
                        isRaceCompleted = true;
                    }
                }

                OnPassCheckpoint?.Invoke(this);

                if(isRaceCompleted){StartCoroutine(ShowPositionCO(100));}
                else{StartCoroutine(ShowPositionCO(1.5f));}
            }

        }
    }


}
