using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    public static levelManager LM;
    [SerializeField] GameObject mGameOver;

    // Use this for initialization
    void Start()
    {
        LM = this;
        mGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update() { }

    public void GameOver()
    {
        Time.timeScale = 0; //esto hay que cambiarlo
        mGameOver.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        mGameOver.SetActive(false);
        Time.timeScale = 1;
    }
}
