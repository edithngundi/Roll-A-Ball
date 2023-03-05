using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;

    // Total time for the game in seconds
    public float totalTime = 30.0f;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool isGameOver = false;
    private float timeRemaining;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        timeRemaining = totalTime;
        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Update the time remaining
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                // Time has run out, the player has lost
                SetGameOverText("You Lose");
            }

            // Update the count and timer text
            SetCountText();
        }
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            if (count >= 12)
            {
                // The player has collected all pickups within the time frame, they win
                SetGameOverText("You Win");
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "\nTime: " + Mathf.RoundToInt(timeRemaining).ToString();
    }

    void SetGameOverText(string gameOverText)
    {
        isGameOver = true;
        countText.text = gameOverText;
        speed = 0;
    }
}
