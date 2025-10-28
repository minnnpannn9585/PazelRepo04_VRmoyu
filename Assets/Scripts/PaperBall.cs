using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBall : MonoBehaviour
{
    public GameObject paperBallTaskUi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ThrowTarget"))
        {
            paperBallTaskUi.SetActive(true);
        }
    }
}
