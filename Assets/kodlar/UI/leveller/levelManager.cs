using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
   public static levelManager Instance;


    public List<Button> buttonList = new List<Button>();

    private int activeLevelIndex = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

/*public string targetSceneName;
private void Update() {
    if(SceneManager.GetActiveScene().name == targetSceneName){
        LoadCompleteLevels();
        CheckAllButtons();
    }
}
 */
    private void Start()
    {
        LoadCompleteLevels();
        CheckAllButtons();
        
    }



    private void CheckAllButtons()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (activeLevelIndex >= buttonList[i].GetComponent<buttonController>().buttonValue)
            {
                buttonList[i].GetComponent<buttonController>().SetLockState();
            }
        }
    }


    private void LoadCompleteLevels()
    {
        if (PlayerPrefs.GetInt("LevelIndex") == 0)
        {
            activeLevelIndex = 1;
        }
        else
        {
            activeLevelIndex = PlayerPrefs.GetInt("LevelIndex");
        }
    }

     public void SaveActiveLevelIndex()
    {
        activeLevelIndex++;
        PlayerPrefs.SetInt("LevelIndex", activeLevelIndex);
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        PlayerPrefs.DeleteKey("LevelIndex");
    }
   
}