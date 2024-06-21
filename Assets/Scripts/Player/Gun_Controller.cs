using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Controller : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform firePoint;
    [SerializeField]public float fireForce;
    public GameObject pivotObject;
    AudioManager audioManager;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    public void Fire()
    {
        if (!PauseMenu.isPaused)
        {   
            audioManager.PlaySFX(audioManager.gunShot);
            GameObject dart = Instantiate(dartPrefab, firePoint.position, firePoint.rotation);

            dart.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
       
    }

    public void Flip (){
        transform.Rotate(180, 0, 0);
    }
}