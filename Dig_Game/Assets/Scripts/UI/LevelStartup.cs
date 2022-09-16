using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartup : MonoBehaviour
{
    public void Startup()
    {
        GameManager.Instance.TextCrawl();
    }
}
