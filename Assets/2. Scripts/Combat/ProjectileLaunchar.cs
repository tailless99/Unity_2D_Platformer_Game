using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunchar : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform launchPoint;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 originScale= projectile.transform.localScale;

        float value = transform.localScale.x > 0 ? 1 : -1;
        projectile.transform.localScale = new Vector3
        {
            x = originScale.x * value,
            y = originScale.y * value,
            z = originScale.z
        };
    }
}
