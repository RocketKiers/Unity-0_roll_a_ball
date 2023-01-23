using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool isgrounded = true;
    public float speed = 20;
    public float jumpForce = 7;
    public TextMeshProUGUI countText;
    public GameObject TextWinObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    void Start()
    {
        Debug.Log("Start");
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        TextWinObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();


        movementX = movementVector.x;
        movementY = movementVector.y;


    }



    void Update()
    {
    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY); // new vector created
        movement = movement.normalized * speed; //normalizes the vector
        rb.AddForce(movement); //applies the vector to the rigidbody
        if (Input.GetKeyDown("space") && GetComponent<Rigidbody>().transform.position.y <= 1f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump fixed update");
        }
    }


    void SetCountText() // function for coin text and win text
    {
        countText.text = "Coins count: " + count.ToString(); // code to show to coin count top left
        if (count >= 14) //if you have collected all the coins
        {
            TextWinObject.SetActive(true); // win text is visible
        }
    }

    void OnTriggerEnter(Collider other)     // on colliding with another game object
    {
        if (other.gameObject.CompareTag("PickUp")) // if you touch an object with the pickup tag, you're touching a coin
        {
            other.gameObject.SetActive(false); // if you collect the coin it dissapears
            count = count + 1; // add a coin to the coin count
            SetCountText(); // run function that checks if you have all the coins
        }
    }
}