using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;
    public SpriteRenderer sprite;

    
    AudioSource click;
    bool active = true;
    bool next = false;

    private void Start()
    {
        //click = GameObject.FindWithTag("Click").GetComponent<AudioSource>();
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //click.Play();
            active = false;
            dialogPanel.SetActive(true);
            StartCoroutine(Type());
            Debug.LogWarning(" vvv This line(35) needs to be updated to stop player movement while the player is in dialog.");
            //PlayerMovement.canMove = false;            
        }
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
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
        continueButton.SetActive(false);

        click.Play();

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            dialogPanel.SetActive(false);
            sprite.color = Color.clear;
            Debug.LogWarning(" vvv This line(77) needs to be updated to allow player movement after they are done in dialog.");
            //PlayerMovement.canMove = true;
        }
    }
}
