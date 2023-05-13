using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed; // the initial speed of the player
    public float turnSpeed; // the speed of turning
    public TextMeshProUGUI scoreText; // the text object for displaying the score
    public Vector3 startingPosition; // the starting position of the player
    private Rigidbody rb;
    private float horizontalMove;
    private float verticalMove;
    private int score;
    private Vector3 checkpoint; // the last checkpoint the player touched

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        SetScoreText();
        startingPosition = transform.position; // set the starting position
        checkpoint = startingPosition; // set the initial checkpoint to the starting position
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal"); // get input for horizontal movement
        verticalMove = Input.GetAxis("Vertical"); // get input for vertical movement
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalMove, 0.0f, verticalMove); // create a vector for the movement direction

        rb.AddForce(movement * speed); // apply force to the rigidbody to move the player

        if (rb.position.y < -10) // if the player falls off the ground
        {
            Respawn();
        }

        if (rb.position.y > 17) // if the player goes too high
        {
            Respawn();
        }

        speed += 0.1f * Time.fixedDeltaTime; // increase speed as the player moves across the ground

        if (horizontalMove != 0f) // if the player is moving horizontally
        {
            float turn = horizontalMove * turnSpeed * Time.fixedDeltaTime; // calculate the turn amount
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f); // create a Quaternion for the turn
            rb.MoveRotation(rb.rotation * turnRotation); // apply the turn to the rigidbody rotation
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup")) // if the player collides with a pickup
        {
            other.gameObject.SetActive(false); // deactivate the pickup
            score++; // increment the score
            SetScoreText(); // update the score text
        }
        else if (other.gameObject.CompareTag("Checkpoint")) // if the player collides with a checkpoint
        {
            checkpoint = other.transform.position; // set the new checkpoint position
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // set the score text to the current score
    }

    void Respawn()
    {
        transform.position = checkpoint; // set the player position to the last checkpoint position
        rb.velocity = Vector3.zero; // reset the player velocity
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpoint = position;

    }
}