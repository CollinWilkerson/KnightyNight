using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;

    private Monster[] _monsters;

    private void OnEnable()
    {
        _monsters = FindObjectsByType<Monster>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (MontersAreAllDead())
        {
            GoToNextLevel();
        }
    }

    private bool MontersAreAllDead()
    {
        foreach(Monster monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    private void GoToNextLevel()
    {
        Debug.Log("Go to level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }
}
