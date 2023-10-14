using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int coinReward = 25;
    public int coinPenalty = 25;

    Coin coin;

    private void Start()
    {
        coin = FindObjectOfType<Coin>();
    }

    public void RewardCoin()
    {
        if(coin == null) { return; }
        coin.DePosit(coinReward);
    }

    public void PenaltyCoin()
    {
        if (coin == null) { return; }
        coin.WithDraw(coinPenalty);
    }
}
