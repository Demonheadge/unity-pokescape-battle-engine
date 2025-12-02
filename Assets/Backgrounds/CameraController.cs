using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Transform[] focusObjects; // Array of objects to focus on
    private int currentFocusIndex = 0;

    private Vector3 zoomInPosition = new Vector3(1f, 0.5f, -10f); // Adjust Z for perspective camera
    private Vector3 zoomOutPosition = new Vector3(0f, 0f, -10f); // Adjust Z for perspective camera
    private float zoomInSize = 1.5f;
    private float zoomOutSize = 2.8f;

    private bool isZoomedIn = false;
    public float zoomTransitionSpeed = 2f; // Speed of the zoom transition
    public float positionTransitionSpeed = 2f; // Speed of the position transition
    private float currentZoomVelocity; // Used for SmoothDamp
    private Vector3 currentPositionVelocity; // Used for SmoothDamp

    private Vector3 targetPosition;
    private float targetSize;

    private float positionThreshold = 0.01f; // Threshold for snapping position
    private float sizeThreshold = 0.01f; // Threshold for snapping orthographic size

    void Start()
    {
        // Initialize target position and size
        targetPosition = mainCamera.transform.position;
        targetSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            ZoomIn();
        }
        else if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            ZoomOut();
        }
        else if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            FocusOnObject();
        }
        else if (isZoomedIn && Keyboard.current.cKey.wasPressedThisFrame)
        {
            ChangeFocusObject();
        }

        // Smoothly transition the orthographic size
        SmoothTransition();
    }

    private void ZoomIn()
    {
        targetPosition = zoomInPosition;
        targetSize = zoomInSize;
        isZoomedIn = true;
    }

    private void ZoomOut()
    {
        targetPosition = zoomOutPosition;
        targetSize = zoomOutSize;
        isZoomedIn = false;
    }

    private void FocusOnObject()
    {
        if (focusObjects.Length > 0 && currentFocusIndex < focusObjects.Length)
        {
            targetPosition = new Vector3(focusObjects[currentFocusIndex].position.x, focusObjects[currentFocusIndex].position.y, mainCamera.transform.position.z);
        }
    }

    private void ChangeFocusObject()
    {
        if (focusObjects.Length > 0)
        {
            currentFocusIndex = (currentFocusIndex + 1) % focusObjects.Length;
            FocusOnObject();
        }
    }

    private void SmoothTransition()
    {
        // Smoothly transition the orthographic size
        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, targetSize, ref currentZoomVelocity, 1f / zoomTransitionSpeed);

        // Snap orthographic size if close enough
        if (Mathf.Abs(mainCamera.orthographicSize - targetSize) < sizeThreshold)
        {
            mainCamera.orthographicSize = targetSize;
        }

        // Smoothly transition the position
        mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, targetPosition, ref currentPositionVelocity, 1f / positionTransitionSpeed);

        // Snap position if close enough
        if (Vector3.Distance(mainCamera.transform.position, targetPosition) < positionThreshold)
        {
            mainCamera.transform.position = targetPosition;
        }
    }
}
