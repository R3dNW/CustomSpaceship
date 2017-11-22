using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFieldController : MonoBehaviour
{
    public GameObject starParticlesPrefab;

    public CameraRig cameraRig;

    private GameObject[,] starParticles;

    public int gridSize = 2;
    public float starsWidth = 100;

    void Start()
    {
        if (this.cameraRig == null)
        {
            this.cameraRig = Camera.main.GetComponentInParent<CameraRig>();
        }

        this.starParticles = new GameObject[this.gridSize, this.gridSize];

        for (int x = 0; x < this.gridSize; x++)
        {
            for (int y = 0; y < this.gridSize; y++)
            {
                this.starParticles[x, y] = Instantiate(this.starParticlesPrefab, this.transform);
            }
        }
    }

    void Update()
    {
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                Vector2 newPos =
                    new Vector2(
                        (Mathf.Floor(this.cameraRig.transform.position.x / this.starsWidth) + x) * this.starsWidth,
                        (Mathf.Floor(this.cameraRig.transform.position.y / this.starsWidth) + y) * this.starsWidth);

                if (newPos != (Vector2)this.starParticles[x, y].transform.position)
                {
                    // TODO: Should this refresh or not?

                    ParticleSystem ps = this.starParticles[x, y].GetComponent<ParticleSystem>();
                    ParticleSystem.EmissionModule psemit = ps.emission;

                    ps.Simulate(0.0f, true, true);
                    psemit.enabled = true;
                    ps.Play();

                    this.starParticles[x, y].transform.position = newPos;
                }
            }
        }
    }
}
