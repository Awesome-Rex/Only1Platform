using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool dead;

    public float maxHealth;
    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health > maxHealth)
            {
                _health = maxHealth;
            } 
            if (_health <= 0f)
            {
                _health = 0f;
                dead = true;
            }

            if (_health > 0)
            {
                dead = false;
            }
        }
    }
    [SerializeField]
    private float _health;

    private void Awake()
    {
        dead = false;

    }
}
