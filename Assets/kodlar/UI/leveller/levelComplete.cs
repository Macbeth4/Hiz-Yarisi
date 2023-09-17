
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelComplete : MonoBehaviour
{
    private Button levelCompleteButton;

    private void Start()
    {
        levelCompleteButton = GetComponent<Button>();
        levelCompleteButton.onClick.AddListener(LevelComplete);
       // Debug.Log("Level Index " + SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelComplete()
    {
        if (PlayerPrefs.GetInt("LevelIndex") <= SceneManager.GetActiveScene().buildIndex)
        {
            levelManager.Instance.SaveActiveLevelIndex();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}