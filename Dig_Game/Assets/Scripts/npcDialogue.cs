using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npcDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public bool playerIsClose;
    private bool done;
    private bool begin;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
        begin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!begin && playerIsClose && !done)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(typing());
            }
            begin = true;
        }

        if (begin && dialogueText.text == dialogue[index])
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                NextLine();
            }
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(typing());
        }
        else
        {
            done = true;
            zeroText();


            if (this.transform.parent.gameObject.tag == "disappear")
            {
                Debug.Log("enter compareTag!");
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            Debug.Log("enter!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
            begin = false;
        }
    }
}

