using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Manager : MonoBehaviour
{
    public ClassType currentClass;

    public List<ClassTools> PlayerTools;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum ClassType
{
    None = 0,
    Fighter = 1,
    Gladiator = 2,
    Engineer = 3
}

[System.Serializable]
public class ClassTools
{
    [SerializeField] ClassType PlayerClass;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject digTool;
}