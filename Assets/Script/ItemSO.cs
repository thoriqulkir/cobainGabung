using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType = new ItemType();

    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;


    public void UseItem()
    {
        if(statToChange == StatToChange.Health)
        {
         //   GameObject.Find("HealthManager").GetComponent<PlayerHealth>();
        }
    }

    public enum StatToChange
    {
        None,
        Health,
    }

    public enum ItemType
    {
        None,
        Heal,
        Collectable,
        Key
    };
}
