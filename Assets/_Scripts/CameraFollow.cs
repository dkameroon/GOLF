using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    [SerializeField] private ActiveVectors activeVectors;

    private GameObject followTarget;  
    private Vector3 offset;
    private Vector3 changePos;

    [SerializeField] private GameObject hole;  
    [SerializeField] private float smoothSpeed = 0.125f;  
    [SerializeField] private float distanceAboveBall = 3f;  
    [SerializeField] private float distanceBehindBall = 5f; 
    [SerializeField] private float bottomScreenIndent = 1f; 
    [SerializeField] private float minFieldOfView = 30f;  
    [SerializeField] private float maxFieldOfView = 60f;  
    [SerializeField] private float fieldOfViewScalingFactor = 0.1f;  

    private Camera cameraComponent;

    private void Awake()
    {
        Application.targetFrameRate = 240;
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        cameraComponent = GetComponent<Camera>();
        if (cameraComponent == null)
        {
            Debug.LogError("No Camera component found on the CameraFollow GameObject. Please add a Camera component.");
        }
    }

    public void SetTarget(GameObject target)
    {
        followTarget = target;
        offset = new Vector3(0, distanceAboveBall, -distanceBehindBall);
        changePos = transform.position;
    }

    private void LateUpdate()
    {
        if (followTarget)
        {
            Vector3 targetPosition = followTarget.transform.position + offset;
            targetPosition.y = followTarget.transform.position.y + bottomScreenIndent;
            
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            if (hole != null)
            {
                Vector3 directionToBall = followTarget.transform.position - transform.position;
                Vector3 directionToHole = hole.transform.position - transform.position;

                if (cameraComponent != null)
                {
                    float distanceToHole = Vector3.Distance(transform.position, hole.transform.position);
                    float targetFieldOfView = Mathf.Lerp(minFieldOfView, maxFieldOfView, distanceToHole * fieldOfViewScalingFactor);
                    cameraComponent.fieldOfView = Mathf.Clamp(targetFieldOfView, minFieldOfView, maxFieldOfView);
                }
                
                Vector3 combinedLookDirection = Vector3.Lerp(directionToBall, directionToHole, 0.1f);
                transform.rotation = Quaternion.LookRotation(combinedLookDirection);
            }
            else
            {
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
