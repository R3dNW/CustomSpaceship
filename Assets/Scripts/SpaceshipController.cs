using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuiltPart
{
    [System.NonSerialized]
    public Part part;
    
    public string PartName
    {
        get
        {
            if (this.part == null)
            {
                return null;
            }

            return this.part.name;
        }
    }

    public int x;
    public int y;
    public int rotation;

    public BuiltPart(int x, int y, int rotation, Part part)
    {
        this.part = part;
        this.x = x;
        this.y = y;
        this.rotation = rotation;
    }
}

public class SpaceshipData
{
    public int MaxWidth { get; private set; }
    public int MaxHeight { get; private set; }

    public BuiltPart[,] BuiltParts { get; set; }

    public SpaceshipData(int maxWidth, int maxHeight)
    {
        this.MaxWidth = maxWidth;
        this.MaxHeight = maxHeight;
        this.BuiltParts = new BuiltPart[maxWidth, maxHeight];

        for (int x = 0; x < this.MaxWidth; x++)
        {
            for (int y = 0; y < this.MaxHeight; y++)
            {
                this.BuiltParts[x, y].x = x;
                this.BuiltParts[x, y].y = y;
            }
        }
    }

    public SpaceshipData(int maxWidth, int maxHeight, BuiltPart[,] parts)
    {
        this.MaxWidth = maxWidth;
        this.MaxHeight = maxHeight;
        this.BuiltParts = parts;
    }
}

public abstract class SpaceshipController : MonoBehaviour
{
    public SpaceshipData Data { get; protected set; }
    
    public int _maxWidth = 10;
    public int _maxHeight = 10;

    protected GameObject[,] partVisuals;

    void Start()
    {
        if (this.Data == null)
        {
            this.Data = new SpaceshipData(this._maxWidth, this._maxHeight);
            this.partVisuals = new GameObject[this.Data.MaxWidth, this.Data.MaxHeight];
        }
    }

    protected virtual void Update()
    {
    }

    public Vector2 GetLocalCentreOfMass()
    {
        float totalMass = 0;
        Vector2 accumulated = Vector2.zero;

        for (int x = 0; x < this.Data.MaxWidth; x++)
        {
            for (int y = 0; y < this.Data.MaxHeight; y++)
            {
                Part part = this.Data.BuiltParts[x, y].part;

                if (part != null)
                {
                    totalMass += part.mass;
                    accumulated += (part.mass * new Vector2(x, y));
                }
            }
        }

        if (totalMass == 0)
        {
            return this.transform.position;
        }

        return accumulated / totalMass;
    }

    public Vector2 GetWorldCentreOfMass()
    {
        return this.transform.position + (this.transform.rotation * this.GetLocalCentreOfMass()); 
    }

    public float GetTotalMass()
    {
        float totalMass = 0;

        for (int x = 0; x < this.Data.MaxWidth; x++)
        {
            for (int y = 0; y < this.Data.MaxHeight; y++)
            {
                Part part = this.Data.BuiltParts[x, y].part;

                if (part != null)
                {
                    totalMass += part.mass;
                }
            }
        }

        return totalMass;
    }


    public void LoadSpaceship(SpaceshipData data)
    {
        this.Data = data;
        
        this.partVisuals = new GameObject[data.MaxWidth, data.MaxHeight];

        for (int x = 0; x < data.MaxWidth; x++)
        {
            for (int y = 0; y < data.MaxHeight; y++)
            {
                BuiltPart builtPart = data.BuiltParts[x, y];

                if (builtPart.part == null)
                {
                    continue;
                }

                InstantiatePartVisuals(builtPart);
            }
        }
    }

    public void LoadSpaceship(int width, int height, BuiltPart[,] builtParts)
    {
        this.LoadSpaceship(new SpaceshipData(width, height, builtParts));
    }

    public void InstantiatePartVisuals(BuiltPart builtPart)
    {
        GameObject partVisual = 
            GameObject.Instantiate(
                builtPart.part.prefab,
                (Vector2)this.transform.position + new Vector2(builtPart.x, builtPart.y),
                Quaternion.Euler(0, 0, 90 * builtPart.rotation),
                this.transform);
        
        PlayModePartController pmpCtrl = partVisual.GetComponent<PlayModePartController>();

        if (pmpCtrl != null)
        {
            pmpCtrl.spaceship = this;
            pmpCtrl.builtPart = builtPart;
        }

        this.partVisuals[builtPart.x, builtPart.y] = partVisual;
    }

    public void AddPart(BuiltPart builtPart)
    {
        this.Data.BuiltParts[builtPart.x, builtPart.y] = builtPart;

        this.InstantiatePartVisuals(builtPart);
    }

    public void AddPart(int x, int y, int rotation, Part part)
    {
        this.AddPart(new BuiltPart(x, y, rotation, part));
    }

    public void RemovePart(int x, int y)
    {
        Destroy(this.partVisuals[x, y]);
        this.partVisuals[x, y] = null;
        this.Data.BuiltParts[x, y].part = null;
    }
}
