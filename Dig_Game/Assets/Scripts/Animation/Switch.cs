using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Switch : MonoBehaviour
{
    //public List<SpriteResolver> spriteResolvers = new List<SpriteResolver>();
   
    //Define sprite resolver
    public SpriteResolver chest;
    public SpriteResolver head;
    public SpriteResolver leftLeg;
    public SpriteResolver rightLeg;
    public SpriteResolver weapon;
    public SpriteResolver pickaxe;



    // Start is called before the first frame update
    void Start()
    {
       // foreach (var resolver in FindObjectsOfType<SpriteResolver>())
       // {
       //     spriteResolvers.Add(resolver);
      //  }
    }

    // Update is called once per frame
    void Update()
    {
        Equipment[] equipment = GameManager.Instance.InventoryManager.GetEquipment();
        


        

            //Get specific equipment sprite
            chest.SetCategoryAndLabel("Chest",equipment[2].armorName );
            head.SetCategoryAndLabel("Head", equipment[1].armorName);
            leftLeg.SetCategoryAndLabel("LeftLeg", equipment[3].armorName);
            rightLeg.SetCategoryAndLabel("RightLeg", equipment[4].armorName);
            weapon.SetCategoryAndLabel("Weapon", equipment[5].armorName);
            pickaxe.SetCategoryAndLabel("Pickaxe", equipment[0].armorName);


        // foreach(var resolver in FindObjectsOfType<SpriteResolver>())
        // {
        //  }


    }
}
