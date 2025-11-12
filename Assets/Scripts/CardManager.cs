using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public List<CardSlot> allSlots;
    public bool canExplode = false;

    // new: control explosion behavior and force range
    public float minExplodeForce = 300f;
    public float maxExplodeForce = 700f;
    private bool hasExploded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (canExplode && !hasExploded)
        {
            for(int i = 0; i < allSlots.Count; i++)
            {

                Rigidbody rb = allSlots[i].OccupyingCard.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;

                var slot = allSlots[i];
                var card = slot.OccupyingCard;
                // unparent so physics acts in world space
                card.transform.SetParent(null);

                rb.isKinematic = false;
                rb.useGravity = true;

                // create a randomized direction:
                // base direction is outward from this manager's position to the card,
                // then add some random spread so force isn't perfectly radial.
                Vector3 baseDir = (card.transform.position - transform.position).normalized;
                Vector3 randomSpread = Random.insideUnitSphere * 0.6f;
                // Optionally reduce downward pushes so cards don't immediately bury into floor:
                randomSpread.y = Mathf.Clamp(randomSpread.y, -0.2f, 1f);

                Vector3 explodeDirection = (baseDir + randomSpread).normalized;

                float force = Random.Range(minExplodeForce, maxExplodeForce);
                rb.AddForce(explodeDirection * force, ForceMode.Impulse);
            }

            // ensure explosion only happens once per trigger
            hasExploded = true;
        }
    }

    public void CheckCompletion()
    {
        bool allFilled = true;

        foreach (var slot in allSlots)
        {
            // check if all slots occupied
            if (slot.OccupyingCard == null)
            {
                allFilled = false;
                continue;
            }
        }

        // if all filled, get the puzzle result
        if (allFilled)
        {
            canExplode = true;
        }
    }
}
