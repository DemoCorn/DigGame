using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script is for greying out the buttons
public class CraftingButtonGreyOut : MonoBehaviour
{
    
  //public CanvasRenderer Button;
    public SpriteRenderer Button;
    //the grey out state of the button
  public  Sprite GreyOut;
    //the state of the button when its near the craft button 
  public Sprite Normal;

    private void Start()
    {
        //Button = GameObject.GetComponent<Image>();

    }

    private void Update()
    {
        if (GameManager.Instance.UIManager.onCraftingTable== true)
        {
            ChangeSprite(Normal);
        }    

        else
        {
            ChangeSprite(GreyOut);
        }
    }
    void ChangeSprite(Sprite SpriteState)
    {
        Button.sprite = SpriteState;
        
    }
 
}
