using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimedBlocks : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] public bool isvisible;
    [SerializeField] public float AppearTime;
    [SerializeField] public float InvisibleTime;
    private float currenttime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        isvisible = true;
    }

    public void setVisible(bool val)
    {

        if (val == true)
        {
            isvisible = true;
        }

        else
        {
            Debug.Log("SetVisible Toggled to Disable");
            isvisible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isvisible == false)
        {
            TimerFunctionINVisible();
            //InVisibleNONInteractable();
        }

        else
        {
            TimerFunctionVisible();
           // VisibleInteractable();
        }
        ///for testing purposes
        //TimerFunction();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GetComponent<Collider2D>().enabled = false;
        rend.enabled = false;
    }

    public void VisibleInteractable()
    {
        rend.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    public void InVisibleNONInteractable()
    {
        Debug.Log("activated Invisible block");
        rend.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void TimerFunctionVisible()
    {
        currenttime += Time.deltaTime;

        if (currenttime >= AppearTime)
        {
            Debug.Log("visible");
            InVisibleNONInteractable();
            isvisible = false;
            currenttime = 0;
        }
    }

    public void TimerFunctionINVisible()
    {
        currenttime += Time.deltaTime;

        if(currenttime>=InvisibleTime)
        {
            Debug.Log("Invisible");
            VisibleInteractable();
            isvisible = true;
            currenttime = 0;
        }
    }

}
