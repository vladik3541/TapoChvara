using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event Action OnDeath;
    public float health = 100;
    private Animator animator;
    private UIManager manager;
    private bool isDie = false;
    private void Start()
    {
        manager = FindObjectOfType<UIManager>();
        manager.InitSliderEnemyHealth(health);
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float amount)
    {
        if(health>0)
        {
            health -= amount;
            manager.UpdateEnemyHealth(health);
        }
        else if (health <= 0)
        {
            if (isDie) return;
            isDie = true;
            health = 0;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        OnDeath?.Invoke();
        Destroy(gameObject,5);
    }
}
