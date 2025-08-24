using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Text healthText;

    private Health localPlayerHealth;

    public void SetPlayer(Health playerHealth)
    {
        localPlayerHealth = playerHealth;

        UpdateHealthUI(localPlayerHealth.NetworkedHealth);
    }

    public void OnHealthChanged(int newHealth)
    {
        UpdateHealthUI(newHealth);
    }

    private void UpdateHealthUI(int value)
    {
        if (healthText != null)
        {
            healthText.text = value.ToString();
        }
    }
}