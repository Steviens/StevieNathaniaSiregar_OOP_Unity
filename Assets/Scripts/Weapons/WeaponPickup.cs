using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Referensi senjata yang akan dipasangkan
    private Weapon weapon; // Senjata yang akan diambil

    private void Awake()
    {
        weapon = weaponHolder; // Menginisialisasi weapon dengan weaponHolder
        Debug.Log($"Weapon picked up");
    }

    private void Start()
    {
        // if (weapon != null)
        // {
        //     TurnVisual(false); // Menonaktifkan visual senjata pada awal
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && weapon != null)
        {
            // Mengambil komponen Weapon dari Player
            Player player = other.GetComponent<Player>(); 

            if (player.thisweapon != null)
            {
                player.thisweapon.gameObject.SetActive(false); // Menonaktifkan senjata lama
            }

            // Instansiasi senjata tanpa parent terlebih dahulu
            Weapon bebas = Instantiate(weapon);

            // Set parent setelah instansiasi
            bebas.transform.SetParent(other.transform);
            bebas.transform.localPosition = Vector2.zero;
            bebas.transform.localRotation = Quaternion.identity;

            player.thisweapon = bebas; // Mengatur senjata yang baru ke player
            TurnVisual(true, bebas); // Memanggil TurnVisual dengan weapon baru
        }
    }

    private void TurnVisual(bool on)
    {
        gameObject.SetActive(on); // Mengubah status aktif objek visual
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        weapon.gameObject.SetActive(on); // Mengubah tampilan visual senjata
    }
}