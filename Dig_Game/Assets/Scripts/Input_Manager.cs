using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class Inputs
{
    public string left;
    public string right;
    public string jump;
    public string uAttack;
    public string dAttack;
    public string lAttack;
    public string rAttack;
}

public class Input_Manager : MonoBehaviour
{
    [SerializeField] private string FilePath;
    private Inputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        LoadJson();
    }

    public void LoadJson()
    {
        if (!File.Exists(FilePath + "Inputs.json"))
        {
            inputs = new Inputs();
            inputs.left = "a";
            inputs.right = "d";
            inputs.jump = "space";
            inputs.uAttack = "up";
            inputs.dAttack = "down";
            inputs.lAttack = "left";
            inputs.rAttack = "right";
            try
            {
                // Save to file
                using (StreamWriter file = File.CreateText(FilePath + "Inputs.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, inputs);
                }

            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
        else
        {
            using (StreamReader file = File.OpenText(FilePath + "Inputs.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                inputs = (Inputs)serializer.Deserialize(file, typeof(Inputs));
            }
        }
    }
    
    public Inputs GetInputs()
    {
        return inputs;
    }
}
