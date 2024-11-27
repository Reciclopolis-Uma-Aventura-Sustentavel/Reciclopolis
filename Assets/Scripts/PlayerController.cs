using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    public Animator playerAnimator;

    private bool isWalking;

    private bool playerFacingRight = true;

    private int jabCount;

    private float crosstempo = 0.5f;


    
    void Start()
    {
        // Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (playerDirection.x != 0 || playerDirection.y != 0) {
            isWalking = true;
        }

        else {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);

    }

    void Update()
    {

        PlayerMove();
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isWalking)
            {
                if (jabCount < 2)
                {
                    PlayerJab();
                    jabCount++;
                    if (!cc_bool)
                    {
                        StartCoroutine(CrossController());
                    }
                }

                else if (jabCount >= 2)
                {
                    PlayerCross();
                    jabCount = 0;
                }
            }
        } StopCoroutine(CrossController());

    }

    void PlayerMove()
    {
        // Pega a entrada do jogador, e cria um Vector2 para usar no playerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        else if (playerDirection.x > 0 && !playerFacingRight) {

            Flip();

        }



    }

    void UpdateAnimator() {

        playerAnimator.SetBool("is_walking", isWalking);
    
    }

    void Flip()
    {

        playerFacingRight = !playerFacingRight;
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        playerAnimator.SetTrigger("press_jab");
        

    }

    void PlayerCross()
    {
        playerAnimator.SetTrigger("press_cross");

    }


    private bool cc_bool;
    IEnumerator CrossController()
    {
        cc_bool = true;
        
        yield return new WaitForSeconds(crosstempo);

        jabCount = 0;

        cc_bool = false;
    }

}
