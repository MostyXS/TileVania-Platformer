﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startingSceneIndex;

    int currentSceneIndex;
    private void Awake()
    {
        
        if(FindObjectsOfType<ScenePersist>().Length>1 )
        {
            Destroy(gameObject);
           
        }
        else 
        {
            DontDestroyOnLoad(gameObject);
        }


    }

    private void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
        {
            Destroy(gameObject);
        }

    }



}
