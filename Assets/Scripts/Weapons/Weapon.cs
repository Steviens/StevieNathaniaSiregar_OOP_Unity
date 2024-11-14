using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;

    [Header("Bullets")]


    [SerializeField] private Bullet bullet;

    [SerializeField] private float shootIntervalInSeconds = 3f;
    [SerializeField] private Transform bulletSpawnPoint;

    private IObjectPool<Bullet> objectPool;
    private float timer;

    void Update()
    {
        // Logika penembakan bullet
    }
}
