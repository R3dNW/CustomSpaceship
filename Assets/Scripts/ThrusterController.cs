using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ThrusterController : PlayModePartController
{
    bool active = false;
    float warmedUpFactor = 0;

    public float force = 10;

    public ParticleSystem whiteCore;
    private ParticleSystem.EmissionModule whiteCoreEmission;
    private float whiteCoreEmissionRateMax;
    
    public ParticleSystem innerFlame;
    private ParticleSystem.EmissionModule innerFlameEmission;
    private float innerFlameEmissionRateMax;

    public ParticleSystem outerFlame;
    private ParticleSystem.EmissionModule outerFlameEmission;
    private float outerFlameEmissionRateMax;

    [Range(0, 1)]
    public float warmUpRate;

    public bool activateOnForward = false;
    public bool activateOnLeft = false;             // Move Left
    public bool activateOnAntiClockwise = false;    // Turn Left
    public bool activateOnBack = false;        
    public bool activateOnClockwise = false;        // Turn Right
    public bool activateOnRight = false;            // Move Right

    public override void PlayModeAwake()
    {
        this.whiteCoreEmission = this.whiteCore.emission;
        this.whiteCoreEmissionRateMax = this.whiteCoreEmission.rateOverTime.constant;

        this.innerFlameEmission = this.innerFlame.emission;
        this.innerFlameEmissionRateMax = this.innerFlameEmission.rateOverTime.constant;

        this.outerFlameEmission = this.outerFlame.emission;
        this.outerFlameEmissionRateMax = this.outerFlameEmission.rateOverTime.constant;
    }

    public override void PlayModeStart()
    {
        UpdateFlames();

        Vector3 com = this.spaceship.GetLocalCentreOfMass();
        Vector3 localPosition = this.transform.localPosition;

        Vector3 positionRelativeToCOM = localPosition - com;

        if (this.builtPart.rotation == 0) // Pointing Up
        {
            if (positionRelativeToCOM.x > 0)
            {
                this.activateOnAntiClockwise = true;
            }
            else if (positionRelativeToCOM.x < 0)
            {
                this.activateOnClockwise = true;
            }

            this.activateOnForward = true;
        }
        else if (this.builtPart.rotation == 1) // Pointing Left
        {
            if (positionRelativeToCOM.y > 0)
            {
                this.activateOnAntiClockwise = true;
            }
            else if (positionRelativeToCOM.y < 0)
            {
                this.activateOnClockwise = true;
            }

            this.activateOnLeft = true;
        }
        else if (this.builtPart.rotation == 2) // Pointing Back
        {
            if (positionRelativeToCOM.x > 0)
            {
                this.activateOnClockwise = true;
            }
            else if (positionRelativeToCOM.x < 0)
            {
                this.activateOnAntiClockwise = true;
            }

            this.activateOnBack = true;
        }
        else if (this.builtPart.rotation == 3) // Pointing Right
        {
            if (positionRelativeToCOM.y > 0)
            {
                this.activateOnClockwise = true;
            }
            else if (positionRelativeToCOM.y < 0)
            {
                this.activateOnAntiClockwise = true;
            }

            this.activateOnRight = true;
        }
    }

    public override void PlayModeMoveUpdate(string[] controls)
    {
        this.active = false;

        if (controls.Contains("forward") && activateOnForward)
        {
            Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
            rb.AddForceAtPosition((this.transform.up * this.force), this.transform.position);

            this.active = true;
        }

        if (controls.Contains("left") && activateOnLeft)
        {
            Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
            rb.AddForceAtPosition((this.transform.up * this.force), this.transform.position);

            this.active = true;
        }

        if (controls.Contains("back") && activateOnBack)
        {
            Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
            rb.AddForceAtPosition((this.transform.up * this.force), this.transform.position);

            this.active = true;
        }

        if (controls.Contains("right") && activateOnRight)
        {
            Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
            rb.AddForceAtPosition((this.transform.up * this.force), this.transform.position);

            this.active = true;
        }

        if (controls.Contains("clockwise") && activateOnClockwise)
        {
            Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
            rb.AddForceAtPosition((this.transform.up * this.force), this.transform.position);

            this.active = true;
        }

        if (controls.Contains("anticlockwise") && activateOnAntiClockwise)
        {
            Rigidbody2D rb = this.GetComponentInParent<Rigidbody2D>();
            rb.AddForceAtPosition((this.transform.up * this.force), this.transform.position);

            this.active = true;
        }

        if (this.active && (this.warmedUpFactor < 1))
        {
            this.warmedUpFactor = Mathf.Lerp(this.warmedUpFactor, 1, this.warmUpRate);
        }
        else if ((this.active == false) && (this.warmedUpFactor > 0))
        {
            this.warmedUpFactor = Mathf.Lerp(this.warmedUpFactor, 0, this.warmUpRate);
        }
        else
        {
            return;
        }
        
        UpdateFlames();
    }

    protected void UpdateFlames()
    {
        ParticleSystem.MinMaxCurve rateOverTime;

        rateOverTime = this.whiteCoreEmission.rateOverTime;
        rateOverTime.constant = this.whiteCoreEmissionRateMax * this.warmedUpFactor;
        this.whiteCoreEmission.rateOverTime = rateOverTime;

        rateOverTime = this.innerFlameEmission.rateOverTime;
        rateOverTime.constant = this.innerFlameEmissionRateMax * this.warmedUpFactor;
        this.innerFlameEmission.rateOverTime = rateOverTime;

        rateOverTime = this.outerFlameEmission.rateOverTime;
        rateOverTime.constant = this.outerFlameEmissionRateMax * this.warmedUpFactor;
        this.outerFlameEmission.rateOverTime = rateOverTime;
    }

    public override void PlayModeActionUpdate(string[] actions)
    {
        return;
    }
}
