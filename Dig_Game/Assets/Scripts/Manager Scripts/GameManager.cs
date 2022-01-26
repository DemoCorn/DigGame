using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] Input_Manager InputManager;

    [SerializeField] Inventory_Manager InventoryManager;

    [SerializeField] GameObject player;

    [SerializeField] Camera mainCamera;

    private bool isWinning = false;

    [SerializeField] int LevelNum;

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

    public void EquipPlayer(float healthChange, float armorChange, float attackChange, float digChange)
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
            playerAttack.Equip(attackChange, digChange);
        }
        else
        {
            Debug.LogError("No attack script on player");
        }
    }

    public void EditInventory(ItemGroup items)
    {
        InventoryManager.EditInventory(items);
    }

    public float GetPlayerHealth()
    {
        return player.GetComponent<Player_Health>().GetHealth();
    }

    public Vector3 GetCameraPosition()
    {
        return mainCamera.transform.position;
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

    public int GetLevelNum()
    {
        return LevelNum;
    }
}