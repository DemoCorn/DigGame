using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextScript : MonoBehaviour
{
    [SerializeField] float speed = 50.0f;
    [SerializeField] float textPosBegin = -1273.286f;
    [SerializeField] float boundaryTextEnd = 1276.64f;
    bool loadingLevel = false;

    RectTransform myGorectTransform;

    // Start is called before the first frame update
    void Start()
    {
        myGorectTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoScrollText());
    }

    IEnumerator AutoScrollText()
    {
        while(myGorectTransform.localPosition.y < boundaryTextEnd)
        {
            myGorectTransform.Translate(Vector3.up * speed * Time.deltaTime );
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((myGorectTransform.localPosition.y >= boundaryTextEnd || Input.GetKeyDown("space")) && !loadingLevel)
        {
            loadingLevel = true;
            GameManager.Instance.Reset();
        }
    }
}
