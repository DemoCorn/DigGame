using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    //public GameObject continueButton;
    public GameObject dialogPanel;
    public GameObject dialogPanelPressQ;
    public string dialogPanelTag;
    public SpriteRenderer sprite;
    public GameObject barrier;

    private Image dialogImage = null;
    private Text dialogText = null;

    private IEnumerator GrandpaDialog;
    
    bool active = true;
    bool next = false;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && active)
        {
            //click.Play();
            active = false;
            dialogImage.color = Color.white;
            dialogText.color = Color.white;
            //dialogPanel.SetActive(true);
            GrandpaDialog = Type();
            StartCoroutine(GrandpaDialog);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            //click.Play();
            active = true;
            StopCoroutine(GrandpaDialog);
            dialogImage.color = Color.clear;
            dialogText.color = Color.clear;
            textDisplay.text = "";

            //dialogPanel.SetActive(true);
        }
    }

    private void Update()
    {
        if (dialogImage != null && dialogText != null)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
                QuickExitDialog();

            if (textDisplay.text == sentences[index] && Input.GetKeyDown(KeyCode.Q))
                NextSentence();
        }
        else if(GameManager.Instance.UIManager.loaded)
        {
            dialogPanel = GameObject.FindWithTag("GrandpaDialog");
            dialogPanelPressQ = GameObject.FindWithTag("PressQ");
            textDisplay = dialogPanel.GetComponentInChildren<TextMeshProUGUI>();
            dialogImage = dialogPanel.GetComponent<Image>();
            dialogText = dialogPanelPressQ.GetComponent<Text>();
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

        

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            dialogImage.color = Color.clear;
            dialogText.color = Color.clear;
            //dialogPanel.GetComponent<SpriteRenderer>().color = Color.white;
            dialogPanel.SetActive(false);
            sprite.color = Color.clear;
            barrier.SetActive(false);
        }
    }

    public void QuickExitDialog()
    {
        textDisplay.text = "";
        dialogImage.color = Color.clear;
        dialogText.color = Color.clear;
        //dialogPanel.GetComponent<SpriteRenderer>().color = Color.white;
        //dialogPanel.SetActive(false);
        sprite.color = Color.clear;
        Debug.LogWarning(" vvv This line(77) needs to be updated to allow player movement after they are done in dialog.");
        barrier.SetActive(false);
    }
}
