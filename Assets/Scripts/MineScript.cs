using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    //----- Variables
    private Singleton _singletonManager;

    [HideInInspector]
    public bool isMine;
    private SpriteRenderer sR;

    [Header("Hidden Blocks")]
    [SerializeField]
    private GameObject[] numberPrefabs;
    [SerializeField]
    private GameObject minePrefab;
    [SerializeField]
    private Sprite[] catsImages;

    [Header("Revealed Blocks")]
    private Vector2 initialPos;
    private GameObject blockParent;
    private bool isRevealed = false;

    //----- Functions
    void Awake() {
        _singletonManager = Singleton.Instance;
    } //-- Awake end

    void Start() {
        isMine = Random.value < 0.20f;
        if (isMine) {
            if (_singletonManager.mineCount > 0) {
                _singletonManager.mineCount--;
            } else {
                isMine = false;
            }
        }

        blockParent = gameObject.transform.parent.gameObject;
        initialPos = transform.position;
    } //-- Start end

    public void ShowMines() {
        if (isMine) {
            int index = Random.Range(0, numberPrefabs.Length);

            GameObject mineObj = Instantiate(minePrefab, initialPos, Quaternion.identity);
            mineObj.transform.parent = blockParent.transform;
            mineObj.GetComponent<SpriteRenderer>().sprite = catsImages[index];

            isRevealed = true;
            gameObject.SetActive(false);
        }
    } //-- ShowMines end

    public void ShowNearbyMinesCount(int nearbyMines) {
        GameObject nearbyObj = Instantiate(numberPrefabs[nearbyMines], initialPos, Quaternion.identity);
        nearbyObj.transform.parent = blockParent.transform;

        isRevealed = true;
        gameObject.SetActive(false);
    } //-- ShowNearMines end

    public bool IsClicked() {
        //return gameObject.tag == "WhiteBlock";
        return isRevealed;
    } //-- IsClicked end

    void OnMouseDown() {
        if(isMine) {
            MatrixGrid.ShowAllMines();
            _singletonManager.isGameOver = true;
        } else {
            string[] index = gameObject.name.Split("-");
            int x = int.Parse(index[0]);
            int y = int.Parse(index[1]);

            MatrixGrid.RevealBlock();
            ShowNearbyMinesCount(MatrixGrid.NearbyMines(x, y));
            MatrixGrid.CheckMines(x, y, new bool[_singletonManager.rows, _singletonManager.columns]);

            Debug.Log("Target: " + _singletonManager.targetBlocks.ToString());
            if (MatrixGrid.CheckGameStatus() == _singletonManager.targetBlocks) {
                _singletonManager.isGameWon = true;
            }   
        }
    } //-- OnMouseDown end
}


/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/