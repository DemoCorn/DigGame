using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMovement : MonoBehaviour
{
    public float playerHealth = 100f;
    public float speed = 0;
    static bool staticBool;

    public Text playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = ("Players Health: ") + playerHealth.ToString();
        transform.Translate(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, 0);
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount;
        if (playerHealth <= 0f)
        {
            Die();
        }
        Debug.Log(amount);
    }
    private void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player Has Died");
    }
}
