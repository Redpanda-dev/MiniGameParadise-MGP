using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{

    [SerializeField] private float detachDelay = 0.2f;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float respawnDelay = 0.5f;

    private Camera mainCamera;
    private bool isDragging;
    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;

    [SerializeField] GameObject PauseWindow;
    [SerializeField] GameObject VictoryWindow;
    [SerializeField] GameObject GameOverWindow;

    Scorer _GameLogic;
    AudioHandler _audioHandler;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        _GameLogic = GameObject.Find("GameLogic").GetComponent<Scorer>();
        _audioHandler = GameObject.FindGameObjectWithTag("AudioHandle").GetComponent<AudioHandler>();
        SpawnNewBall();
    }
    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentBallRigidbody == null) { return; }
        if(!PauseWindow.activeSelf && !VictoryWindow.activeSelf && !GameOverWindow.activeSelf) {
            ProcessTouch();
        }
    }

    void ProcessTouch()
    {
        if (Touch.activeTouches.Count == 0) 
        { 
            if(isDragging)
            {
                LaunchBall();
            }
            isDragging = false;
            
            return; 
        }

        Vector2 touchPosition = new Vector2();

        foreach(Touch touch in Touch.activeTouches){
                touchPosition += touch.screenPosition;
                Debug.Log(touchPosition);
        } 

        touchPosition /= Touch.activeTouches.Count;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        if(worldPosition.y < -4.30f || worldPosition.x > -2.98f) {
            return;
        }

        isDragging = true;
        currentBallRigidbody.isKinematic = true;

        currentBallRigidbody.position = worldPosition;
        
    }

    public void SpawnNewBall()
    {
        _GameLogic.RemoveHealth(1);
        
        if(_GameLogic.getHealth() > 0){
            GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

            currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
            currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

            currentBallSpringJoint.connectedBody = pivot;
        }
    }

    void LaunchBall()
    {
        int launchCount = PlayerPrefs.GetInt("LaunchCount", 0);
        int newLaunchCount = launchCount+1;
        PlayerPrefs.SetInt("LaunchCount", newLaunchCount);
        
        _audioHandler.PlayNormalJumpSound();
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;

        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;

        Invoke(nameof(SpawnNewBall), respawnDelay);
    }
}
