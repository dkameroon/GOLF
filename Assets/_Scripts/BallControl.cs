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
        if (!isSwiping && rb.velocity.magnitude < standingThreshold && Time.timeScale != 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swipeStartPos = Input.mousePosition;
                isSwiping = true;
                trajectoryLineRenderer.enabled = true;
                trajectoryLineRenderer.positionCount = 20;
            }
            
        }
        
        if (rb.velocity.magnitude < standingThreshold ){
            GameUI.Instance.text.gameObject.SetActive(true);
            GameUI.Instance.text.text = "Shot";
        }
        else
        {
            GameUI.Instance.text.gameObject.SetActive(false);
        }
        
        if (isSwiping)
        {
            Vector3 swipeEndPos = Input.mousePosition;
            Vector3 swipeDirection = swipeEndPos - swipeStartPos;
            float swipeLength = swipeDirection.magnitude;

            float normalizedSwipeLength = Mathf.Clamp01((swipeLength - minSwipeDistance) / (maxSwipeDistance - minSwipeDistance));
            float shotPower = normalizedSwipeLength * maxSwipeForce;
            PowerBarUI.Instance.PowerBar.fillAmount = shotPower / maxSwipeForce;

            Vector3 forceDirection = new Vector3(-swipeDirection.x, 0, -swipeDirection.y).normalized;

            float trajectoryLength = minSwipeDistance + (maxSwipeDistance - minSwipeDistance) * normalizedSwipeLength * trajectoryLengthScalingFactor;

            Vector3 trajectoryEndpoint = transform.position + forceDirection * trajectoryLength;

            trajectoryLineRenderer.SetPosition(0, transform.position);
            trajectoryLineRenderer.SetPosition(1, trajectoryEndpoint);

            if (Input.GetMouseButtonUp(0))
            {
                trajectoryLineRenderer.enabled = false;
                
                rb.AddForce(forceDirection * shotPower, ForceMode.Impulse);
                PowerBarUI.Instance.PowerBar.fillAmount = 0;
                isSwiping = false;
                if (Time.timeScale > 0)
                {
                    OnShooting?.Invoke(this,EventArgs.Empty);
                    SoundManager.Instance.PlayHitSound(transform.position,1f);
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
