using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sahnegecisleri : MonoBehaviour
{
    public void anamenu(){SceneManager.LoadScene("menu");}
    public void kisilik2(){SceneManager.LoadScene("2oyunculu");}
    public void kisilik3(){SceneManager.LoadScene("3oyunculu");}
    public void kisilik4(){SceneManager.LoadScene("4oyunculu");}
    public void yenidendene(){SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}
    
}
