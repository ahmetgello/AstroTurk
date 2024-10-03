using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Vector3 myPosition;
    private Vector3 firstPosition;

    [SerializeField] private Vector3 fallPosition;

    [SerializeField] private float jumpSpeed;

    private Rigidbody2D rb;

    [SerializeField] private GroundCheck groundCheck;

    [SerializeField] private GameObject bombGO;

    [SerializeField] private Transform bulletSpawnPosition;

    private bool playerDirection;

    private SpriteRenderer sr;

    private Animator anim;

    private bool isJumping;

    private bool isHoldingBomb;

    [SerializeField] private GameObject bomb;
    private GameObject bombInHand;

    private bool isThrowing;

    [SerializeField] private BombSpwaner bombSpawner;

    public Joystick movementJoystick;
    [SerializeField] private float playerSpeed;

    [SerializeField] private GameObject shootButton;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip jumpEndSound;
    [SerializeField] private AudioClip jumpSound;

    void Start()
    {
        firstPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        bombSpawner.SpawnBomb();
    }

    void Update()
    {
        myPosition.x = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(myPosition.x * playerSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
           Jump();

        if (Input.GetKeyDown(KeyCode.Mouse0) && isHoldingBomb)
           StartCoroutine(Shoot());

        if (Input.GetKeyDown(KeyCode.D))
        {
           ChangePlayerDirectionToRight();
           playerDirection = true;
           sr.flipX = true; 
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
           ChangePlayerDirectionToLeft();
           playerDirection = false;
           sr.flipX = false;
        }

        if ((myPosition.x > 0.2f || myPosition.x < -0.2f) && !isJumping)
        {
            audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
            if (!audioSource.isPlaying || audioSource.clip == jumpEndSound)
            {
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(audioSource.clip);
            }
    //        audioSource.enabled = true;
            if (!isHoldingBomb && !isThrowing)
                anim.Play("Move");
            else if(!isThrowing)
                anim.Play("BombWalk");
        }
        else if (movementJoystick.Direction.x > 0.01f)
        {
            audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
            if (!audioSource.isPlaying || audioSource.clip == jumpEndSound)
            {
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(audioSource.clip);
            }
            if (!isJumping)
                if (!isHoldingBomb && !isThrowing)
                    anim.Play("Move");
                else if (!isThrowing)
                    anim.Play("BombWalk");

            rb.linearVelocity = new Vector2(movementJoystick.Direction.x * playerSpeed, rb.linearVelocity.y);
            sr.flipX = true;
            ChangePlayerDirectionToRight();
            playerDirection = true;
        }
        else if (movementJoystick.Direction.x < -0.01f)
        {
            audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
            if (!audioSource.isPlaying || audioSource.clip == jumpEndSound)
            {
                audioSource.volume = 0.6f;
                audioSource.PlayOneShot(audioSource.clip);
            }
            if (!isJumping)
                if (!isHoldingBomb && !isThrowing)
                    anim.Play("Move");
                else if (!isThrowing)
                    anim.Play("BombWalk");
            rb.linearVelocity = new Vector2(movementJoystick.Direction.x * playerSpeed, rb.linearVelocity.y);
            sr.flipX = false;
            ChangePlayerDirectionToLeft();
            playerDirection = false;
        }
        else if(myPosition.x == 0 && !isJumping)
        {
   //         audioSource.enabled = false;
            if (!isHoldingBomb && !isThrowing)
                anim.Play("idle_side");
            else if (!isThrowing)
                anim.Play("BombHold");
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < fallPosition.y)
            Respawn();

        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f && isJumping)
        {
            EndJump();
        }
    }

    private void Respawn()
    {
        transform.position = firstPosition;
    }

    public void Jump()
    {
        if(CanJump())
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            anim.Play("Jump");
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(jumpSound);
            /*        mySpeed = 4;*/
        }
    }


    private bool CanJump()
    {
        if (groundCheck.isGrounded)
        {
            return true;
        }
        return false;
    }

    private void EndJump()
    {
        isJumping = false;
        audioSource.volume = 1f;
        audioSource.PlayOneShot(jumpEndSound);
/*        mySpeed = 6;*/
    }

    public void ShootButtonClicked()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if(CanShoot())
        {
            shootButton.SetActive(false);
            isThrowing = true;
            isHoldingBomb = false;
            anim.Play("BombThrow");
            Destroy(bombInHand);
            Rigidbody2D bombRB = Instantiate(bombGO, bulletSpawnPosition.position, Quaternion.identity).GetComponent<Rigidbody2D>();

            if (playerDirection)
                bombRB.AddForce(new Vector2(12, 12), ForceMode2D.Impulse);
            else
                bombRB.AddForce(new Vector2(-12, 12), ForceMode2D.Impulse);

            yield return new WaitForSeconds(1f);
            isThrowing = false;

            yield return new WaitForSeconds(3f);
            bombSpawner.SpawnBomb();
        }
    }

    private bool CanShoot()
    {
        if (isHoldingBomb)
        {
            return true;
        }
        return false;
    }

    private void ChangePlayerDirectionToRight()
    {
        bulletSpawnPosition.localPosition = new Vector3(0.5f, bulletSpawnPosition.localPosition.y, 0);
        if(bombInHand)
            bombInHand.transform.position = bulletSpawnPosition.position;
    }

    private void ChangePlayerDirectionToLeft()
    {
        bulletSpawnPosition.localPosition = new Vector3(-0.5f, bulletSpawnPosition.localPosition.y, 0);
        if(bombInHand)
            bombInHand.transform.position = bulletSpawnPosition.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "bomb")
        {
            HoldABomb();
            Destroy(col.transform.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "alien")
        {
            Respawn();
        }
    }

    private void HoldABomb()
    {
        isHoldingBomb = true;
        bombInHand = Instantiate(bomb, bulletSpawnPosition.position , Quaternion.identity, transform);
        shootButton.SetActive(true);
    }
}
