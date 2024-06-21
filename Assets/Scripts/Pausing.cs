using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject MenuBase;
    public static bool isPaused;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        MenuBase.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {

            audioManager.PlaySFX(audioManager.buttonPress);

            if(isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        MenuBase.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame() {
        MenuBase.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

}
