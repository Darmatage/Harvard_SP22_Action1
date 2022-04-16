using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float damageRate; // How often to take damage?

    // In case we want things besides the player to be damageable.
    private List <IDamageable> listToDamage = new List<IDamageable>();

    void Start() {
        StartCoroutine(ApplyDamage());
    }

    IEnumerator ApplyDamage() {
        while (true) {
            for (int i = 0; i < listToDamage.Count; i++) {
                listToDamage[i].TakeDamage(damage);
            }

            // Make sure the damage doesn't apply every single frame.
            yield return new WaitForSeconds(damageRate);
        }
    }

    void OnCollisionEnter(Collision collision) {
        // If Damageable, add to list
        if (collision.gameObject.GetComponent<IDamageable>() != null) {
            listToDamage.Add(collision.gameObject.GetComponent<IDamageable>());
        }
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.GetComponent<IDamageable>() != null) {
            listToDamage.Remove(collision.gameObject.GetComponent<IDamageable>());
        }
    }
}
