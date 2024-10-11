using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton instance;

    public int difficulty { get; set; }
    public int mineCount { get; set; }
    public int targetBlocks { get; set; }
    public int rows { get; set; }
    public int columns { get; set; }

    // public int openBlock { get; set; }
    public bool isGameOver { get; set; } = false;
    public bool isGameWon { get; set; } = false;


    // Singleton Instance
    public static Singleton Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<Singleton>();

                if (instance == null) {
                    GameObject singletonObject = new GameObject("Singleton");
                    instance = singletonObject.AddComponent<Singleton>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    } //-- Singleton end

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    } //-- Awake end
}



/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/