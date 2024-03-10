using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    private const string IsDying = "IsDying";
    private const string IsFainting = "IsFainting";
    private const string IsJumping = "IsJumping";
    private const string IsBending = "IsBending";
    private const float Respawn = 120f;
    private const int MaxSpeed = 200;
    private Animator animator;
    private float horizontalInput;
    public int speed;
    private int turnSpeed;
    private float valueForce;
    public float gravityValue;
    public int control;
    private Rigidbody rb;
    [SerializeField] private Collider capsuleCollider;
    private Collider capsuleCollider2;
    private Collider sphereCollider;
    private Score score;
    private Health health;
    private bool isOnTheGround = true;
    public bool isAlive;
    private bool isControl = true;
    private Vector3 playerPosition;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private StartCountdown gameManager;

    void Start()
    {
        control = 0;
        speed = 120;
        turnSpeed = 70;
        valueForce = 115.0f;
        gravityValue = 20f;
        gameOverPanel.SetActive(false);
        warningPanel.SetActive(false);
        Physics.gravity = new Vector3(0, -9.8f, 0);
        Physics.gravity *= gravityValue;
        animator = this.gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = transform.GetChild(2).transform.GetChild(0).GetComponent<SphereCollider>();
        capsuleCollider2 = transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<CapsuleCollider>();
        score = GameObject.Find("Score").GetComponent<Score>();
        health = GameObject.Find("Health").GetComponent <Health>();
        InvokeRepeating("SpeedUp", 30, 30);
        
    }

    void Update()
    {
        
        playerPosition = transform.position;
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
                AudioManager.instance.PlayJumpingSound();
                animator.SetBool(IsJumping, true);
                rb.AddForce(Vector3.up * valueForce, ForceMode.Impulse);
                Invoke("NotJumping", 1f);
            }
          
            if (Input.GetKeyDown(KeyCode.DownArrow) && isOnTheGround)
            {
                isControl = false;
                capsuleCollider.enabled = false;
                capsuleCollider2.enabled = true;
                AudioManager.instance.PlayBendingSound();
                animator.SetBool(IsBending, true);
                Invoke("NotBending", 1f);
            }
  
        }
        if (health.health < 0)
        {
            health.health = 0;
        }

    }
    void SpeedUp()
    {
        if (speed <= MaxSpeed)
        {
            speed += 10;
            if (speed >= 160)
            {
                animator.SetFloat("runSpeed", 1.4f);
                
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
            DeathControl();
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            score.isScore = false;
            health.health -= 1;
            if (health.health < 0)
            {
                health.health = 0;
            }
            if (health.health > 0)
            {
                AudioManager.instance.PlayDeathSound();
                isAlive = false;
                animator.SetBool(IsFainting, true);
                StartCoroutine(OtherGetAlive());
                collision.gameObject.transform.position += Vector3.forward * 100f;

            }
            if (health.health < 1)
            {
                AudioManager.instance.PlayGameOverSound();
                isAlive = false;
                gameOverPanel.SetActive(true);
                score.CheckAndUpdateHighScore();
                animator.SetBool(IsDying, true);
            }
        }
        if (collision.gameObject.CompareTag("Sarsýlma"))
        {
            Shaking();
        }
        if (collision.gameObject.CompareTag("Car"))
        {
            Shaking();
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
            DeathControl();
        }
        if (other.gameObject.CompareTag("Sarsýlma"))
        {
            DeathControl();
        }
        if (other.gameObject.CompareTag("Car"))
        {
            score.isScore = false;
            health.health -= 1;
            if (health.health < 0)
            {
                health.health = 0;
            }
            if (health.health > 0)
            {
                AudioManager.instance.PlayDeathSound();
                isAlive = false;
                animator.SetBool(IsFainting, true);
                StartCoroutine(OtherGetAlive());
                other.gameObject.transform.position += Vector3.forward * 100f;

            }
            if (health.health < 1)
            {
                AudioManager.instance.PlayGameOverSound();
                isAlive = false;
                gameOverPanel.SetActive(true);
                score.CheckAndUpdateHighScore();
                animator.SetBool(IsDying, true);
            }

        }
        if (other.gameObject.CompareTag("Sarsýlma2"))
        {
            Shaking();
        }
        if (isControl == true && other.gameObject.CompareTag("Road"))
        {
            control += 1;
            
        }
    }
    IEnumerator GetAlive()
    { 
        yield return new WaitForSeconds(3f);
        animator.SetBool(IsFainting, false);
        yield return new WaitForSeconds(1f);
        playerPosition -= Vector3.forward * Respawn;
        transform.position = playerPosition;
        StartCoroutine(gameManager.Countdown());
        score.isScore = true;
        StartCoroutine(score.IncreaseScore());
        yield return new WaitForSeconds(3f);
        isAlive = true;
    }
    IEnumerator OtherGetAlive()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool(IsFainting, false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(gameManager.Countdown());
        score.isScore = true;
        StartCoroutine(score.IncreaseScore());
        yield return new WaitForSeconds(3f);
        isAlive = true;
    }

    IEnumerator WarningPanel()
    {
        for (int i=0; i < 3; i++)
        {
            warningPanel.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            warningPanel.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            
        }      
    }
    void DeathControl()
    {
        score.isScore = false;
        health.health -= 1;
        if (health.health < 0)
        {
            health.health = 0;
        }
        if (health.health > 0)
        {
            AudioManager.instance.PlayDeathSound();
            isAlive = false;
            animator.SetBool(IsFainting, true);
            StartCoroutine(GetAlive());

        }
        if (health.health < 1)
        {
            AudioManager.instance.PlayGameOverSound();
            isAlive = false;
            gameOverPanel.SetActive(true);
            score.CheckAndUpdateHighScore();
            animator.SetBool(IsDying, true);
        }
    }

    void Shaking()
    {
        AudioManager.instance.PlayCrushSound();
        StartCoroutine(WarningPanel());
        health.health2 -= 1;
        if (health.health2 == 0)
        {
            health.health -= 1;
            health.health2 = 2;
        }
        if (health.health < 0)
        {
            health.health = 0;
        }
        if (health.health < 1)
        {
            AudioManager.instance.PlayGameOverSound();
            score.isScore = false;
            isAlive = false;
            gameOverPanel.SetActive(true);
            score.CheckAndUpdateHighScore();
            animator.SetBool(IsDying, true);
        }
    }
    
}
