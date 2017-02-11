using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int inventoryLeft;

    public float moveSpeed = 5;
    public int inventorySpace = 3;

    public Text inventoryText;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inventoryLeft = inventorySpace;
        SetInventoryText();
	}
	
	void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveHorizontal, moveVertical) * moveSpeed; // * Time.deltaTime?

        rb2d.velocity = move;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Junk"))
        {
            if (inventoryLeft > 0)
            {
                GameObject.Destroy(other.gameObject);
                inventoryLeft--;
                SetInventoryText();
            }
        }
        if(other.CompareTag("GarbageCan"))
        {
            inventoryLeft = inventorySpace;
            SetInventoryText();
        }
    }

    private void SetInventoryText()
    {
        inventoryText.text = inventoryLeft + " inventory spaces left";
    }
}
