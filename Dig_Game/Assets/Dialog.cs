using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    //public GameObject continueButton;
    public GameObject dialogPanel;
    public string dialogPanelTag;
    public SpriteRenderer sprite;

    
    AudioSource click;
    bool active = true;
    bool next = false;

    private void Start()
    {
        //click = GameObject.FindWithTag("Click").GetComponent<AudioSource>();
        dialogPanel = GameObject.FindWithTag("GrandpaDialog");
        textDisplay = dialogPanel.GetComponentInChildren<TextMeshProUGUI>();
        InvokeRepeating("FindVariables", 0.3f, 0.1f);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //click.Play();
            active = false;
            dialogPanel.GetComponent<SpriteRenderer>().color = Color.white;
            //dialogPanel.SetActive(true);
            StartCoroutine(Type());
            Debug.LogWarning(" vvv This line(35) needs to be updated to stop player movement while the player is in dialog.");
            //PlayerMovement.canMove = false;            
        }
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index] && Input.GetKeyDown(KeyCode.Q))
        {
            NextSentence();
        }
    }

    
     IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray()) 
        { 
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        next = false;

        //click.Play();

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            dialogPanel.GetComponent<>() = Color.clear;
            //dialogPanel.SetActive(false);
            sprite.color = Color.clear;
            Debug.LogWarning(" vvv This line(77) needs to be updated to allow player movement after they are done in dialog.");
            //PlayerMovement.canMove = true;
        }
    }

    public void FindVariables()
    {
        dialogPanel = GameObject.FindWithTag("GrandpaDialog");
        //dialogPanel.GetComponent<SpriteRenderer>().color = Color.white;
        textDisplay = dialogPanel.GetComponentInChildren<TextMeshProUGUI>();
    }
}
