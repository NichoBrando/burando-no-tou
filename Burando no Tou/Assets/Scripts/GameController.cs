using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject BattleScene;

    public void StartBattle()
    {
        BattleScene.SetActive(true);
    }

    public void EndBattle()
    {
        BattleScene.SetActive(false);
    }

    public void EndGame(){
        SceneManager.LoadScene(2);
    }
}
