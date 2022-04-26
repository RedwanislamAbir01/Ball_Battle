using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public override void Start()
    {

        base.Start();
        PlayerPrefs.SetInt("current_scene", SceneManager.GetActiveScene().buildIndex);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
