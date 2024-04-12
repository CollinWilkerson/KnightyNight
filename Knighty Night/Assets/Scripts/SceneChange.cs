using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] string LevelName;
    public void StartGame()
    {
        Debug.Log("Loading level 1");
        SceneManager.LoadScene("Level1");
    }
    public void GoToSceneByName()
    {
        Debug.Log("Loding scene " + LevelName);
        SceneManager.LoadScene(LevelName);
    }
}
