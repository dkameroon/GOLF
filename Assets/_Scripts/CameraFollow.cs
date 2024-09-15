using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    [SerializeField] private ActiveVectors activeVectors;

    private GameObject followTarget;  // The ball
    private Vector3 offset;
    private Vector3 changePos;

    [SerializeField] private GameObject hole;  // Reference to the hole
    [SerializeField] private float smoothSpeed = 125f;  // Smoothing for camera movement
    [SerializeField] private float distanceAboveBall = 3f;  // Height of the camera above the ball
    [SerializeField] private float distanceBehindBall = 5f;  // Distance of the camera behind the ball
    [SerializeField] private float bottomScreenIndent = 1f;  // Distance from the bottom of the screen

    private void Awake()
    {
        Application.targetFrameRate = 240;
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        followTarget = target;
        offset = new Vector3(0, distanceAboveBall, -distanceBehindBall);  // Set the initial camera offset from the ball
        changePos = transform.position;
    }

    private void LateUpdate()
    {
        if (followTarget)
        {
            // Calculate the target position of the camera
            Vector3 targetPosition = followTarget.transform.position + offset;
            // Adjust the camera's vertical position to keep the ball at the bottom of the screen
            targetPosition.y = followTarget.transform.position.y + bottomScreenIndent;

            // Smooth the camera movement to avoid jitter
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 2. Rotate the camera to look at both the ball and the hole
            if (hole != null)
            {
                // Calculate direction from camera to ball and hole
                Vector3 directionToBall = followTarget.transform.position - transform.position;
                Vector3 directionToHole = hole.transform.position - transform.position;

                // Interpolate the look direction between ball and hole, so both stay in view
                Vector3 combinedLookDirection = Vector3.Lerp(directionToBall, directionToHole, 0.3f);  // Adjust 0.3f to control how much the hole is included
                transform.rotation = Quaternion.LookRotation(combinedLookDirection);
            }
            else
            {
                // If no hole is available, just focus on the ball
                transform.LookAt(followTarget.transform);
            }
        }
    }
}

[System.Serializable]
public class ActiveVectors
{
    public bool x, y, z;
}
