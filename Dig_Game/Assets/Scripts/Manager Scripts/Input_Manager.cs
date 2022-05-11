using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inputs
{
    public string left;
    public string right;
    public string jump;
    public int attack;
    public string inventoryOpen;
    public string[] useUsables = new string[3];
}

public class Input_Manager : MonoBehaviour
{
    [SerializeField] private string FilePath;
    private Inputs inputs;

    // Start is called before the first frame update
    void Awake()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        // If the file doesn't exist, generate one
        if (!File.Exists(FilePath + "Inputs.json"))
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            // Default Keybinds
            inputs = new Inputs();
            inputs.left = "a";
            inputs.right = "d";
            inputs.jump = "space";
            inputs.attack = ((int)KeyCode.Mouse0);
            inputs.inventoryOpen = "e";
            inputs.useUsables[0] = "1";
            inputs.useUsables[1] = "2";
            inputs.useUsables[2] = "3";
            try
            {
                // Save to file
                string json = JsonUtility.ToJson(inputs);
                System.IO.File.WriteAllText(FilePath + "Inputs.json", json);

            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
        else
        {
            // Load File
            string json = File.ReadAllText(FilePath + "Inputs.json");
            inputs = JsonUtility.FromJson<Inputs>(json);
        }
    }
    
    public Inputs GetInputs()
    {
        return inputs;
    }
}
