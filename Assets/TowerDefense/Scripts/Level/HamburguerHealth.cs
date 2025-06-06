using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class HamburguerHealth : MonoBehaviour
{
    [SerializeField] float totalHealth = 100f;
    float currentHealth;

    [SerializeField] List<Image> healthImages;
    int blockCount;

    [Header("Damage Over Time")]
    [SerializeField] float damagePerTick = 5f;
    [SerializeField] float tickRate = 1f; // Every 1 second

    public TextMeshProUGUI lifeHamburguerText;

    void Start()
    {
        currentHealth = totalHealth;
        blockCount = healthImages.Count;
        UpdateHealthImages();
        UpdateText();
    }

    void UpdateText()
    {
        lifeHamburguerText.text = (currentHealth + "%");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, totalHealth);
        UpdateHealthImages();
        UpdateText();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void UpdateHealthImages()
    {
        float percentage = currentHealth / totalHealth;
        int activeImages = Mathf.CeilToInt(percentage * blockCount);

        for (int i = 0; i < blockCount; i++)
        {
            healthImages[i].enabled = i < activeImages;
        }
    }

    // Trigger logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            InvokeRepeating("DamageOverTime", 0f, tickRate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CancelInvoke("DamageOverTime");
        }
    }

    void DamageOverTime()
    {
        TakeDamage(damagePerTick);
    }
}
