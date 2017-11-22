using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsTrayManager : MonoBehaviour
{
    public GameObject partIconPrefab;

    Dictionary<Part, UIPartController> partUIMap;

    void Start()
    {
        this.partUIMap = new Dictionary<Part, UIPartController>();

        foreach(Part part in PartManager.GetEnumerable())
        {
            GameObject go = GameObject.Instantiate(partIconPrefab, this.transform);

            UIPartController controller = go.GetComponent<UIPartController>();

            controller.SetPart(part);

            this.partUIMap.Add(part, controller);
        }
    }

    public void SelectPart(Part part)
    {
        if (BuildModeInputController.Instance.SelectedPart != null)
        {
            this.partUIMap[BuildModeInputController.Instance.SelectedPart].Deselect();
        }

        BuildModeInputController.Instance.SelectPart(part);
    }
}
