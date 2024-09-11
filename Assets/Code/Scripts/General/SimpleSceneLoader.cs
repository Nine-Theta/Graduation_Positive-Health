using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int pIndex)
    {
        SceneManager.LoadScene(pIndex);
    }

    public void LoadSceneByName(string pName)
    {
        SceneManager.LoadScene(pName);
    }
}
