using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField inputField;
    public TMP_Text highScoreText;
    public Button clearDataButton;

    // Start is called before the first frame update
    void Start()
    {
        inputField.onValueChanged.AddListener(NameChanged);

        if (DataManager.Instance.HighScore > 0)
        {
            highScoreText.text = $"High Score : {DataManager.Instance.HighScore} - {DataManager.Instance.HighScoreName}";
            clearDataButton.gameObject.SetActive(true);
        }
        else
        {
            highScoreText.text = "";
            clearDataButton.gameObject.SetActive(false);
        }
        inputField.text = DataManager.Instance.PlayerName;
        inputField.Select();
        inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearHighScore()
    {
        highScoreText.text = "";
        DataManager.Instance.ClearData();
        clearDataButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        DataManager.Instance.PlayerName = inputField.text;
        SceneManager.LoadScene(1);
    }

    public void NameChanged(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }
    }

    public void Exit()
    {
        // MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
