using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Again : MonoBehaviour
{
    public void Retry()
    {
        GameManager.Instance.RestartGame();
    }
}
