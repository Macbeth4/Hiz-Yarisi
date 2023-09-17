using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gerisayim : MonoBehaviour
{
    public TextMeshProUGUI countdowndisplay;
    private void Awake() {
        countdowndisplay.text = "";
    }
    private void Start() {
        StartCoroutine(CountDownCO());
    }

    IEnumerator CountDownCO()
    {
        yield return new WaitForSeconds(0.3f);

        int counter = 3;

        while(true){
            if(counter != 0)
            {
                countdowndisplay.text = counter.ToString();
            }
            else
            {
                countdowndisplay.text = "GO!";
                 GameManager.instance.OnRaceStart();
                  break;
                  
            }        
            counter--;
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.5f);
        
        gameObject.SetActive(false);
        countdowndisplay.text = "";

    }
}
