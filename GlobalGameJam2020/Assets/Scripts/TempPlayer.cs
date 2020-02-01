using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    private bool showInventory = false;

    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private Item testItem1;
    [SerializeField]
    private Item testItem2;
    [SerializeField]
    private Item testItem3;

    // Start is called before the first frame update
    void Start()
    {
        inventory.gameObject.SetActive(showInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Constants.InputKey.INVENTORY))
        {
            showInventory = !showInventory;
            inventory.gameObject.SetActive(showInventory);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.AddItem(testItem1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            inventory.AddItem(testItem2);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.AddItem(testItem3);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            inventory.RemoveItem(testItem2);
        }
    }
}
