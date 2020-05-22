using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    //regen variables
    [SerializeField]
    private int healthRegenSpeed = 1;
    private bool regenerating = false;

    GameManager gameManager;

    private void Start()
    {
        //sets starting health
        SetDefaults();

        //gets a reference to the game manager
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        //don't do anything if paused
        if (PauseMenu.IsOn)
        {
            CancelInvoke("RegenerateHealth");
            regenerating = false;
            return;
        }

        //starts regenerating if have taken damage, stops otherwise
        if (currentHealth < maxHealth && !regenerating)
        {
            InvokeRepeating("RegenerateHealth", 0f, 1f);
            regenerating = true;
        }
        else if(currentHealth == maxHealth && regenerating)
        {
            CancelInvoke("RegenerateHealth");
            regenerating = false;
        }
    }

    //accessor to allow access to health
    public int GetHealth()
    {
        return currentHealth;
    }

    //regenerates health
    private void RegenerateHealth()
    {
        currentHealth += healthRegenSpeed;
    }

    public void AddHealth(int _health) {
        currentHealth += _health;
    }

    //called when enemy is close enough to damage
    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        if(currentHealth <= 0f)
        {
            gameManager.SetGameOverType("died", 0);
        }
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }
}
