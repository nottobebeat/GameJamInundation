using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private int inventoryLeft;
    private Animator animator;

    public float moveSpeed = 5;
    public int inventorySpace = 3;

    public Text inventoryText;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        inventoryLeft = inventorySpace;
        SetInventoryText();
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveHorizontal, moveVertical) * moveSpeed;
        Quaternion rotation = gameObject.transform.rotation;
        if(move.x != 0 || move.y != 0)
            animator.SetBool("PlayerWalking", true);
        else
            animator.SetBool("PlayerWalking", false);
        if (move.x < 0)
            rotation.y = 180;
        else if (move.x > 0)
            rotation.y = 0;


        gameObject.transform.rotation = rotation;
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
                GameManager.instance.SubtractTrash();
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
