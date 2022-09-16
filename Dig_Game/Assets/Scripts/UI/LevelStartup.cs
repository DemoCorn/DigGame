using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartup : MonoBehaviour
{
    public void Startup()
    {
        GameManager.Instance.TextCrawl();
    }

    public void ResetLevel()
    {
        GameManager.Instance.Reset();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
