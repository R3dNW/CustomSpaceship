using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildModeInputController : InputController
{
    public new static BuildModeInputController Instance
    {
        get
        {
            return InputController.Instance as BuildModeInputController;
        }
    }

    public Part SelectedPart { get; protected set; }

    Action UpdateMode { get; set; }

    private int partPlacementX;
    private int partPlacementY;
    private int partPlacementRotation;

    public PartPlacementIndicatorController placementIndicator;

    protected override void Start()
    {
        base.Start();

        this.UpdateMode = this.Update_MouseModeSelector;
    }

    protected override void Update()
    {
        this.GetMouseInfo();

        this.UpdateMode();

        this.GetMouseInfo();

        this.lastMousePos = this.MousePos;
        this.lastWorldSpaceMousePos = this.WorldSpaceMousePos;
    }

    protected override void GetMouseInfo()
    {
        base.GetMouseInfo();

        this.partPlacementX = Mathf.RoundToInt(WorldSpaceMousePos.x);
        this.partPlacementY = Mathf.RoundToInt(WorldSpaceMousePos.y);
    }

    void Update_MouseModeSelector ()
    {
		if (Input.GetMouseButton(1) && Vector3.SqrMagnitude(this.lastMousePos -this. MousePos) > 4)
        {
            this.UpdateMode = this.Update_CameraDrag;

            this.Update_CameraDrag();
        }

        if (this.partPlacementX < 0 || this.partPlacementX >= this.spaceship.Data.MaxWidth || this.partPlacementY < 0 || this.partPlacementY >= this.spaceship.Data.MaxHeight)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0)
            && (EventSystem.current.IsPointerOverGameObject() == false)
            && (this.SelectedPart != null)
            && (this.spaceship.Data.BuiltParts[this.partPlacementX, this.partPlacementY].part == null))
        {
            this.spaceship.AddPart(this.partPlacementX, this.partPlacementY, this.partPlacementRotation, this.SelectedPart);
        }

        if (Input.GetMouseButtonUp(1)
            && (EventSystem.current.IsPointerOverGameObject() == false)
            && (this.spaceship.Data.BuiltParts[partPlacementX, this.partPlacementY].part != null))
        {
            this.spaceship.RemovePart(this.partPlacementX, this.partPlacementY);
        }

        if ((EventSystem.current.IsPointerOverGameObject() == false)
            && (this.spaceship.Data.BuiltParts[this.partPlacementX, this.partPlacementY].part == null))
        {
            this.placementIndicator.gameObject.SetActive(true);

            this.placementIndicator.SetPart(BuildModeInputController.Instance.SelectedPart, this.partPlacementX, this.partPlacementY, this.partPlacementRotation);
        }
        else
        {
            this.placementIndicator.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.partPlacementRotation = (this.partPlacementRotation + 1) % 4;
        }
    }

    void Update_CameraDrag()
    {
        if (Input.GetMouseButtonUp(1))
        {
            ExitMouseMode();
        }
        
        Vector2 worldSpaceToMove = this.lastWorldSpaceMousePos - this.WorldSpaceMousePos;

        this.cameraRig.transform.Translate(worldSpaceToMove);
    }

    void ExitMouseMode()
    {
        this.UpdateMode = this.Update_MouseModeSelector;
    }

    public void SelectPart(Part part)
    {
        this.SelectedPart = part;
    }
}
