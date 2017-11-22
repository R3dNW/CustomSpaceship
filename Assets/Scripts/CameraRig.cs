using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraRig : MonoBehaviour, IEnumerable<Camera>
{
    public Camera[] cameras;

    public IEnumerator<Camera> GetEnumerator()
    {
        return ((IEnumerable<Camera>)this.cameras).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Camera>)this.cameras).GetEnumerator();
    }
}
