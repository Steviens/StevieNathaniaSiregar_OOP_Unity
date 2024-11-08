using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;
    private Weapon weapon;

    private void Awake()
    {
        weapon = weaponHolder;
    }

    private void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            weapon.transform.parent = other.transform;
            TurnVisual(true);
        }
    }

private void TurnVisual(bool on)
{
    // Aktifkan Animator untuk animasi pickup
    Animator animator = weapon.GetComponent<Animator>();
    if (animator != null)
    {
        animator.SetBool("isPickedUp", on);
    }

    // Atur komponen visual yang sesuai
    weapon.gameObject.SetActive(on);
}


    private void TurnVisual(bool on, Weapon newWeapon)
    {
        // Mengatur senjata baru dan mengaktifkan/menonaktifkan komponen visual
        weapon = newWeapon;
        gameObject.SetActive(on);
    }
}
