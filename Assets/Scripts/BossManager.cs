using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject bossObject;
    public float bossTimer;
    bool bossActive;

    private void Start()
    {
        bossActive = bossObject.activeInHierarchy;
    }
    void Update()
    {
        if(bossActive)
        {
            bossTimer -= Time.deltaTime;
            if(bossTimer <= 0f)
            {
                bossActive = false;
                bossObject.SetActive(false);
                bossTimer = 15f;
            }
        }
        else
        {
            bossTimer -= Time.deltaTime;
            if(bossTimer <= 0f)
            {
                bossActive = true;
                bossObject.SetActive(true);
                bossTimer = 15f;
            }
        }
    }
}
