using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invent : MonoBehaviour
{
    public GameObject menu;
    public bool isOpenInventory;
    public GameObject inventory;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleInventory();
        }
    }

    public void ActivateInvent()
    {
        test = Instantiate(menu, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        test.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        //Instantiate(canvas);
    }

    public void DeActivateInvent()
    {
        GameObject.Destroy(test);
    }

    public void ToggleInventory()
    {
        SetInventory(!isOpenInventory);
    }

    public void SetInventory(bool aInventory)
    {
        isOpenInventory = aInventory;
        if (isOpenInventory == true)
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
