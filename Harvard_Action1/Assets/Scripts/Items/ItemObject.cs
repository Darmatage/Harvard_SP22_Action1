using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable {
  private PlayerBars bars;
  public GameHandler gameHandler;
  public ItemData item;

  void Awake() {
    
  }

  void Start() {
    bars = GameObject.FindWithTag("Player").GetComponent<PlayerBars>();
    gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
  }

  IEnumerator DestroyThis() {
    yield return new WaitForSeconds(0.1f);

    Destroy(gameObject);
  }

  public string GetInteractPrompt() {
    return string.Format("Pickup {0}", item.displayName); // Not sure if we want to use this?
  }

  public void OnInteract() {
    StartCoroutine(DestroyThis()); // Once "consumed", destroy it.
  }

  public void OnTriggerEnter2D (Collider2D other) {
    //Debug.LogFormat("Item Type {0}", item.type);
    if (other.gameObject.tag == "Player") {
      // Player has hit consumable item
      if (item.type == ItemType.Consumable) {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AudioSource>().Play();

        // Apply all stat updates from consumeable
        for (int i = 0; i < item.consumables.Length; i++) {
          //Debug.LogFormat("Consumable Type {0}", item.consumables[i].type);
          switch (item.consumables[i].type) {
            case ConsumableType.RedHeart:
              //Debug.Log("Consuming Heart");
              bars.Heal(item.consumables[i].value);
              break;
            case ConsumableType.YellowJelly:
              //Debug.Log("Consuming YellowJelly");
              bars.Consume(item.consumables[i].value);
              break;
          }
        }

        StartCoroutine(DestroyThis());
      }
    }
  }
}
