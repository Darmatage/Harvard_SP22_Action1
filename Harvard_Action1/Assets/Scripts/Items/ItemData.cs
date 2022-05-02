using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConsumableType {
  BrownSticky,
  Cloud,
  RedHeart,
  YellowJelly
}

public enum ItemType {
  Consumable,
  // Resource
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject {
  [Header("Info")]
  public string displayName;
  public string desc;
  public Sprite icon;
  public GameObject prefab;
  public ItemType type;

  [Header("Consumable")]
  public ItemDataConsumable[] consumables;
}

[System.Serializable]
public class ItemDataConsumable {
  public ConsumableType type;
  public float value;
}
