using System.Collections;
using System.Collections.Generic;
using Items.Definitions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
internal sealed class InventorySlot : MonoBehaviour
{
    private void Start()
    {
        var img = GetComponent<Image>();
    }

    public void UpdateSlot(Item item)
    {

    }
}
