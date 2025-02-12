﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Equipable", menuName = "Items/Equipable")]
public class Equipable_Wear : Item
{
    public Sprite Overlay;
    public bool protectGas;
    public bool autoEquip;
    public bodyPart part;

    public override void Use(ref gameItem currItem)
    {
        Player_Control player = GameController.instance.player.GetComponent<Player_Control>();
        Debug.Log("Player current item " + ((player.equipment[(int)this.part] == null) ? "nada" : ItemController.instance.items[player.equipment[(int)this.part].itemFileName].itemName) + " current Item = " + itemName);
        if (player.equipment[(int)this.part] == null || ItemController.instance.items[player.equipment[(int)this.part].itemFileName].itemName != this.itemName)
            player.ACT_Equip(currItem);
        else
            player.ACT_UnEquip(part);

    }

    public virtual void OnEquip(ref gameItem currItem)
    {

    }

    public virtual void OnDequip(ref gameItem currItem)
    {

    }

}
