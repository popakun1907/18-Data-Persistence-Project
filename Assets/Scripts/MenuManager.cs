using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.Instance;
        SetPlayerName();
    }

    public void EnterPlayerName()
    {
        if (gameManager != null)
        {
            gameManager.PlayerName = inputField.text;
            gameManager.SavePlayer();
        }
    }

    public void SetPlayerName()
    {
        if (gameManager != null)
        {
            inputField.text = gameManager.PlayerName;
        }
    }

    public void StartGame()
    {
        if (gameManager.PlayerName != string.Empty &&
            gameManager.PlayerName != "Enter Your Name" &&
            gameManager.PlayerName.Length <= 10)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Enter your name in coorect form, please!");
        }
    }

    public static void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public static void GoToSettings()
    {
        SceneManager.LoadScene(2);
    }

    public static void GoToScoreTable()
    {
        SceneManager.LoadScene(3);
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Aplication.Quit();
#endif

    }
}
