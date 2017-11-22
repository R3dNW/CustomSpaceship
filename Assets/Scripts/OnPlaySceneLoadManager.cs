using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class OnPlaySceneLoadManager : MonoBehaviour
{
    public SpaceshipController spaceship;

    public void Awake()
    {
        if (PlayerPrefs.HasKey("Spaceship"))
        {
            SpaceshipData data = JsonConvert.DeserializeObject<SpaceshipData>(PlayerPrefs.GetString("Spaceship"), new SpaceshipDataToJsonConverter());

            int count = 0;

            foreach (BuiltPart part in data.BuiltParts)
            {
                if (part.part != null)
                {
                    count++;
                }
            }

            spaceship.LoadSpaceship(data);
        }
    }
}
