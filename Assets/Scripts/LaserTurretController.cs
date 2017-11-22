using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LaserTurretController : PlayModePartController
{
    public GameObject turretBase;
    public GameObject turretGun;

    public GameObject laserBoltPrefab;

    private float turretAngle;

    public float cooldownTime = 0.1f;

    private float timeSinceLastShot;

    public override void PlayModeAwake()
    {
    }

    public override void PlayModeStart()
    {
    }

    public override void PlayModeMoveUpdate(string[] controls)
    {
        this.turretAngle = Vector3.SignedAngle(this.turretBase.transform.up, (InputController.Instance.WorldSpaceMousePos - (Vector2)this.turretGun.transform.position), Vector3.back);

        this.turretGun.transform.rotation = this.turretBase.transform.rotation;
        this.turretGun.transform.Rotate(Vector3.back, this.turretAngle);
    }

    public override void PlayModeActionUpdate(string[] actions)
    {
        if (actions.Contains("fire_lasers") && this.timeSinceLastShot <= 0.0f)
        {
            Vector3 position = this.turretGun.transform.position;
            position.z = -2;

            GameObject go = GameObject.Instantiate(this.laserBoltPrefab, position, this.turretGun.transform.rotation);

            this.timeSinceLastShot = this.cooldownTime;
        }

        this.timeSinceLastShot -= Time.deltaTime;
    }
}
