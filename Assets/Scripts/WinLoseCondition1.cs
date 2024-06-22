using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseConditionTiger : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject WinOver;
    bool winCondition = false;
    bool loseCondition = false;

    void Start()
    {
        GameOver.SetActive(false);
        WinOver.SetActive(false);
    }

    void Update()
    {
        if (TigerMovement.isSleep)
        {
            WinOver.SetActive(true);
            
        }

        if (Character_Controller.isDead)
        {
            GameOver.SetActive(true);

        }
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
