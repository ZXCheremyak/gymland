using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IHitable
{
    public int hp;
    public int maxhp;
    [SerializeField] int bounty;

    [SerializeField] GameObject hpBar;

    Vector2 startSize;

    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;

    void Start()
    {
        hp = maxhp;
        startSize = hpBar.GetComponent<RectTransform>().sizeDelta;
    }

    public void Hit(int damage)
    {
        TakeDamage(damage);
    }

    void TakeDamage(int damage)
    {
        if (hitSound != null)
        {
            EventManager.playSound.Invoke(hitSound);
        }

        hp -= damage;
        UpdateHpBar();

        if (hp <= 0)
        {
            Die();
        }
    }

    void UpdateHpBar()
    {
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(startSize.x * (float)hp / (float)maxhp, startSize.y);

    }

    void Die()
    {
        if(deathSound!= null)
        {
            EventManager.playSound.Invoke(deathSound);
        }
        Parameters.money += bounty;
        EventManager.moneyChanged.Invoke(bounty);
        EventManager.stoneDestroyed.Invoke();
        Destroy(gameObject);
    }
}
