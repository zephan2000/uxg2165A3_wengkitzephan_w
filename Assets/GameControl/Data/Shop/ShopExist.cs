using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopExist : MonoBehaviour
{
    public GameObject menu;
    public bool isOpenShop;
    public GameObject shop;
    public GameObject test;

    public void ActivateInvent()
    {
        test = Instantiate(menu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        test.transform.SetParent(GameObject.FindGameObjectWithTag("Shop").transform, false);
        //Instantiate(canvas);
    }
    public void DeActivateInvent()
    {
        GameObject.Destroy(test);
    }

    public void ToggleShop(bool isOpenShop)
    {
        SetShop(!isOpenShop);
    }

    public void SetShop(bool ashop)
    {
        isOpenShop = ashop;
        if (isOpenShop == true)
        {
            ActivateInvent();
            test.SetActive(true);
        }
        else
        {
            DeActivateInvent();
            test.SetActive(false);
        }
    }
}
