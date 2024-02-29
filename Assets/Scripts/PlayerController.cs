using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string IsDying = "IsDying";
    private const string IsFainting = "IsFainting";
    private const string IsJumping = "IsJumping";
    private const string IsBending = "IsBending";
    private const string Exit = "Exit";
    public float horizontalInput;
    public float verticalInput;
    public int speed;
    public int turnSpeed;
    private Rigidbody rb;
    [SerializeField] private Collider capsuleCollider;
    private Collider capsuleCollider2;
    private Collider sphereCollider;
    private Animator animator;
    private float valueForce = 115.0f;
    private float gravityValue = 20f;
    private bool isOnTheGround = true;
    public int control;
    private bool isAlive = true;
    private int health;
    private bool isControl = true;

    void Start()
    {
        control = 0;
        rb = GetComponent<Rigidbody>();
        sphereCollider = transform.GetChild(2).transform.GetChild(0).GetComponent<SphereCollider>();
        capsuleCollider2 = transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<CapsuleCollider>();
        animator = this.gameObject.GetComponent<Animator>();
        Physics.gravity *= gravityValue;
        health = 3;
    }

    void Update()
    {
        speed = 120;
        turnSpeed = 70;
        horizontalInput = Input.GetAxis("Horizontal");

        
        if (isAlive)
        {
            
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
           
            if (Input.GetKeyDown(KeyCode.Space) && isOnTheGround)
            {
                isControl = false;
                isOnTheGround = false;
                capsuleCollider.enabled = false;
                sphereCollider.enabled = true;
                animator.SetBool(IsJumping, true);
                rb.AddForce(Vector3.up * valueForce, ForceMode.Impulse);
                Invoke("NotJumping", 1f);
            }
          
            if (Input.GetKeyDown(KeyCode.DownArrow) && isOnTheGround)
            {
                isControl = false;
                capsuleCollider.enabled = false;
                capsuleCollider2.enabled = true;
                animator.SetBool(IsBending, true);
                Invoke("NotBending", 1f);
            }
           
            
            
        }
       
    }
    void NotBending()
    {
        capsuleCollider.enabled = true;
        capsuleCollider2.enabled = false;
        animator.SetBool(IsBending, false);
        isControl = true;
    }
    void NotJumping()
    {
        
        capsuleCollider.enabled = true;
        sphereCollider.enabled = false;
        animator.SetBool(IsJumping, false);
        isControl = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
          
        }
        //for some obstacles
        if (collision.gameObject.CompareTag("Die"))
        {
            health -= 1;
            if (health > 0)
            {
                isAlive = false;
                animator.SetBool(IsFainting, true);
                animator.SetBool(Exit, true);
            }
            if (health == 0)
            {
                isAlive = false;
                animator.SetBool(IsDying, true);
                animator.SetBool(Exit, true);
            }
        }
        if (collision.gameObject.CompareTag("Sarsýlma"))
        {
            //kýrmýzý alarm
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Prefab"))
        {
            other.gameObject.SetActive(false);
        }
       
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //for other obstacles
        if (other.gameObject.CompareTag("Die2"))
        {
            health -= 1;
            if (health > 0)
            {
                isAlive = false;
                animator.SetBool(IsFainting, true);
                animator.SetBool(Exit, true);
            }
            if (health == 0)
            {
                isAlive = false;
                animator.SetBool(IsDying, true);
                animator.SetBool(Exit, true);
            }
            
        }
        if (other.gameObject.CompareTag("Sarsýlma"))
        {
            health -= 1;
            if (health > 0)
            {
                isAlive = false;
                animator.SetBool(IsFainting, true);
                animator.SetBool(Exit, true);
            }
            if (health == 0)
            {
                isAlive = false;
                animator.SetBool(IsDying, true);
                animator.SetBool(Exit, true);
            }
        }
        if (isControl == true && other.gameObject.CompareTag("Road"))
        {
            control += 1;
            Debug.Log(control);
        }


    }
}
