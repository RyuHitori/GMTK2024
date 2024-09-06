using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject storageItemPF;
    public InventorySystem inventory;
    int count;

    public List<ObjectData> spawnedItem;

    private void Start()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

        inventory = gameManager.GetComponent<InventorySystem>();
    }

    void Update()
    {
        if(count != inventory.inventory.Count)
        {
            bool found = false;
            ObjectData dataToAdd = null;
            foreach (InventoryItem item in inventory.inventory)
            {
                foreach (ObjectData data in spawnedItem)
                {
                    if(item.data == data)
                    {
                        found = true;
                        
                    }
                }

                if(!found)
                {
                    dataToAdd = item.data;

                    GameObject newObject = Instantiate(storageItemPF, transform);
                    StorageItem storageItem = newObject.GetComponent<StorageItem>();

                    storageItem.data = dataToAdd;

                    spawnedItem.Add(dataToAdd);

                    break;


                }
                else
                {
                    count += 1;
                }
            }


            
        }
    }


}
