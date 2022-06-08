using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponBehavior : MonoBehaviour, InterfaceWeaponBehavior
{
        [SerializeField] Transform projectilePool;
        [SerializeField] AudioSource soundEffects;
        [SerializeField] AudioClip shotSound;
        [SerializeField] AudioClip reloadSound;
        public Queue<GameObject> projectiles;


        public void Start()
        {
                projectiles = new Queue<GameObject>();
                for (int i = 0; i < projectilePool.childCount; i++)
                        projectiles.Enqueue(projectilePool.GetChild(i).gameObject);
        }
        public void ShootProjectile()
        {
                soundEffects.Stop();
                soundEffects.clip = shotSound;
                soundEffects.Play();
                for (int i = 0; i < 8; i++)
                {
                        GameObject projectile = projectiles.Dequeue();
                        projectile.transform.position = transform.parent.position;
                        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Vector3 direction = new Vector3(mousePosition.x, mousePosition.y, 0);
                        direction -= transform.parent.transform.position;

                        projectile.GetComponent<PlayerProjectile>().direction = direction;

                        projectile.GetComponent<PlayerProjectile>().damage =
                                GameObject.Find("playerWeapon").GetComponent<WeaponController>().damage;

                        projectile.SetActive(true);
                }

        }
        
}
