using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int cost = 30; 

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Coin coin = FindObjectOfType<Coin>();
        if(coin == null)
        {
            return false;
        }

        if(coin.CurrentCoin >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            coin.WithDraw(cost);
            return true;
        }
        return false;
    }
}
