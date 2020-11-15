using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;
   

    public void AddItem(Item _item, int _amount)
    {
        if(_item.buffs.Length > 0)
        {
            Container.Items.Add(new InventorySlot(_item.Id, _item, _amount, false));
            return;

        }
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if(Container.Items[i].item.Id == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        Container.Items.Add(new InventorySlot(_item.Id, _item, _amount, false));
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
            Container = (Inventory)formatter.Deserialize(stream);
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
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;
    public bool isVisible;
    public InventorySlot(int _id,Item _item, int _amount, bool _isVisible)
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
}