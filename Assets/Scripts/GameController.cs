using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    //----- Variables
    private Singleton _singletonManager;


    private int gridSize;

    [Header("Audio Files")]
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    //----- Functions

    void Awake() {
        _singletonManager = Singleton.Instance;

        gridSize = _singletonManager.GridSize;
        Debug.Log(gridSize);
        // SetGridSize(gridSize);
    } //-- Awake end

    public void ReturnBtnPressed() {
        SceneManager.LoadScene("Menu");
    } //-- ReturnBtnPressed end

    public void RestartBtnPressed() {
        SceneManager.LoadScene("Game");
    } //-- RestartBtnPressed end
}


/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/