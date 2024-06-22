using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;
    public int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {   
        health = 3;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts) {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < health; i++) {
            hearts[i].sprite = fullHeart;
        }
    }

    public void TakeDamage(int damage)
    {   
        health -= damage;
        health = Mathf.Clamp(health, 0, hearts.Length); // Ensures health does not go below 0 or above the number of hearts
        Update();


    }

}
