using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureBoxDetail : MonoBehaviour {
    public UIPanelType DefaultType = UIPanelType.eUIPT_JackPot;
    UIPanelType curType = UIPanelType.eUIPT_JackPot;
    public float CountDownDetailHeight = 500;
    public float buyDetailHeight = 600;
    public float SnatchDetailHeight = 700;
    public float SnatchTarDetailHeight = 900;
    public float WithDrawDetailHeight = 800;
    public Animator anim;

    public JackpotPanel countDownPanel;
    public BuyTreasurePanel buyTreasurePanel;
    public SelectSnatchTarPanel selectSnatchTarPanel;
    public SnatchPanel snatchPanel;
    public WithDrawPanel withDrawPanel;

    public Image bg1;
    public Image bg2;
    public Text title;

    Dictionary<UIPanelType, float> uiType2uiheight = new Dictionary<UIPanelType, float>();
    // Use this for initialization
    void Start () {
        Init();
    }

    private void Init()
    {
        uiType2uiheight.Clear();
        uiType2uiheight.Add(UIPanelType.eUIPT_JackPot, CountDownDetailHeight);
        uiType2uiheight.Add(UIPanelType.eUIPT_Buy, buyDetailHeight);
        uiType2uiheight.Add(UIPanelType.eUIPT_Snatch, SnatchDetailHeight);
        uiType2uiheight.Add(UIPanelType.eUIPT_SelectSnatchTar, SnatchTarDetailHeight);
        uiType2uiheight.Add(UIPanelType.eUIPT_WithDraw, WithDrawDetailHeight);
        curType = DefaultType;
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, uiType2uiheight[curType]);
    }

    public void ChangeDetail(UIPanelType type)
    {
        if (curType != type)
        {
            curType = type;
            anim.Play("Leave");
        }
    }

    public void OnLeaveAnimComplete()
    {
        anim.Play("Enter");
        RectTransform rect = GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, uiType2uiheight[curType]);
        switch (curType)
        {
            case UIPanelType.eUIPT_JackPot:
                countDownPanel.gameObject.SetActive(true);
                buyTreasurePanel.gameObject.SetActive(false);
                selectSnatchTarPanel.gameObject.SetActive(false);
                snatchPanel.gameObject.SetActive(false);
                withDrawPanel.gameObject.SetActive(false);
                bg1.gameObject.SetActive(true);
                bg2.gameObject.SetActive(false);
                //title.text = "JackPot";
                countDownPanel.Init();
                break;
            case UIPanelType.eUIPT_Buy:
                buyTreasurePanel.gameObject.SetActive(true);
                selectSnatchTarPanel.gameObject.SetActive(false);
                snatchPanel.gameObject.SetActive(false);
                countDownPanel.gameObject.SetActive(false);
                withDrawPanel.gameObject.SetActive(false);
                bg1.gameObject.SetActive(false);
                bg2.gameObject.SetActive(true);
                //title.text = "Buy";
                buyTreasurePanel.Init();
                break;
            case UIPanelType.eUIPT_Snatch:
                countDownPanel.gameObject.SetActive(false);
                buyTreasurePanel.gameObject.SetActive(false);
                selectSnatchTarPanel.gameObject.SetActive(false);
                snatchPanel.gameObject.SetActive(true);
                withDrawPanel.gameObject.SetActive(false);
                bg1.gameObject.SetActive(false);
                bg2.gameObject.SetActive(true);
                //title.text = "Snatch";
                snatchPanel.Init();
                break;
            case UIPanelType.eUIPT_SelectSnatchTar:
                countDownPanel.gameObject.SetActive(false);
                buyTreasurePanel.gameObject.SetActive(false);
                selectSnatchTarPanel.gameObject.SetActive(true);
                snatchPanel.gameObject.SetActive(false);
                withDrawPanel.gameObject.SetActive(false);
                bg1.gameObject.SetActive(false);
                bg2.gameObject.SetActive(true);
                selectSnatchTarPanel.Init();
                //title.text = "Select Target";
                break;
            case UIPanelType.eUIPT_WithDraw:
                countDownPanel.gameObject.SetActive(false);
                buyTreasurePanel.gameObject.SetActive(false);
                selectSnatchTarPanel.gameObject.SetActive(false);
                snatchPanel.gameObject.SetActive(false);
                withDrawPanel.gameObject.SetActive(true);
                bg1.gameObject.SetActive(false);
                bg2.gameObject.SetActive(true);
                withDrawPanel.Init();
                //title.text = "WithDraw";
                break;
            case UIPanelType.eUIPT_Pause:
                break;
            default:
                break;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}


}
