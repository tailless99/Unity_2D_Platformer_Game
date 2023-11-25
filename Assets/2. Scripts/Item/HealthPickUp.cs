using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int HealthRestore = 20;
    public Vector3 spinrotationSpeed = new Vector3 (0, 180, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageAble damageAble = collision.GetComponent<DamageAble>();

        if(damageAble)
        {
            bool wasHealed = damageAble.Heal(HealthRestore);

            if(wasHealed) Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinrotationSpeed * Time.deltaTime;
    }
}
