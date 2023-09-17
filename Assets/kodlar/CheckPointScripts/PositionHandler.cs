using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PositionHandler : MonoBehaviour
{
    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    private void Start() {
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        foreach (CarLapCounter lapCounter in carLapCounters){
            lapCounter.OnPassCheckpoint += OnPassCheckpoint;
        }
    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {
        
        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberOfCheckPointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();

        int carPosition = carLapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPosition(carPosition);

        
    }


}