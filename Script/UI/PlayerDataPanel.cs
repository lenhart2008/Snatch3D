using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataPanel : MonoBehaviour {

    public static PlayerDataPanel _instance;
    public static PlayerDataPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(PlayerDataPanel)) as PlayerDataPanel;
            }
            return _instance;
        }
    }

    public Text playerTreasureCountText;
    public Text playerETHCountText;
    public Text playerAddressText;
    public Text playerShieldCountDownText;
    public Text playerAwardCountText; 
    public CountDownComponent countDownCom;

    public Transform EffectRoot;
    public GameObject CoinEffect;
    public Button SnatchLetterBtn;
    public float refreshIntervalTime = 2;
    public float curRefreshTime = 0;
    // Use this for initialization
    bool isInit = false;
    void Start () {
        Init();

    }

    private void Init()
    {
        countDownCom.timeChangeCallback = OnShieldCountDownChange;
        DataManager.Instance.playerDataCtrl.ResigerDataChangeCallBack(UpdatePlayerDataUI);
        DataManager.Instance.snatchLetterDataCtrl.ResigerDataChangeCallBack(UpdateSnatchLetterDataUI);
        UpdatePlayerDataUI(DataManager.Instance.playerDataCtrl.playerData);
        UpdateSnatchLetterDataUI(DataManager.Instance.snatchLetterDataCtrl.letterData);
        isInit = true;
    }

    public void OnShieldCountDownChange(string value)
    {
        playerShieldCountDownText.text = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (curRefreshTime < refreshIntervalTime)
        {
            curRefreshTime += Time.deltaTime;
        }
        else
        {
            curRefreshTime = 0;
            RefreshPlayerDataPanel();
        }
    }

    private void RefreshPlayerDataPanel()
    {
        DataManager.Instance.playerDataCtrl.RefreshData();
        DataManager.Instance.snatchLetterDataCtrl.RefreshData();
    }

    public void UpdateSnatchLetterDataUI(SnatchLetterData data)
    {
        if (!DataManager.Instance.snatchTarListDataCtrl.AddIsInvalid(data.address))
        {
            SnatchLetterBtn.gameObject.SetActive(true);
        }
        else
        {
            SnatchLetterBtn.gameObject.SetActive(false);
        }
    }

    public void ShowMegBox()
    {
        string add = DataManager.Instance.snatchLetterDataCtrl.letterData.address;
        string count = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(DataManager.Instance.snatchLetterDataCtrl.letterData.count);
        string str1 = "      Thank you for your " + count + " TRX. I will keep it well.";
        string str2 = add;
        UIManager.Instance.ShowMsgBox(str1, str2);
    }

    public void UpdatePlayerDataUI(TreasurePlayerData data)
    {
       
        int lastTreasure = int.Parse(playerTreasureCountText.text);
        int increaseTreasure = (int)data.treasureNum - lastTreasure;
        if (increaseTreasure > 0 && isInit)
        {
            CreateCoinEffect(increaseTreasure);
        }

        playerAddressText.text = data.address.ToString();
        playerTreasureCountText.text = data.treasureNum.ToString();
        playerETHCountText.text = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(data.playerETH);
        playerAwardCountText.text = GameMethodLibrary.ConversionOfMoneyUnitsBigInteger2str(data.award);
        countDownCom.SetShowTime((int)data.shieldTime);
    }

    public void ShowCurSelectPlayerData(UInt64 playerId)
    {
        if (TreasureContract.Instance.dic_PlayerID2PlayerData.ContainsKey(playerId))
        {
            var item = TreasureContract.Instance.dic_PlayerID2PlayerData[playerId];
            playerTreasureCountText.text = item.treasureNum.ToString();
            playerETHCountText.text = item.playerETH.ToString();
        }
        
    }

    private void CreateCoinEffect(int num)
    {
        GameObject effect = GameObject.Instantiate(CoinEffect);
        CoinEffect ce = effect.GetComponent<CoinEffect>();
        ce.GenerateCoin(num);
        effect.transform.SetParent(EffectRoot, false);
    }
}
