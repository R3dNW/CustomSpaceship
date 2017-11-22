using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject GridElementPrefab;

    public int width;
    public int height;

    void Start()
    {
        for (int x = 0; x < this.width; x++)
        {
            for (int y = 0; y < this.height; y++)
            {
                GameObject.Instantiate(GridElementPrefab, new Vector3(x, y, this.transform.position.z), Quaternion.identity, this.transform);
            }
        }
    }
}