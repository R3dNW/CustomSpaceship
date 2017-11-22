using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildModeSpaceshipController : SpaceshipController
{
    public GameObject comIndicator;

    protected override void Update ()
    {
        this.UpdateCOMIndicator();
    }

    public void UpdateCOMIndicator()
    {
        Vector3 comIndicatorPos = GetWorldCentreOfMass();
        comIndicatorPos.z = -1;

        this.comIndicator.transform.position = comIndicatorPos;
    }
}
