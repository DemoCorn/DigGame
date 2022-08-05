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
    GameObject player;
    PlayerComponents playerComponents;
    Camera mainCamera;

    [Header("Misc")]
    [SerializeField] int LevelNum;
    [SerializeField] Vector3 PlayerStartPosition;

    [HideInInspector] public bool isMainLevel = false;

    private bool isWinning = false;
    public bool tutorialComplete = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

        playerComponents = new PlayerComponents(
            player.GetComponent<Player_Health>(),
            player.GetComponentInChildren<Player_WeaponStats>(),
            player.GetComponent<Player_Movement>(),
            player.GetComponent<Collider2D>());

        if (tutorialComplete)
        {
            player.transform.position = PlayerStartPosition;
        }

        GenerationManager.Generate();
        InventoryManager.BootUp();
        UIManager.BootUp();
        isMainLevel = true;
    }

    // Changes stats for different player scripts
    public void EquipPlayer(float healthChange, float armorChange, float attackChange, float digChange)
    {
        if (playerComponents.health != null)
        {
            playerComponents.health.Equip(healthChange, armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerComponents.aim != null)
        {
            playerComponents.aim.Equip(attackChange, digChange);
        }
        else
        {
            Debug.LogError("No player aim script on player");
        }
    }

    //use to grab the Player for transformation
    public GameObject GetPlayerTransform()
    {
        return player;
    }    

    // Buff and Debuff lines
    public void BuffPlayer(float healthChange, float armorChange, float attackChange, float digChange, float SpeedChange, float duration = 60.0f)
    {
        if (playerComponents.health != null)
        {
            playerComponents.health.Equip(healthChange, armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerComponents.aim != null)
        {
            playerComponents.aim.Equip(attackChange, digChange);
        }
        else
        {
            Debug.LogError("No player aim script on player");
        }

        if (playerComponents.move != null)
        {
            playerComponents.move.Equip(SpeedChange);
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

        if (playerComponents.health != null)
        {
            playerComponents.health.Equip(-healthChange, -armorChange);
        }
        else
        {
            Debug.LogError("No health script on player");
        }

        if (playerComponents.aim != null)
        {
            playerComponents.aim.Equip(-attackChange, -digChange);
        }
        else
        {
            Debug.LogError("No player aim script on player");
        }

        if (playerComponents.move != null)
        {
            playerComponents.move.Equip(-SpeedChange);
        }
        else
        {
            Debug.LogError("No movement script on player");
        }
    }

    public void ActivatePlayerRevive(float time)
    {
        playerComponents.health.SetRevive(true);
        StartCoroutine(DeactivatePlayerRevive(time));
    }

    IEnumerator DeactivatePlayerRevive(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerComponents.health.SetRevive(false);
    }

    public void RetirePlayer()
    {
        playerComponents.health.Die(true);
    }

    public void HealPlayer(float heal)
    {
        playerComponents.health.Heal(heal);
    }

    // Various get functions
    public float GetPlayerHealth()
    {
        return playerComponents.health.GetHealth();
    }
    public float GetPlayerMaxHealth()
    {
        return playerComponents.health.GetMaxHealth();
    }

    public float GetPlayerAttack()
    {
        return playerComponents.aim.GetAttack();
    }

    public float GetPlayerArmor()
    {
        return playerComponents.health.GetDefence();
    }

    public float GetPlayerDig()
    {
        return playerComponents.aim.GetDig();
    }

    public void PlayerTakeDamage(float dmg)
    {
        playerComponents.health.TakeDamage(dmg);
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public GameObject GetPlayer()
    {
        return player;
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
        return playerComponents.collider;
    }

    public Collider2D GetSwordCollider()
    {
        return playerComponents.aim.GetWeaponHitbox();
    }

    // Scene Control Functions
    public void Reset()
    {
        StartCoroutine("ResetLevel", 1);
    }

    private IEnumerator ResetLevel(int nScene)
    {
        UIManager.loaded = false;
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
        isMainLevel = false;
        SceneManager.LoadScene(2);
    }

    private class PlayerComponents
    {
        public PlayerComponents(Player_Health hp, Player_WeaponStats ws, Player_Movement mov, Collider2D col)
        {
            health = hp;
            aim = ws;
            move = mov;
            collider = col;
        }

        public Player_Health health;
        public Player_WeaponStats aim;
        public Player_Movement move;
        public Collider2D collider;
    }
}