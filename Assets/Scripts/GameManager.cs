using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static UnityEvent<bool> OnGameOver { get; private set; } = new();
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Start()
    {
        OnGameOver.AddListener(EndGame);
    }
    private void EndGame(bool _isVictory)
    {
        gameOverScreen.SetActive(true);
        PlayerMovement.IsMoving = false;
        if(_isVictory)
        {
            gameOverText.text = "Victory";
        }
        else
        {
            gameOverText.text = "Defeat";
        }
    }
    public void ExitApp()
    {
        Application.Quit();
    }
}
