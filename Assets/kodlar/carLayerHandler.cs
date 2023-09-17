using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carLayerHandler : MonoBehaviour
{
    
    List<SpriteRenderer> defultLayerSpriteRenderers = new List<SpriteRenderer>();

    bool isDrivingOnOverPass = false;

    Collider2D carCollider;

    private void Awake() 
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if(spriteRenderer.sortingLayerName == "araba"){
                defultLayerSpriteRenderers.Add(spriteRenderer);
            }

        }

        carCollider = GetComponentInChildren<Collider2D>();
    
        carCollider.gameObject.layer = LayerMask.NameToLayer("ObjectOnUnderpass");
    }


    private void Start() {
        UpdateSortingAndCollisionLayers();
    }

    void UpdateSortingAndCollisionLayers(){
        if(isDrivingOnOverPass){
            SetSortingLayer("ustyol");
        }
        else{
            SetSortingLayer("araba");
        }

    }


    void SetSortingLayer(string layerName){
        foreach (SpriteRenderer spriteRenderer in defultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    public GameObject ustbox1,ustbox2;
    public GameObject yanbox1,yanbox2;
    private void Update() {
        if(isDrivingOnOverPass == false){
            ustbox1.SetActive(false);
            ustbox2.SetActive(false);
            yanbox1.SetActive(true);
            yanbox2.SetActive(true);
        }
        else{
            ustbox1.SetActive(true);
            ustbox2.SetActive(true);
            yanbox1.SetActive(false);
            yanbox2.SetActive(false);
        }
    }

   private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("UnderpassTrigger"))
        {
            isDrivingOnOverPass = false;

            carCollider.gameObject.layer = LayerMask.NameToLayer("ObjectOnUnderpass");

            UpdateSortingAndCollisionLayers();
        }  
        else if(other.CompareTag("OverpassTrigger"))
        {
            isDrivingOnOverPass = true;

            carCollider.gameObject.layer = LayerMask.NameToLayer("ObjectOnOverpass");
            UpdateSortingAndCollisionLayers();
        }
   }


}
