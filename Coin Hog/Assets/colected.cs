using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colected : MonoBehaviour
{
    public GameObject coinParent;
    public int coinCount = 0;
    public int totalNumberOfCoins = 0;
    public bool makeLose = false;
    // Use this for initialization
    void Start()
    {
        totalNumberOfCoins = coinParent.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

        coinCount = totalNumberOfCoins - coinParent.transform.childCount;
    }

    

    void OnTriggerEnter2D(Collider2D col)
    {
        

        if (col.tag.Equals("Coin"))
        {
            Destroy(col.gameObject);
        }
        else if (col.tag.Equals("Bad"))
        {
            makeLose = true;
        }
    }
}