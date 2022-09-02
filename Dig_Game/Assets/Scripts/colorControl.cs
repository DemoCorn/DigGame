using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorControl : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalTime;
    public bool active;
    public bool leave;
    [SerializeField][Range(0f, 1f)] float t;
    private SpriteRenderer mask_render;
    private SpriteRenderer black_render;
    private GameObject root;
    private GameObject root2;
    private Color oldMask;
    Color newMask;
    private Color oldBlack;
    Color newBlack;
    private float timeSpan;
    void Start()
    {
        active = false;
        leave = false;
        root = GameObject.Find("Big Mask");
        root2 = GameObject.Find("Black");
        mask_render = root.GetComponent<SpriteRenderer>();
        black_render = root2.GetComponent<SpriteRenderer>();
        oldMask = mask_render.color;
        oldBlack = black_render.color;
        newMask = new Color32(56,56,56,0);
        newBlack = new Color32(0, 0, 0, 0);
        t = 0;
        timeSpan = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(active && !leave)
        {
            timeSpan += Time.deltaTime;
            t = Mathf.Lerp(0, 1, timeSpan / totalTime);
            mask_render.color = Color.Lerp(oldMask, newMask, t);
            black_render.color = Color.Lerp(oldBlack, newBlack,t);
        }

        if(!active && leave)
        {
            timeSpan += Time.deltaTime;
            t = Mathf.Lerp(0, 1, timeSpan / totalTime);
            mask_render.color = Color.Lerp(newMask, oldMask, t);
            black_render.color = Color.Lerp(newBlack, oldBlack, t);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leave = false;
            active = true;
            timeSpan = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leave = true;
            active = false;
            timeSpan = 0;
        }
    }
}
