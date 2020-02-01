using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> items;

    [SerializeField]
    private List<Image> slots;

    private int lastSlot = 0;

    private void Start()
    {
        items = new List<Item>();
        slots = GetComponentsInChildren<Image>().ToList();
    }

    public void AddItem(Item item)
    {
        if (lastSlot >= slots.Count)
        {
            Debug.Log("Maximo de items");
            return;
        }
        items.Add(item);
        slots[lastSlot].sprite = item.Image;
        lastSlot += 1;
    }

    public void RemoveItem(Item item)
    {
        var index = items.IndexOf(item);
        var isRemove = items.Remove(item);
        if (isRemove)
            sortSlots(index);
        Debug.Log("Last remove: " + lastSlot);
    }

    private void sortSlots(int index)
    {
        for (var i = index; i < lastSlot; i++)
        {
            if (i == lastSlot-1)
                slots[i].sprite = null;
            else
                slots[i].sprite = slots[i + 1].sprite;
        }
        lastSlot -= 1;
    }
}
