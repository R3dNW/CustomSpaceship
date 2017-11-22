using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PartPlacementIndicatorController : MonoBehaviour
{
    Part part;

    GameObject partVisuals;

    public void SetPart(Part part, int x, int y, int rotation)
    {
        if (part == null)
        {
            return;
        }

        if (this.part != null && this.part.name == part.name)
        {
            this.partVisuals.transform.rotation = Quaternion.Euler(0, 0, 90 * rotation);
            this.partVisuals.transform.position = (Vector2)this.transform.position + BuildModeInputController.Instance.WorldSpaceMousePos.Round();
            return;
        }


        this.part = part;

        if (this.partVisuals != null)
        {
            Destroy(this.partVisuals);
        }

        this.partVisuals =
            GameObject.Instantiate(
                part.prefab,
                (Vector2)this.transform.position + new Vector2(x, y),
                Quaternion.Euler(0, 0, 90 * rotation),
                this.transform);

        foreach (SpriteRenderer image in this.partVisuals.GetComponentsInChildren<SpriteRenderer>())
        {
            image.color *= new Color(1, 1, 1, 0.5f);
        }
    }
}