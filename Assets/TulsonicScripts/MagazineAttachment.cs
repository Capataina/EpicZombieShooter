using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Attachements/Magazine")]
public class MagazineAttachment : WeaponAttachmentItem
{
    public int bulletCapacity;
    public string weaponName;
}


[System.Serializable]
public class MagazineAttachmentRuntimeData : RuntimeData
{
    [SerializeField] public int bulletCount;
}
