using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float maxHealth;

    private float currentHealth;

    private string id = "Enemy";

    private GameManager gameManager;

    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GotShot(float _damage)
    {
        currentHealth -= _damage;
        if(currentHealth <= 0f)
        {
            gameManager.KillEnemy(this.gameObject);
        }
    }

    public void SetHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
    }

    public void SetID(string _id)
    {
        id = _id;
    }
}
