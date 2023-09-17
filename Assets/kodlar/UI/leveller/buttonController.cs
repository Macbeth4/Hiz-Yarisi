
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour
{
    private Button levelButton;

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject lockIconObject;

    public int buttonValue;
    private bool isComplete;


    private void Start()
    {
        levelButton = GetComponent<Button>();
        levelButton.onClick.AddListener(LoadSelectedScene);
    }


    public void SetLockState()
    {
        isComplete = true;

        if (isComplete)
        {
            buttonText.text = buttonValue.ToString();
            lockIconObject.SetActive(false);
        }
    }



    public void LoadSelectedScene()
    {
        if (isComplete)
        {
            SceneManager.LoadScene(buttonValue);
        }
    }
}
