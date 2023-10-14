using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public int coin = 200;
    public int currentCoin = 0;
    public TextMeshProUGUI textCoin;

    public int CurrentCoin { get { return currentCoin; } }
    void Awake()
    {
        currentCoin = coin;
        UpdateUI();
    }

    public void DePosit(int amount)
    {
        currentCoin += Mathf.Abs(amount);
        UpdateUI();
    }

    public void WithDraw(int amount)
    {
        currentCoin -= Mathf.Abs(amount);
        UpdateUI();

        if (currentCoin < 0)
        {
            //UI loss
            LoadScene();
        }
    }

    void UpdateUI()
    {
        textCoin.text = "Coin : " + currentCoin;
    }

    void LoadScene()
    {
        UnityEngine.SceneManagement.Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
