using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SwitchToPlaySceneController : MonoBehaviour
{
    public SpaceshipController spaceship;

    AsyncOperation loadPlayScene;

    public void SwitchToPlayScene()
    {
        PlayerPrefs.SetString("Spaceship", JsonConvert.SerializeObject(this.spaceship.Data, new SpaceshipDataToJsonConverter()));

        this.loadPlayScene = SceneManager.LoadSceneAsync("PlayScene");
    }
}
