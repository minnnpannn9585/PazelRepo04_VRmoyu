using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public Transform bossTrans;
    public Transform[] pointsTrans = new Transform[10];
    public float moveSpeed = 3f;
    public float rotationSpeed = 360f; // degrees per second around world Y
    public float reachThreshold = 0.1f;

    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        bossTrans = transform.GetChild(0);
        for (int i = 0; i < pointsTrans.Length; i++)
        {
            pointsTrans[i] = transform.GetChild(1).GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex >= pointsTrans.Length)
        {
            return;
        }
        if (pointsTrans == null || pointsTrans.Length == 0) return;

        // Ensure current target is valid
        Transform target = pointsTrans[currentIndex];
        if (target == null) return;

        // Move towards the current target
        bossTrans.position = Vector3.MoveTowards(bossTrans.position, target.position, moveSpeed * Time.deltaTime);

        // Rotate around world Y to face the target (ignore vertical difference)
        Vector3 flatDir = target.position - bossTrans.position;
        flatDir.y = 0f;
        if (flatDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatDir, Vector3.up);
            bossTrans.rotation = Quaternion.RotateTowards(bossTrans.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // If reached the target, advance to the next one (wrap around)
        if (Vector3.Distance(bossTrans.position, target.position) <= reachThreshold)
        {
            currentIndex = (currentIndex + 1);
            
        }
    }
}
