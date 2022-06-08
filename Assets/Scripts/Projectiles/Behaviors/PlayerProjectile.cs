using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
        [SerializeField] PlayerWeaponBehavior playerWeapon;
        [SerializeField] List<Sprite> projectileSprites;
        [SerializeField] float speed;
        public Vector3 direction;
        public int damage;
        // Start is called before the first frame update
        void OnEnable()
        {
                direction.x *= Random.Range(0.5f,1);
                direction.y *= Random.Range(0.5f, 1);
                direction = direction.normalized;
                GetComponent<SpriteRenderer>().sprite = projectileSprites[Random.Range(0, projectileSprites.Count)];
                
                transform.Rotate(0, 0, Vector3.Angle(direction,transform.position));
        }

        // Update is called once per frame
        void OnDisable()
        {
                playerWeapon.projectiles.Enqueue(gameObject);
        }

        private void Update()
        {
                transform.position += direction * speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
                if (collision.gameObject.tag == "Player")
                        return;
                this.gameObject.SetActive(false);
        }
}
