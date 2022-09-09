using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextScript : MonoBehaviour
{
    [SerializeField] float speed = 50.0f;
    float textPosBegin = -1273.286f;
    float boundaryTextEnd = 1276.64f;

    RectTransform myGorectTransform;
    [SerializeField]
    TextMeshProUGUI mainText;

    [SerializeField]
    bool isLooping = false;
    // Start is called before the first frame update
    void Start()
    {
        myGorectTransform = gameObject.GetComponent<RectTransform>();
        StartCoroutine(AutoScrollText());
    }

    IEnumerator AutoScrollText()
    {
        while(myGorectTransform.localPosition.y< boundaryTextEnd)
        {
            myGorectTransform.Translate(Vector3.up * speed * Time.deltaTime );
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
