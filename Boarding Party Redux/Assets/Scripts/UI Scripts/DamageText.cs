using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public Animator anim;
    public string damageTextAnimation;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetDamageText(Vector3 pos, float damage)
    {
        
    }
}
