using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Equipment_Choice : MonoBehaviour
{
    [SerializeField] List<GameObject> equipment = new List<GameObject>();
    [SerializeField] int currentlyEquiped = 0;
    // Start is called before the first frame update
    void Start()
    {
        SwapEquipment(currentlyEquiped);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            SwapEquipment(currentlyEquiped + 1);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            SwapEquipment(currentlyEquiped - 1);
        }
    }

    public void SwapEquipment(int equipmentIndex)
    {
        if (equipmentIndex >= equipment.Count)
        {
            equipmentIndex = 0;
        }
        if (equipmentIndex < 0)
        {
            equipmentIndex = equipment.Count - 1;
        }

        for (int i = 0; i < equipment.Count; i++)
        {
            equipment[i].SetActive(i == equipmentIndex);
        }
        currentlyEquiped = equipmentIndex;
    }
}
