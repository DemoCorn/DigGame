using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] Input_Manager InputManager;

    [SerializeField] Inventory_Manager InventoryManager;

    private int score = 0;

    [SerializeField] GameObject player;

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

    public void EquipPlayer(float healthChange, float armorChange, float attackChange)
    {
        Player_Health playerHealth = player.GetComponent<Player_Health>();
        Player_Attack playerAttack = player.GetComponentInChildren<Player_Attack>();

        if (playerHealth != null)
        {
            playerHealth.Equip(healthChange, armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerAttack != null)
        {
            playerAttack.Equip(attackChange);
        }
    }

    public void EditInventory(ItemGroup items)
    {
        InventoryManager.EditInventory(items);
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

    public Inputs GetInputs()
    {
        return InputManager.GetInputs();
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