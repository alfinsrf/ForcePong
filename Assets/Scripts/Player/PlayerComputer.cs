using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComputer : MonoBehaviour
{
    public Transform ballTransform;    
    
    public float playerSpeed;

    private Vector2 destination;

    // Update is called once per frame
    void Update()
    {
        if (ballTransform != null)
        {
            PlayerComputerMovement();
            CheckLimit();
        }
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

    public void PlayerComputerMovement()
    {
        destination = new Vector2(transform.position.x, ballTransform.position.y);
        transform.position = Vector2.Lerp(transform.position, destination, playerSpeed * Time.deltaTime);
    }
}