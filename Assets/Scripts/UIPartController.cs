using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPartController : MonoBehaviour
{
    public Image iconImage;
    public Text nameText;
    public Text amountText;
    public Image selectedIndicator;

    Part part;

    public void Start()
    {
        this.Deselect();
    }

    public void SetPart (Part part)
    {
        this.part = part;

        this.iconImage.sprite = part.icon;
        this.nameText.text = part.displayName;
        this.amountText.text = "1000";

        this.name = part.name + " UI";
	}

    public void Select()
    {
        this.selectedIndicator.gameObject.SetActive(true);

        this.GetComponentInParent<ItemsTrayManager>().SelectPart(this.part);
    }

    public void Deselect()
    {
        this.selectedIndicator.gameObject.SetActive(false);
    }
}
