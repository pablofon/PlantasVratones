using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float damageTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //EJEMPLO DE ACCESO DIRECTO + USANDO VALORES PROPIOS
            GameObject hitObject = collision.gameObject;
            StunPlayer stunPlayer = hitObject.GetComponent<StunPlayer>();
            //StunPlayer stunScript = collision.gameObject.GetComponent<StunPlayer>();
            stunPlayer.StunEffect(damageTime);
        }
    }
}
