using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/EnemyShoot_Settings")]

public class EnemyShoot_Settings : ScriptableObject
{
    public string name;

    public float speed;

    public float startTimeBetweenShots;

    public GameObject projectile;

    //remove if too janky

}
