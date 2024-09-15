using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{
    public static BallControl Instance { get; private set; }
    public event EventHandler OnShooting;
    public event EventHandler OnLevelComplete;
    
    private Rigidbody rb;
    private Vector3 swipeStartPos;
    private bool isSwiping = false;
    

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float minSwipeDistance = 1.0f;
    [SerializeField] private float maxSwipeDistance = 5.0f;
    [SerializeField] private float maxSwipeForce = 15.0f;
    [SerializeField] private float standingThreshold = 0.01f;
    [SerializeField] private LineRenderer trajectoryLineRenderer;
    [SerializeField] private float trajectoryLengthScalingFactor = 1.0f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = 5.0f;
        rb.maxAngularVelocity = 10.0f;
        CameraFollow.instance.SetTarget(ballPrefab);
        trajectoryLineRenderer.enabled = false;
    }

    void Update()
    {
        HandleSwipeInput();
    }

    private void HandleSwipeInput()
{
    // Ensure the ball is stationary before allowing swipes
    if (!isSwiping && rb.velocity.magnitude < standingThreshold && Time.timeScale != 0)
    {
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPos = Input.mousePosition;  // Record the screen space position where the swipe starts
            isSwiping = true;
            trajectoryLineRenderer.enabled = true;
            trajectoryLineRenderer.positionCount = 20;
        }
    }

    // Show the UI that indicates you can shoot
    if (rb.velocity.magnitude < standingThreshold)
    {
        GameUI.Instance.textYouCanShoot.gameObject.SetActive(true);
        GameUI.Instance.textYouCanShoot.text = "You can hit!";
    }
    else
    {
        GameUI.Instance.textYouCanShoot.gameObject.SetActive(false);
    }

    // Handle the swipe movement
    if (isSwiping)
    {
        Vector3 swipeEndPos = Input.mousePosition;  // Get the current swipe end position (in screen space)
        Vector3 swipeDirection = swipeEndPos - swipeStartPos;  // Calculate the swipe direction in screen space
        float swipeLength = swipeDirection.magnitude;  // Calculate the length of the swipe

        // Normalize swipe length to a value between 0 and 1
        float normalizedSwipeLength = Mathf.Clamp01((swipeLength - minSwipeDistance) / (maxSwipeDistance - minSwipeDistance));
        float shotPower = normalizedSwipeLength * maxSwipeForce;  // Calculate power of the shot
        PowerBarUI.Instance.PowerBar.fillAmount = shotPower / maxSwipeForce;

        // Invert the swipe direction
        Vector3 worldSwipeDirection = new Vector3(-swipeDirection.x, 0, -swipeDirection.y);  // Invert X and Y to reverse direction
        worldSwipeDirection = Camera.main.transform.TransformDirection(worldSwipeDirection);  // Convert to world space direction
        worldSwipeDirection.y = 0;  // We want to keep the movement flat on the XZ plane

        // Calculate the trajectory length
        float trajectoryLength = minSwipeDistance + (maxSwipeDistance - minSwipeDistance) * normalizedSwipeLength * trajectoryLengthScalingFactor;

        // Calculate the trajectory endpoint
        Vector3 trajectoryEndpoint = transform.position + worldSwipeDirection.normalized * trajectoryLength;

        // Set trajectory line renderer points
        trajectoryLineRenderer.positionCount = 2;
        trajectoryLineRenderer.SetPosition(0, transform.position);
        trajectoryLineRenderer.SetPosition(1, trajectoryEndpoint);

        // Apply the force when the mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            trajectoryLineRenderer.enabled = false;  // Hide the trajectory line

            rb.AddForce(worldSwipeDirection.normalized * shotPower, ForceMode.Impulse);  // Add force in inverted direction
            PowerBarUI.Instance.PowerBar.fillAmount = 0;  // Reset power bar
            isSwiping = false;  // End swipe

            if (Time.timeScale > 0)
            {
                OnShooting?.Invoke(this, EventArgs.Empty);  // Trigger OnShooting event
                SoundManager.Instance.PlayHitSound(transform.position, 1f);  // Play sound
            }
        }
    }
}



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slope"))
        {
            rb.angularDrag = 0f;
            rb.maxAngularVelocity = 10f;
            rb.drag = 0f;
        }
        else if (other.CompareTag("Ground"))
        {
            rb.angularDrag = 5.0f;
            rb.maxAngularVelocity = 10.0f;
        }
        else
        {
            GameManager.Instance.Victory();
        }
        
    }
}
