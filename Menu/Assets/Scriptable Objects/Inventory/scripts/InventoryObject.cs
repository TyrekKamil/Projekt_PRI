using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using Random = System.Random;

[CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;
    public GameObject hpItemPrefab;
   

    public void AddItem(Item _item, int _amount)
    {
        if (_item.buffs.Length > 0)
        {
            SetEmptySlot(_item, _amount);
            return;

        }
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount, false);
                return Container.Items[i];
            }
        }

        //TODO: set up a full inventory case
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount, false);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount, false);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount, false);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Vector3 playerPosition = GameObject.Find("Player").transform.position + Vector3.right;
                for (int j = 0; j < Container.Items[i].amount; j++)
                {
                    Instantiate(hpItemPrefab, playerPosition, GameObject.Find("Player").transform.rotation);
                }
                Container.Items[i].UpdateSlot(-1, null, 0, false);

            }
        }
    }

    public bool FindItem(string ItemName)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Name == ItemName)
            {
                return true;
            }
        }

        return false;
    }

    public void UseItem(Item item)
    {
        GameObject player = GameObject.Find("Player");

        if (item.Name == "HealthPotion")
        {
            PlayerUIUpdates playerUI = player.GetComponent<PlayerUIUpdates>();
            if(playerUI.currentHealth >= 81)
            {
                playerUI.currentHealth = 100;
            }
            else
            {
                playerUI.currentHealth += 20;
            }
            playerUI.OnRestoreHpFromPotion();
            ReduceNumberOfItems(item.Name);
        }

        
    }

    public int NumberOfItems(string ItemName)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Name == ItemName)
            {
                return Container.Items[i].amount;
            }
        }

        return 0;
    }

    public void ReduceNumberOfItems(string ItemName)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item.Name == ItemName)
            {
                if (Container.Items[i].amount > 1)
                {
                    Container.Items[i].ReduceAmountByOne();
                    return;
                }
                else
                {
                    Container.Items[i].UpdateSlot(-1, null, 0, false);
                }
                return;
            }
        }
    }

    //TODO: function created for getting hp pots from chest, in the future change 'hpitemprefab' gameobject to name of the prefab typed as argument
    public void GenerateNumberOfItemsFromChest(int NumberOfItems)
    {
        Random r = new Random();
        for (int i = 0; i < NumberOfItems; i++)
        {
            Vector3 chestPosition = GameObject.Find("Chest").transform.position + randomSite(r.Next(0,2));
            Debug.Log(chestPosition);
            Instantiate(hpItemPrefab, chestPosition, GameObject.Find("Chest").transform.rotation);
        }
    }

    public Vector3 randomSite(int number)
    {
        Random r = new Random();
        Vector3 vect = new Vector3(r.Next(0,3),0,0);
        if (number == 0)
        {
            return Vector3.right + vect;
        }
        else
        {
            return Vector3.left + vect;
        }
    }

    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount, false);
            }
            stream.Close();
        }
    }

    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[32];
}

[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public Item item;
    public int amount;
    public bool isVisible;

    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
        isVisible = false;
    }
    public InventorySlot(int _id,Item _item, int _amount, bool _isVisible)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        isVisible = _isVisible;
    }

    public void UpdateSlot(int _id, Item _item, int _amount, bool _isVisible)
    {
        ID = _id;
        item = _item;
        amount = _amount;
        isVisible = _isVisible;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void ReduceAmountByOne()
    {
        amount -= 1;
    }
}