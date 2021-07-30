using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider speedSlider;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        speedSlider.value = gameManager.BallSpeed;
    }

    private void Update()
    {

    }

    public void SetSpeed()
    {
        if (gameManager != null)
        {
            gameManager.BallSpeed = speedSlider.value;
        }
    }

    public void ReturnToMenu()
    {
        if (gameManager != null)
        {
            gameManager.SaveSettings();
        }

        SceneManager.LoadScene(0);
    }
}
