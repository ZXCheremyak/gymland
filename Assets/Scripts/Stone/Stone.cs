using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IHitable
{
    public int hp;
    public int maxhp;
    [SerializeField] int bounty;

    [SerializeField] GameObject hpBarCanvas;

    Vector2 startSize;

    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] DamageFlash damageFlash;

    float hpBarVisibilityTime;

    void Start()
    {
        hp = maxhp;
        startSize = hpBarCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
    }

    void Update()
    {
        if (hpBarVisibilityTime <= 0)
        {
            if(hpBarCanvas.active)
            {
                hpBarCanvas.SetActive(false);
            }
            return;
        }
        hpBarVisibilityTime -= Time.deltaTime;
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
        hpBarVisibilityTime = 5f;
        hpBarCanvas.SetActive(true);
        UpdateHpBar();

        damageFlash.CallDamageFlash();

        if (hp <= 0)
        {
            Die();
        }
    }

    void UpdateHpBar()
    {
        hpBarCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(startSize.x * (float)hp / (float)maxhp, startSize.y);

    }

    void Die()
    {
        if(deathSound!= null)
        {
            EventManager.playSound.Invoke(deathSound);
        }
        Parameters.money += bounty;
        EventManager.moneyChanged.Invoke();
        EventManager.stoneDestroyed.Invoke(this);
        Destroy(gameObject);
    }
}
