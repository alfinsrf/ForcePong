using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public float playerSpeed;

    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CheckLimit();
    }

    private void CheckLimit()
    {
        var playerPosition = transform.position;

        if (playerPosition.y > 2.22f)
        {
            playerPosition.y = 2.22f;
        }
        else if (playerPosition.y < -6.11f)
        {
            playerPosition.y = -6.11f;
        }

        transform.position = playerPosition;
    }

    private void PlayerMovement()
    {
        float playerVerticalInput = 0;

        if (Input.GetKey(moveUp))
        {
            playerVerticalInput = 1;
        }
        else if (Input.GetKey(moveDown))
        {
            playerVerticalInput = -1;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        rb.velocity = new Vector2(rb.velocity.x, playerVerticalInput * playerSpeed);
    }
}
