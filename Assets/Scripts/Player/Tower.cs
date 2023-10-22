using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int cost = 65;
    public float shootDelay = 1.1f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

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

    IEnumerator Shoot()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(shootDelay);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}
