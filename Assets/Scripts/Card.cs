using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Card : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private CardSlot currentSlot;
    public void SnapToSlot(CardSlot slot)
    {
        if (currentSlot != null)
        {
            currentSlot.OccupyingCard = null;
        }

        currentSlot = slot;
        slot.OccupyingCard = this;

        // 移动到插槽位置
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // 放入插槽后，禁用物理（固定位置）
        rb.isKinematic = true;
    }

    private void OnTriggerStay(Collider other)
    {
        // 只有在未被抓取的状态下才检测吸附
        if (!grabInteractable.isSelected && other.TryGetComponent(out CardSlot slot))
        {
            // 插槽为空时才吸附
            if (slot.OccupyingCard == null)
            {
                SnapToSlot(slot);
                other.GetComponent<MeshRenderer>().enabled = false;
                CardManager.Instance.CheckCompletion();
            }
        }
    }
}
