using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCondition : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject WinOver;
    void Start()
    {
        GameOver.SetActive(false);
        WinOver.SetActive(false);
    }

    void Update()
    {
        if (WaypointMovement.isSleep)
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
