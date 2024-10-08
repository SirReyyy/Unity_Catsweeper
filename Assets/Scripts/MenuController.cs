using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //----- Variables
    private Singleton _singletonManager;

    [Header("Buttons")]
    public GameObject playBtn;
    public GameObject easyBtn, normalBtn, diffBtn;

    private bool isPlayHidden = false;

    //----- Functions

    void Awake() {
        _singletonManager = Singleton.Instance;
        
        GetMenuButtons();
    } //-- Awake end

    private void GetMenuButtons() {
        // Get buttons with tag Menu Button
        playBtn = GameObject.FindGameObjectWithTag("PlayButton");
        GameObject[] menuBtn = GameObject.FindGameObjectsWithTag("MenuButton");
        easyBtn = menuBtn[0];
        normalBtn = menuBtn[1];
        diffBtn = menuBtn[2];

        foreach (GameObject btnObj in menuBtn) {
            btnObj.GetComponent<Button>().onClick.AddListener(() => SetGridSize());
            btnObj.SetActive(false);
        }
    } //-- GetMenuButtons end

    private void SetGridSize() {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        _singletonManager.GridSize = int.Parse(name);
        SceneManager.LoadScene("Game");
    } //-- SetGridSize end


    public void PlayBtnPressed() {
        ShowHideBtn();
    } //-- PlayBtnPressed end

    private void ShowHideBtn() {
        if(!isPlayHidden) {
            playBtn.SetActive(false);
            easyBtn.SetActive(true);
            normalBtn.SetActive(true);
            diffBtn.SetActive(true);

            isPlayHidden = true;
            StartCoroutine(PlayBtn());

        } else {
            playBtn.SetActive(true);
            easyBtn.SetActive(false);
            normalBtn.SetActive(false);
            diffBtn.SetActive(false);

            isPlayHidden = false;
        }
    } //-- ShowHideBtn end

    IEnumerator PlayBtn() {
        yield return new WaitForSeconds(20.0f);
        ShowHideBtn();
    } //-- PlayBtn end

}


/*
Project Name : Catsweeper
Created by   : Sir Reyyy
*/