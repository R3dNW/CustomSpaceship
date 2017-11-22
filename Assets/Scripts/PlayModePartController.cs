using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class PlayModePartController : MonoBehaviour
{
    [HideInInspector]
    public SpaceshipController spaceship;

    [HideInInspector]
    public BuiltPart builtPart;

    public void Awake()
    {
    }

    public void Start()
    {
    }

    public void Update()
    {
    }

    public virtual void PlayModeAwake() { }
    public virtual void PlayModeStart() { }
    public virtual void PlayModeMoveUpdate(string[] controls) { }
    public virtual void PlayModeActionUpdate(string[] actions) { }
}
