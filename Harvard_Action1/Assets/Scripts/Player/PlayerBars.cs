using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBars : MonoBehaviour, IDamageable
{
    public Bar brownSticky;
    public Bar health;
    public Bar whiteCloud;
    public Bar yellowJelly;
    public UnityEvent onTakeDamage;

    void Start() {
        brownSticky.curValue = brownSticky.startValue;
        health.curValue = health.startValue;
        whiteCloud.curValue = whiteCloud.startValue;
        yellowJelly.curValue = yellowJelly.startValue;

        if (!brownSticky.uiBar) {
            brownSticky.uiBar = GameObject.FindWithTag("BarBrown").GetComponent<Image>();
        }

        if (!health.uiBar) {
            health.uiBar = GameObject.FindWithTag("BarRed").GetComponent<Image>();
        }

        if (!whiteCloud.uiBar) {
            whiteCloud.uiBar = GameObject.FindWithTag("BarWhite").GetComponent<Image>();
        }

        if (!yellowJelly.uiBar) {
            yellowJelly.uiBar = GameObject.FindWithTag("BarYellow").GetComponent<Image>();
        }
    }

    void Update() {
        // Decay values over time
        brownSticky.Subtract(brownSticky.decayRate * Time.deltaTime);
        whiteCloud.Subtract(whiteCloud.decayRate * Time.deltaTime);
        // yellowJelly.Subtract(yellowJelly.decayRate * Time.deltaTime);

        // Regen values over time
        health.Add(health.regenRate * Time.deltaTime);
        yellowJelly.Add(yellowJelly.regenRate * Time.deltaTime);

        // Update Bars
        brownSticky.uiBar.fillAmount = brownSticky.GetPercentage();
        health.uiBar.fillAmount = health.GetPercentage();
        whiteCloud.uiBar.fillAmount = whiteCloud.GetPercentage();
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
        // Debug.LogFormat("Consumed: {0}", value);
        yellowJelly.Add(value);
    }

    public void Die() {
        // TODO
		SceneManager.LoadScene("EndLose");
        Debug.Log("DIE: NOOOOOOO");
    }

    public void Float(float value) {
        // Debug.LogFormat("Float: {0}", value);
        whiteCloud.Add(value);
    }

    public void Heal(float value) {
        // Debug.LogFormat("Healed: {0}", value);
        health.Add(value);
    }

    public void Sticky(float value) {
        // Debug.LogFormat("Sticky: {0}", value);
        brownSticky.Add(value);
    }

    public void TakeDamage(int value) {
		// Debug.Log("playerbars takedamage");
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

    public float GetValue() {
        return curValue;
    }

    public void Subtract(float value) {
        curValue = Mathf.Max(curValue - value, 0.0f);
    }
}

public interface IDamageable {
    void TakeDamage(int value);
}
