using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCompletion : MonoBehaviour
{
    [SerializeField] private Collider2D playerCheck;
    private Collider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.tutorialComplete)
        {
            Destroy(gameObject);
        }
        else
        {
            playerCollider = GameManager.Instance.GetPlayerCollider();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        List<Collider2D> collisions = new List<Collider2D>();
        playerCheck.OverlapCollider(new ContactFilter2D(), collisions);

        if (collisions.Contains(playerCollider))
        {
            GameManager.Instance.tutorialComplete = true;
            Destroy(gameObject);
        }
    }
}
