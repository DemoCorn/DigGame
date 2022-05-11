using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;


    [Header("Other Managers")]
    public Input_Manager InputManager;
    public Inventory_Manager InventoryManager;
    public Layer_Manager LayerManager;
    public Generation_Manager GenerationManager;
    public UI_Manager UIManager;

    [Header("Objects")]
    [SerializeField] GameObject player;
    [SerializeField] Camera mainCamera;

    [Header("Misc")]
    [SerializeField] int LevelNum;

    private bool isWinning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ManagerLoad();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ManagerLoad()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();

        GenerationManager.Generate();
        UIManager.BootUp();
    }

    // Changes stats for different player scripts
    public void EquipPlayer(float healthChange, float armorChange, float attackChange, float digChange)
    {
        Player_Health playerHealth = player.GetComponent<Player_Health>();
        Player_Attack playerAttack = player.GetComponentInChildren<Player_Attack>();
        Weapon_Attack weaponAttack = player.GetComponentInChildren<Weapon_Attack>();

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
            playerAttack.Equip(digChange);
        }
        else
        {
            Debug.LogError("No player attack script on player");
        }

        if (playerAttack != null)
        {
            playerAttack.Equip(attackChange);
        }
        else
        {
            Debug.LogError("No weapon attack script on player");
        }
    }

    // Various get functions
    public float GetPlayerHealth()
    {
        return player.GetComponent<Player_Health>().GetHealth();
    }
    public float GetPlayerMaxHealth()
    {
        return player.GetComponent<Player_Health>().GetMaxHealth();
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

    public int GetLevelNum()
    {
        return LevelNum;
    }

    public Collider2D GetSwordCollider()
    {
        return player.transform.Find("Aim/Weapon").GetComponent<Player_WeaponStats>().GetWeaponHitbox();
    }

    // Scene Control Functions
    public void Reset()
    {
        StartCoroutine("ResetLevel", 0);
    }

    private IEnumerator ResetLevel(int nScene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(nScene, LoadSceneMode.Single);

        while (!load.isDone)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        ManagerLoad();
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