using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenResult : MonoBehaviour
{
    [SerializeField] private GameObject Retire;
    [SerializeField] private GameObject Die;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.IsWinning())
        {
            Retire.SetActive(true);
            Die.SetActive(false);
        }
        else
        {
            Retire.SetActive(false);
            Die.SetActive(true);
        }
    }
}
