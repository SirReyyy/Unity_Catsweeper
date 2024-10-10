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

    //----- Functions
    void Awake() {
        _singletonManager = Singleton.Instance;
    } //-- Awake end

    void Start() {
        isMine = Random.value < 0.15f;

        if(isMine) {
            // if(_singletonManager.mineCount < )
        }

        blockParent = gameObject.transform.parent.gameObject;
        initialPos = transform.position;
    } //-- Start end

    public void ShowMines() {
        if (isMine) {
            int index = Random.Range(0, numberPrefabs.Length);
            sR.sprite = catsImages[index];
        }
    } //-- ShowMines end

    public void ShowNearbyMinesCount(int nearbyMines) {
        // gameObject.SetActive(false);
        GameObject newObj = Instantiate(numberPrefabs[nearbyMines], initialPos, Quaternion.identity);
        newObj.transform.parent = blockParent.transform;

        Destroy(gameObject);
    } //-- ShowNearMines end

    void OnMouseDown() {
        ShowNearbyMinesCount(5);
    } //-- OnMouseDown end

}


/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/