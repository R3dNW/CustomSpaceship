using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SteeringMode
{
    Mouse,
    Keyboard
}

public class PlayModeSpaceshipController : SpaceshipController
{
    public SteeringMode steeringMode;

    float minAngleToMove = 5;

    public void Awake()
    {
        foreach (PlayModePartController pmpCtrl in this.GetComponentsInChildren<PlayModePartController>())
        {
            pmpCtrl.PlayModeAwake();
        }
    }

    public void Start()
    {
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        rb.centerOfMass = this.GetLocalCentreOfMass();
        rb.mass = this.GetTotalMass();

        foreach (PlayModePartController pmpCtrl in this.GetComponentsInChildren<PlayModePartController>())
        {
            pmpCtrl.PlayModeStart();
        }
    }

    public void FixedUpdate()
    {
        List<string> pressedControls = new List<string>();

        if (Input.GetKey(KeyCode.W))
        {
            pressedControls.Add("forward");
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            pressedControls.Add("back");
        }

        if (this.steeringMode == SteeringMode.Keyboard)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                pressedControls.Add("left");
            }

            if (Input.GetKey(KeyCode.A))
            {
                pressedControls.Add("anticlockwise");
            }

            if (Input.GetKey(KeyCode.E))
            {
                pressedControls.Add("right");
            }

            if (Input.GetKey(KeyCode.D))
            {
                pressedControls.Add("clockwise");
            }
        }
        else if (this.steeringMode == SteeringMode.Mouse)
        {
            //float direction = Utilities.AngleDirection(this.transform.up, (InputController.Instance.worldSpaceMousePos - (Vector2)this.GetWorldCentreOfMass()).normalized, Vector3.back);

            float angle = Vector3.SignedAngle(this.transform.up, (InputController.Instance.WorldSpaceMousePos - this.GetWorldCentreOfMass()), Vector3.back);

            if (Mathf.Abs(angle) > this.minAngleToMove)
            {
                if (angle > 0)
                {
                    pressedControls.Add("clockwise");
                }
                else if (angle < 0)
                {
                    pressedControls.Add("anticlockwise");
                }
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                pressedControls.Add("left");
            }

            if (Input.GetKey(KeyCode.D))
            {
                pressedControls.Add("right");
            }
        }

        foreach (PlayModePartController pmpCtrl in this.GetComponentsInChildren<PlayModePartController>())
        {
            pmpCtrl.PlayModeMoveUpdate(pressedControls.ToArray());
        }
    }

    protected override void Update()
    {
        base.Update();

        List<string> pressedControls = new List<string>();

        if (Input.GetMouseButton(0))
        {
            pressedControls.Add("fire_lasers");
        }

        foreach (PlayModePartController pmpCtrl in this.GetComponentsInChildren<PlayModePartController>())
        {
            pmpCtrl.PlayModeActionUpdate(pressedControls.ToArray());
        }
    }
}
