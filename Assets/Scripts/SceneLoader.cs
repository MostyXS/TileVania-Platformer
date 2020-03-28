using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void JoinPortal()
    {
        StartCoroutine(NextLVLLoad());
    }
    private IEnumerator NextLVLLoad()
    {
        yield return new WaitForSeconds(1f);
        LoadNextScene();

    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
        
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }


}
