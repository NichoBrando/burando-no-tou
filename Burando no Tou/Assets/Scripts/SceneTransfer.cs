using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public static void StartGame()
    {
        SceneManager.LoadScene(1);
    } 
}
