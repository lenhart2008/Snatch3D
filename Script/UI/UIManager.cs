using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIPanelType
{
    eUIPT_JackPot = 0,
    eUIPT_Buy = 1,
    eUIPT_Snatch = 2,
    eUIPT_SelectSnatchTar = 3,
    eUIPT_WithDraw = 4,
    eUIPT_Pause = 5,
    eUIPT_MsgBox = 6
}

public class UIManager : MonoBehaviour {

    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(UIManager)) as UIManager;
            }
            return _instance;
        }
    }

    public StartMenuPanel startMenu;
    public TreasureBoxDetail detail;
    public GameObject uiBroad;
    public GameObject uiPlayerData;
    public GameObject uiDetail;
    public MsgBox msgBox;
    // Use this for initialization
    void Start () {
        uiBroad.SetActive(false);
        uiPlayerData.SetActive(false);
        uiDetail.SetActive(false);
        msgBox.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowStartMenu()
    {
        startMenu.Show();
    }

    internal void ShowUIPanel(UIPanelType eUIPT_Pause)
    {
        
    }

    public void EnterGame()
    {
        StartCoroutine(_EnterGame());
     
        //Animator animBroad = uiBroad.GetComponent<Animator>();
        //animBroad.Play("Enter");
        //Animator animPlayerdata = uiPlayerData.GetComponent<Animator>();
        //animPlayerdata.Play("Enter");
        //Animator animDetail = uiDetail.GetComponent<Animator>();
        //animDetail.Play("Enter");
    }

    internal void HideUIPanel(UIPanelType eUIPT_Pause)
    {
        
    }

    IEnumerator _EnterGame()
    {
        yield return new WaitForSeconds(1f);
        uiBroad.SetActive(true);
        yield return new WaitForSeconds(1f);
        uiPlayerData.SetActive(true);
        uiDetail.SetActive(true);
    }

    public void OnBuyBtnClick()
    {
        ChangeDetail(UIPanelType.eUIPT_Buy);
    }

    public void OnSnatchBtnClick()
    {
        ChangeDetail(UIPanelType.eUIPT_SelectSnatchTar);
    }

    public void OnHelpBtnClick()
    {
        ChangeDetail(UIPanelType.eUIPT_WithDraw);
    }

    public void ChangeDetail(UIPanelType type)
    {
        detail.ChangeDetail(type);
    }

    public void ShowMsgBox(string title, string content, int type = 1, Action callback = null)
    {
        msgBox.ShowLetter(title, content, type, callback);
    }
}
