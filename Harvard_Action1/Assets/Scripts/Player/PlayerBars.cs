using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour, IDamageable
{
    public Bar health;
    public Bar yellowJelly;
    public UnityEvent onTakeDamage;

    void Start() {
        health.curValue = health.startValue;
        yellowJelly.curValue = yellowJelly.startValue;
    }

    void Update() {
        // Decay values over time
        // yellowJelly.Subtract(yellowJelly.decayRate * Time.deltaTime);

        // Regen values over time
        health.Add(health.regenRate * Time.deltaTime);

        // Update Bars
        health.uiBar.fillAmount = health.GetPercentage();
        yellowJelly.uiBar.fillAmount = yellowJelly.GetPercentage();

        // Is Player Dead?
        if (health.curValue == 0.0f) {
            Die();
        }
    }

    public void Burn(Bar bar, float value) {
        bar.Subtract(value);
    }

    public void Consume(float value) {
        Debug.LogFormat("Consumed: {0}", value);
        yellowJelly.Add(value);
    }

    public void Die() {
        // TODO
        Debug.Log("DIE: NOOOOOOO");
    }

    public void Heal(float value) {
        Debug.LogFormat("Headled: {0}", value);
        health.Add(value);
    }

    public void TakeDamage(int value) {
        health.Subtract(value);
        onTakeDamage?.Invoke();
    }
}

[System.Serializable]
public class Bar {
    public float curValue;
    public float decayRate; // Decay over time?
    public float maxValue;
    public float regenRate; // Regenerate over time?
    public float startValue;
    public Image uiBar;

    public void Add(float value) {
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public float GetPercentage() {
        return curValue / maxValue;
    }

    public void Subtract(float value) {
        curValue = Mathf.Max(curValue - value, 0.0f);
    }
}

public interface IDamageable {
    void TakeDamage(int value);
}
