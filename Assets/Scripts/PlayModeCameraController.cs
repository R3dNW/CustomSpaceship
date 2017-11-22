using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeCameraController : MonoBehaviour
{
    public SpaceshipController spaceship;

    [Range(0, 1)]
    public float cameraSpeed = 0.1f;

    void FixedUpdate()
    {
        Vector3 targetPosition = this.spaceship.transform.position + (this.spaceship.transform.rotation * (Vector3)this.spaceship.GetLocalCentreOfMass());

        targetPosition.z = -10;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, this.cameraSpeed);
    }
}
