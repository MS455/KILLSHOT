using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{


    [FormerlySerializedAs("health")]
    public float maxHealth;
    
    float currentHealth;

    public GameObject healthBarPrefab;

    public GameObject deathEffectPrefab;

    HealthBar myHealthBar;

 
    void Start()
    {
        currentHealth = maxHealth;
    
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBar>();

    }

    public void KillMe()
    {
        TakeDamage(currentHealth);
    }

    public void TakeDamage(float damageAmount)
    {

        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                if (deathEffectPrefab != null)
                {
                    Instantiate(deathEffectPrefab, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }

    }

    private void OnDestroy()
    {
   
        if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }
    }


    void Update()
    {

        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
      
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);


    }
}
