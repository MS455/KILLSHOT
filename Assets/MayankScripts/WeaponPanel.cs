﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{

    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI ammoCount;

    public WeaponBehaviour myWeapon;

    public void AssignWeapon(WeaponBehaviour weapon)
    {
        myWeapon = weapon;
        weaponName.text = weapon.GetComponent<Useable>().displayName;
    }

    void Update()
    {
        if (myWeapon != null)
        {
            ammoCount.text = myWeapon.ammo.ToString();
        }
    }
}
