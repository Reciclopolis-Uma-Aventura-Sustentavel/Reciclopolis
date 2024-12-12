using UnityEngine;

public class DogController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalForce;
    private float verticalForce;

    private bool isWalking;

    private bool isDead;

    public float dogSpeed = 0.3f;
    private float currentSpeed;

    public bool facingRight;
    public bool previousDirectionRight;
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<PlayerController>().transform;
        currentSpeed = dogSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        // Direção do olhar

        if (target.position.x < this.transform.position.x && !isDead)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }

        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        // Animação de walking

        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        // Atualiza o animator
        UpdateAnimator();

    }

    private void FixedUpdate()
    {
        Vector3 targetDistance = target.position - this.transform.position;

        horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);
        verticalForce = targetDistance.y / Mathf.Abs(targetDistance.y);


        // Distância entre o dog e o player máxima
        if (Mathf.Abs(targetDistance.x) < 1f && Mathf.Abs(targetDistance.y) < 0.5f)
        {
            horizontalForce = 0;
            verticalForce = 0;
        }

        if (Mathf.Abs(targetDistance.y) <= 0.3f) { 
            verticalForce = 0;
        }

        // Velocidade do dog
        rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);


    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }
}
