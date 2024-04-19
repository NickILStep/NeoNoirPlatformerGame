using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            PlayerDeath.isDead = true;
            Destroy(collision.gameObject);
        }
    }
}
