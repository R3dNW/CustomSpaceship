using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoltController : MonoBehaviour
{
    public GameObject strikeEffectPrefab;

    public float maxDistance = 100;
    private float sqrMaxDistance;

    public float speed = 20f;

    private float activationDistance;

    Vector2 originalPosition;

    public LayerMask canHit = ~0;
    
    void Start ()
    {
        this.sqrMaxDistance = this.maxDistance * this.maxDistance;

        // TODO: What is an appropriate activation distance?
        this.activationDistance = this.speed * Time.fixedDeltaTime;
	}
	
	void FixedUpdate ()
    {
        this.transform.Translate((this.transform.up * this.speed), Space.World);
        
        if (Physics2D.Raycast(this.transform.position, this.transform.up, this.activationDistance, this.canHit))
        {
            Debug.Log("Hit Something.");

            GameObject.Instantiate(this.strikeEffectPrefab, this.transform.position, this.transform.rotation * Quaternion.Euler(0, 0, 180));

            Destroy(this.gameObject);

            return;
        }

        if (Vector3.SqrMagnitude((Vector2)this.transform.position - this.originalPosition) >= sqrMaxDistance)
        {
            Destroy(this.gameObject);

            return;
        }
	}
}
