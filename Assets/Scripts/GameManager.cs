﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public float maxHealth = 100f;
    public Slider healthSlider;

    [SerializeField]
    private float _currHealth;

    private int level = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    void InitGame()
    {
    }

    public void InitLevel()
    {
        SceneManager.LoadScene("DemoLevel2");
        InitPlayerHealth();
    }

    public void InitPlayerHealth()
    {
        _currHealth = maxHealth;
        healthSlider.value = _currHealth;
    }

    public void LoseHealth(float amount)
    {
        _currHealth = Mathf.Clamp(_currHealth - amount, 0, maxHealth);

        healthSlider.value = _currHealth;

        if (_currHealth <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}