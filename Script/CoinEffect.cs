using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinEffect : MonoBehaviour {
    public GameObject coinPrefab;
    public float rangeX = 0.1f;
    public float rangeY = 2;
    public float rangeZ = 0.1f;
    public int maxCoinNum = 100;
    int curCoinNum = 0;
    List<GameObject> list_coin = new List<GameObject>();
    public float lifeTime = 10;
    public float curTime = 0;
    public float blackHoleTime = 5;
    public GameObject blackholeObj;
	// Use this for initialization
	void Start () {
        //GenerateCoin(500);

    }

    private void OnEnable()
    {
        list_coin.Clear();
       // GenerateCoin(coinNum);
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Reset()
    {
        blackholeObj.SetActive(false);
        for (int i = 0; i < list_coin.Count; i++)
        {
            GameObject item = list_coin[i];
            if (item != null)
            {
                Destroy(item);
            }           
        }
        list_coin.Clear();
    }

    // Update is called once per frame
    void Update () {
        curTime += Time.deltaTime;
        if (curTime > lifeTime)
        {
            Destroy(this.gameObject);
        }
        if (curTime > blackHoleTime)
        {
            EnableBlackHole();
        }

    }

    private void EnableBlackHole()
    {
        if (!blackholeObj.activeInHierarchy)
        {
            blackholeObj.SetActive(true);
        }
        
    }

    public void GenerateCoin(int num)
    {
        //StartCoroutine(GenerateByCoroutin(num));
        if (num > maxCoinNum)
        {
            num = maxCoinNum;
        }

        for (int i = 0; i < num; i++)
        {
            GameObject coin = GameObject.Instantiate(coinPrefab);
            coin.transform.SetParent(this.transform, false);
            float randX = UnityEngine.Random.Range(rangeX * -1, rangeX);
            float randY = UnityEngine.Random.Range(0, rangeY);
            float randZ = UnityEngine.Random.Range(rangeZ * -1, rangeZ);
            coin.transform.localPosition = new Vector3(randX, randY, randZ);
            list_coin.Add(coin);
        }
    }

    private IEnumerator GenerateByCoroutin(int num)
    {
        while (curCoinNum < num)
        {
            int creatNum = 0;
            if (num - curCoinNum >= maxCoinNum)
            {
                creatNum += maxCoinNum;
            }
            else
            {
                creatNum += (num - curCoinNum);
            }

            for (int i = 0; i < creatNum; i++)
            {
                GameObject coin = GameObject.Instantiate(coinPrefab);
                coin.transform.SetParent(this.transform, false);
                float randX = UnityEngine.Random.Range(rangeX * -1, rangeX);
                float randY = UnityEngine.Random.Range(0, rangeY);
                float randZ = UnityEngine.Random.Range(rangeZ * -1, rangeZ);
                coin.transform.localPosition = new Vector3(randX, randY, randZ);
                list_coin.Add(coin);
            }
            curCoinNum += creatNum;
            yield return new WaitForSeconds(0.5f) ;
        }        
    }

}
