using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public List<CardSlot> allSlots;
    
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
            //can break cards
        }
    }
}
