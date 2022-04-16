using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
