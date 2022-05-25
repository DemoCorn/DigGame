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
    [SerializeField] Vector3 PlayerStartPosition;

    private bool isWinning = false;
    public bool tutorialComplete = false; 

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
            Destroy(gameObject);
        }
    }

    public void ManagerLoad()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();

        if (tutorialComplete)
        {
            player.transform.position = PlayerStartPosition;
        }

        GenerationManager.Generate();
        UIManager.BootUp();
    }

    // Changes stats for different player scripts
    public void EquipPlayer(float healthChange, float armorChange, float attackChange, float digChange)
    {
        Player_Health playerHealth = player.GetComponent<Player_Health>();
        Player_WeaponStats playerAim = player.GetComponentInChildren<Player_WeaponStats>();

        if (playerHealth != null)
        {
            playerHealth.Equip(healthChange, armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerAim != null)
        {
            playerAim.Equip(attackChange, digChange);
        }
        else
        {
            Debug.LogError("No player aim script on player");
        }
    }

    // Buff and Debuff lines
    public void BuffPlayer(float healthChange, float armorChange, float attackChange, float digChange, float SpeedChange, float duration = 60.0f)
    {
        Player_Health playerHealth = player.GetComponent<Player_Health>();
        Player_WeaponStats playerAim = player.GetComponentInChildren<Player_WeaponStats>();
        Player_Movement playerMove = player.GetComponent<Player_Movement>();

        if (playerHealth != null)
        {
            playerHealth.Equip(healthChange, armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerAim != null)
        {
            playerAim.Equip(attackChange, digChange);
        }
        else
        {
            Debug.LogError("No player aim script on player");
        }

        if (playerMove != null)
        {
            playerMove.Equip(SpeedChange);
        }
        else
        {
            Debug.LogError("No movement script on player");
        }

        StartCoroutine(DebuffPlayer(healthChange, armorChange, attackChange, digChange, SpeedChange, duration));
    }

    IEnumerator DebuffPlayer(float healthChange, float armorChange, float attackChange, float digChange, float SpeedChange, float delay)
    {
        yield return new WaitForSeconds(delay);

        Player_Health playerHealth = player.GetComponent<Player_Health>();
        Player_WeaponStats playerAim = player.GetComponentInChildren<Player_WeaponStats>();
        Player_Movement playerMove = player.GetComponent<Player_Movement>();

        if (playerHealth != null)
        {
            playerHealth.Equip(-healthChange, -armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerAim != null)
        {
            playerAim.Equip(-attackChange, -digChange);
        }
        else
        {
            Debug.LogError("No player aim script on player");
        }

        if (playerMove != null)
        {
            playerMove.Equip(-SpeedChange);
        }
        else
        {
            Debug.LogError("No movement script on player");
        }
    }

    public void ActivatePlayerRevive(float time)
    {
        player.GetComponent<Player_Health>().SetRevive(true);
        StartCoroutine(DeactivatePlayerRevive(time));
    }

    IEnumerator DeactivatePlayerRevive(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.GetComponent<Player_Health>().SetRevive(false);
    }

    public void HealPlayer(float heal)
    {
        player.GetComponent<Player_Health>().Heal(heal);
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

    public float GetPlayerAttack()
    {
        return player.GetComponentInChildren<Player_WeaponStats>().GetAttack();
    }

    public float GetPlayerArmor()
    {
        return player.GetComponent<Player_Health>().GetDefence();
    }

    public float GetPlayerDig()
    {
        return player.GetComponentInChildren<Player_WeaponStats>().GetDig();
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

    public Collider2D GetPlayerCollider()
    {
        return player.GetComponent<Collider2D>();
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