using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{

    public bool isUIInput = false;

    Vector2 inputVector = Vector2.zero;
    araba araba;

    public FixedJoystick joystick;

    private void Awake() {
        araba = GetComponent<araba>();
    }

    private void Update() 
    {
        if(isUIInput){

        }
        else{
            inputVector = Vector2.zero;

            inputVector.x = joystick.Horizontal;
            inputVector.y = joystick.Vertical;

        }
        araba.SetInputVector(inputVector);
    }

    public void SetInput(Vector2 newInput){
        inputVector = newInput;
    }

}
