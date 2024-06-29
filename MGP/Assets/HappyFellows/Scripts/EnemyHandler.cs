using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    
    Rigidbody2D myRigidbody;  //Set reference in inspector
    Animator animator;
    public float FallingThreshold = 0f;  //Adjust in inspector to appropriate value for the speed you want to trigger detecting a fall, probably by just testing
    bool isFalling = false;
    public bool isFallen = false;

    Scorer _GameLogic; 
    Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _GameLogic = GameObject.Find("GameLogic").GetComponent<Scorer>();
        cam = Camera.main;
    }
    
    void Update()
    {
        if (myRigidbody.velocity.y < FallingThreshold || isFallen)
        {
            Falling();
        }
        else
        {
            if(!isFallen){
                isFalling = false;
                animator.SetBool("isFalling", isFalling);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(!isFallen){
            if(other.gameObject.tag == "Ground")
            {
                Fallen();
            }
        }
    }

    private void Fallen()
    {
        gameObject.tag = "FallenEnemy";
        isFallen = true;
        _GameLogic.AddPoints(1);
        Invoke("DestroyMe", 5f);
    }

    void OnBecameInvisible()
    {
        if(!isFallen){
            Debug.Log("Out of View");
            Fallen();
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void Falling()
    {
        animator.SetBool("isFalling", true);
    }
}
