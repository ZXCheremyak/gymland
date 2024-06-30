using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        Invoke("SelfDestroy", 1f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2) * 50, Random.Range(1, 2) * 400));
        text.text = Parameters.power.ToString();
    }


    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
