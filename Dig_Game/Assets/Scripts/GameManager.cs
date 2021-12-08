using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private int score = 0;

    public GameObject player;

    private bool isWinning = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetScore()
    {
        return score;
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public float GetPlayerHealth()
    {
        return player.GetComponent<Player_Health>().GetHealth();
    }

    public bool IsWinning()
    {
        return isWinning;
    }

    public void EndGame(bool Winning)
    {
        isWinning = Winning;
        SceneManager.LoadScene("End Screen");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Level");
    }
}