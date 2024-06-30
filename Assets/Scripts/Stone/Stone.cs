using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IHitable
{
    public int hp;
    public int maxhp;
    [SerializeField] public int bounty;

    [SerializeField] GameObject hpBarCanvas;

    Vector2 startSize;

    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] AudioClip[] deathSounds;
    [SerializeField] DamageFlash damageFlash;

    float hpBarVisibilityTime;

    [SerializeField] ParticleSystem hitParticles;

    public GameObject spawner;

    void Start()
    {
        gameObject.AddComponent<DamageFlash>();
        damageFlash = GetComponent<DamageFlash>();
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
        

        damageFlash.CallDamageFlash();
        hp -= damage;
        hpBarVisibilityTime = 5f;
        hpBarCanvas.SetActive(true);
        UpdateHpBar();

        damageFlash.CallDamageFlash();

        if (hp <= 0)
        {
            Die();
            return;
        }

        AudioClip hitSound = GetRandomSound(hitSounds);
        if (hitSound != null)
        {
            EventManager.playSound.Invoke(hitSound);
        }
    }

    void UpdateHpBar()
    {
        hpBarCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(startSize.x * (float)hp / (float)maxhp, startSize.y);

    }

    void Die()
    {
        AudioClip deathSound = GetRandomSound(deathSounds);
        if(deathSound!= null)
        {
            EventManager.playSound.Invoke(deathSound);
        }
        Parameters.money += bounty;
        EventManager.moneyChanged.Invoke();
        EventManager.stoneDestroyed.Invoke(this);


        Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    AudioClip GetRandomSound(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    void OnDestroy()
    {
        if (spawner == null) return;
        spawner.GetComponent<StoneSpawner>().RequestRespawn();
    }
}
