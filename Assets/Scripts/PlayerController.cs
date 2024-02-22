using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public int speed;
    public int turnSpeed;
    private Rigidbody rb;
    private Animator animator;
    private float valueForce = 115.0f;
    private float gravityValue = 20f;
    private bool isOnTheGround = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityValue;
    }

    // Update is called once per frame
    void Update()
    {
        speed = 80;
        turnSpeed = 50;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround)
        {
            rb.AddForce(Vector3.up * valueForce, ForceMode.Impulse);
            isOnTheGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isOnTheGround = true;
    }
}
