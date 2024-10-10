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

    [Header("Mine Field")]
    private float posX, posY;
    private float scale;
    public TMP_Text mineCountTxt;
    
    [Header("Mine Blocks")]
    [SerializeField]
    private GameObject mineBlocks;
    [SerializeField]
    private GameObject mineField;
    [HideInInspector]
    private int rows;
    [HideInInspector]
    private int columns;


    private int easyMineCount = 15, normalMineCount = 20, diffMineCount = 30;
    private int mineCount, remMines;

    [Header("Audio Files")]
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    //----- Functions

    void Awake() {
        _singletonManager = Singleton.Instance;
        SetDifficulty();
    } //-- Awake end

    public void Start() {
        OnLevelLoaded();
    } //-- Start end

    public void OnLevelLoaded() {
        if (SceneManager.GetActiveScene().name == "Game") {
            MatrixGrid.mineBlocks = new MineBlocks[rows, columns];

            for(int x = 0; x < rows; x++) {
                for (int y = 0; y < columns; y++) {
                    GameObject mine = Instantiate(mineBlocks, new Vector3(x - rows/2, y - columns/2, 0), Quaternion.identity) as GameObject;
                    mine.name = x + "-" + y;
                    mine.transform.parent = mineField.transform;
                    MatrixGrid.mineBlocks[x, y] = mine.GetComponent<MineBlocks>();
                }
            }

            mineField.transform.localScale = new Vector3(scale, scale, 1);
            mineField.transform.position = new Vector3(posX, posY, 0);
            mineCountTxt.text = remMines.ToString();

        } else {
            mineCount = 0;
        }
    } //-- OnLevelLoaded end

    public void SetDifficulty() {
        int option = _singletonManager.difficulty;
        switch (option) {
            default:
            case 1:
                rows = 11; columns = 9;
                posX = 2.2f; posY = 0;
                scale = 1.0f;
                mineCount = remMines = normalMineCount;
                _singletonManager.mineCount = mineCount;
                break;
            case 0:
                rows = 9; columns = 8;
                posX = 2.5f; posY = 0.4f;
                scale = 1.1f;
                mineCount = remMines = easyMineCount;
                _singletonManager.mineCount = mineCount;
                break;
            case 2:
                rows = 13; columns = 10;
                posX = 2.3f; posY = 0.4f;
                scale = 0.9f;
                mineCount = remMines = diffMineCount;
                _singletonManager.mineCount = mineCount;
                break;
        }
    }

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