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
    public GameObject root;
    public GameObject dialogPanel;
    public GameObject dialogPanelPressQ;
    public string dialogPanelTag;
    public SpriteRenderer sprite;
    public GameObject barrier;

    private Image dialogImage = null;
    private Text dialogText = null;

    private IEnumerator GrandpaDialog;

    public bool active;
    public bool done;
    public bool begin;
    public bool start;

    void Start()
    {
        start = false;
        StartCoroutine(LateStart(1));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (GameManager.Instance.UIManager.loaded)
        {
            Debug.Log("enter 1s");
            root = GameObject.Find("DialogParent");
            dialogPanel = root.transform.Find("DialogPanel").gameObject;
            dialogPanelPressQ = root.transform.Find("DialogPanel/Press Q").gameObject;

            textDisplay = dialogPanel.GetComponentInChildren<TextMeshProUGUI>();
            dialogImage = dialogPanel.GetComponent<Image>();
            dialogText = dialogPanelPressQ.GetComponent<Text>();
            dialogImage.color = Color.white;
            dialogText.color = Color.white;
            dialogPanel.SetActive(false);
            dialogPanelPressQ.SetActive(false);
            begin = false;
            active = false;
            done = false;
            start = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textDisplay.text = "";
            index = 0;
            dialogPanel.SetActive(false);
            active = false;
            begin = false;
        }
    }

    private void Update()
    {
        if (active && !begin && !done)
        {
            if (dialogPanel.activeInHierarchy)
            {
                textDisplay.text = "";
                index = 0;
                dialogPanel.SetActive(false);
                dialogPanelPressQ.SetActive(false);
            }
            else
            {
                dialogPanel.SetActive(true);
                dialogPanelPressQ.SetActive(true);
                GrandpaDialog = Type();
                StartCoroutine(GrandpaDialog);
            }

            begin = true;
        }


        if (Input.GetKeyDown(KeyCode.R) && active && !done)
            QuickExitDialog();

        if (start)
        {
            if (textDisplay.text == sentences[index] && Input.GetKeyDown(KeyCode.Q) && !done && active)
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

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            dialogPanel.SetActive(false);
            if (this.transform.parent.gameObject.tag != "notDisappear")
            {
                this.gameObject.transform.parent.gameObject.SetActive(false);
            }
            done = true;
        }
    }

    public void QuickExitDialog()
    {
        if (this.transform.parent.gameObject.tag != "notDisappear")
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
}