using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class UpdateGraph : MonoBehaviour
{
    public static UpdateGraph updateGraphInstance;

    AstarPath pathFinder;
    float updateTimer = 5.0f;

    public enum PlayerLocationStates { mainGame, tutorialLevel, bossRoom};
    public PlayerLocationStates currentPlayerLocation;


    private void Awake()
    {
        pathFinder = GetComponent<AstarPath>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (updateGraphInstance != null && updateGraphInstance != this)
        {
            Destroy(this);
        }
        else
        {
            updateGraphInstance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPlayerLocation)
        {
            case (PlayerLocationStates.mainGame):
                StartCoroutine(MainGraphScan());
                break;
            
            case (PlayerLocationStates.tutorialLevel):
                StartCoroutine(TutorialGraphScan());
                break;

            case (PlayerLocationStates.bossRoom):
                StartCoroutine(BossLevelGraphScan());
                break;
        }
    }

    public IEnumerator MainGraphScan()
    {
        yield return new WaitForSeconds(2f);
        pathFinder.Scan(pathFinder.graphs[0]);
    }

    public IEnumerator TutorialGraphScan()
    {
        yield return new WaitForSeconds(0.5f);
        pathFinder.Scan(pathFinder.graphs[1]);
    }

    public IEnumerator BossLevelGraphScan()
    {
        yield return new WaitForSeconds(0.5f);
        pathFinder.Scan(pathFinder.graphs[2]);
    }

    public void SwitchToMainGameGraph()
    {
        currentPlayerLocation = PlayerLocationStates.mainGame;
    }

    public void SwitchToTutorialGraph()
    {
        currentPlayerLocation = PlayerLocationStates.tutorialLevel;
    }

    public void SwitchToBossLevelGraph()
    {
        currentPlayerLocation = PlayerLocationStates.bossRoom;
    }
}
