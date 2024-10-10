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

    [Header("Ending")]
    public GameObject winner;
    public GameObject gameover;

    //----- Functions

    void Awake() {
        _singletonManager = Singleton.Instance;

        SetDifficulty();
        SaveToSingleton();
    } //-- Awake end

    void Start() {
        OnLevelLoaded();

        winner.SetActive(false);
        gameover.SetActive(false);
    } //-- Start end

    void Update() {
        if(_singletonManager.isGameOver) {
            RemoveColliders();
        }
    } //-- Update end


    public void OnLevelLoaded() {
        _singletonManager.isGameOver = false;
        if (SceneManager.GetActiveScene().name == "Game") {
            MatrixGrid.mineBlocks = new MineScript[rows, columns];

            for(int x = 0; x < rows; x++) {
                for (int y = 0; y < columns; y++) {
                    GameObject mine = Instantiate(mineBlocks, new Vector3(x - rows/2, y - columns/2, 0), Quaternion.identity) as GameObject;
                    mine.name = x + "-" + y;
                    mine.transform.parent = mineField.transform;
                    MatrixGrid.mineBlocks[x, y] = mine.GetComponent<MineScript>();
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
                break;
            case 0:
                rows = 9; columns = 8;
                posX = 2.5f; posY = 0.4f;
                scale = 1.1f;
                mineCount = remMines = easyMineCount;
                break;
            case 2:
                rows = 13; columns = 10;
                posX = 2.3f; posY = 0.4f;
                scale = 0.9f;
                mineCount = remMines = diffMineCount;
                break;
        }
    } //-- SetDifficulty end

    public void SaveToSingleton() {
        _singletonManager.mineCount = mineCount;
        _singletonManager.rows = rows;
        _singletonManager.columns = columns;
    } //-- SaveToSingleton end

    public void ReturnBtnPressed() {
        _singletonManager.mineCount = remMines;
        SceneManager.LoadScene("Menu");
    } //-- ReturnBtnPressed end

    public void RestartBtnPressed() {
        _singletonManager.isGameOver = false;
        SceneManager.LoadScene("Game");
    } //-- RestartBtnPressed end

    void RemoveColliders() {// Find all GameObjects with the tag "WhiteBlock"
        GameObject[] minesObj = GameObject.FindGameObjectsWithTag("WhiteBlock");
        
        foreach (GameObject mine in minesObj) {
            BoxCollider2D boxCollider = mine.GetComponent<BoxCollider2D>();
            if (boxCollider != null) {
                Destroy(boxCollider); // Remove the BoxCollider component
            }
        }

        StartCoroutine(ShowGameOver());
    } //-- RemoveColliders

    IEnumerator ShowGameOver() {
        yield return new WaitForSeconds(2.0f);
        gameover.SetActive(true);
    } //-- ShowGameOver

}


/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/