using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    public GameObject worldMap;

    private void Start()
    {
        worldMap = GameObject.Find("WorldmapContent");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            Debug.Log(true);
            worldMap.SetActive(true);
        }
        else
        {
            worldMap.SetActive(false);
        }
    }
}
