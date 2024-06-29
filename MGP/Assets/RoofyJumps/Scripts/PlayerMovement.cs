using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;

    bool falling = false;

    private InputActions playerControls;

    public GameObject PauseScreen;

    float moveX;
    // Start is called before the first frame update
    Animator animator;

    void Awake()
    {
        playerControls = new InputActions();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseScreen.activeSelf){
            ProcessInput();
            CheckDirectionY();
        }
    }

    private void ProcessInput()
    {
        float inputMethod = 0;
        if (Application.isMobilePlatform)
        {
            if (playerControls.RoofyJumps.MoveLeft.IsPressed())
            {
                inputMethod = -1f;
            }
            else if (playerControls.RoofyJumps.MoveRight.IsPressed())
            {
                inputMethod = 1f;
            }

        }
        else
        {
            inputMethod = Input.GetAxis("Horizontal");
        }

        moveX = inputMethod * moveSpeed;
    }

    private void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
        if(moveX > 0){
            gameObject.transform.localScale = new Vector2(1.6f,2.25f);;
        } else if(moveX < 0){
            gameObject.transform.localScale = new Vector2(-1.6f,2.25f);
        }
    }

    void CheckDirectionY()
    {
        if(rb.velocity.y > 0){
            falling = false;
        }
        else {
            falling = true;
        }
        animator.SetBool("Falling", falling);
    }

}
