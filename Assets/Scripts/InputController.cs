using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; protected set; }

    Ray mouseRay;

    public SpaceshipController spaceship;

    public Vector2 MousePos { get; protected set; }
    protected Vector2 lastMousePos;

    public Vector2 WorldSpaceMousePos { get; protected set; }
    protected Vector2 lastWorldSpaceMousePos;

    public CameraRig cameraRig;
    
    public float minCameraZoom = 2;
    public float maxCameraZoom = 10;

    public float zoomSpeed = 1;

    private float currentZoom = 5;

    protected virtual void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("Two BuildModeInputControllers in Scene");
        }

        Instance = this;

        if (this.cameraRig == null)
        {
            this.cameraRig = Camera.main.GetComponentInParent<CameraRig>();
        }
    }

    protected virtual void Update()
    {
        this.GetMouseInfo();

        this.Update_CameraZoom();

        this.GetMouseInfo();
    }

    protected virtual void GetMouseInfo()
    {
        this.MousePos = Input.mousePosition;

        this.mouseRay = Camera.main.ScreenPointToRay(this.MousePos);

        Plane plane = new Plane(Vector3.back, Vector3.zero);

        float distance;

        if (plane.Raycast(this.mouseRay, out distance))
        {
            this.WorldSpaceMousePos = this.mouseRay.GetPoint(distance);
        }
    }

    void Update_CameraZoom()
    {
        float zoomAmount = this.zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        this.currentZoom = Mathf.Clamp((this.currentZoom - (zoomAmount * this.currentZoom)), this.minCameraZoom, this.maxCameraZoom);

        foreach (Camera camera in this.cameraRig.cameras)
        {
            camera.orthographicSize = this.currentZoom;
        }
    }
}