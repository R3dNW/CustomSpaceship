using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeEffectController : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

	void Update ()
    {
		foreach(ParticleSystem ps in this.particleSystems)
        {
            if (ps.particleCount > 0)
            {
                return;
            }
        }

        Destroy(this);
	}
}
