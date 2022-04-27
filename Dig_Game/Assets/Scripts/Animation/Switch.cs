using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Switch : MonoBehaviour
{
    public List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (var resolver in FindObjectsOfType<SpriteResolver>())
        {
            spriteResolvers.Add(resolver);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            
            foreach(var resolver in FindObjectsOfType<SpriteResolver>())
            {
                resolver.SetCategoryAndLabel(resolver.GetCategory(), "helmet1");
            }
        }
    }
}
