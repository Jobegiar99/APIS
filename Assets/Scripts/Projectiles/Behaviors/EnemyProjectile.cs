using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
        public EnemyController enemyController;
        float speed = 2f;
        public Vector3 direction;
        public int damage;
        // Start is called before the first frame update
        void OnEnable()
        {
                direction = direction.normalized;
                transform.Rotate(0, 0, Vector3.Angle(direction, transform.position));
        }

        // Update is called once per frame
        void OnDisable()
        {
                enemyController.projectiles.Enqueue(gameObject);
        }

        private void Update()
        {
                transform.position += direction * speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
                if (collision.gameObject.tag == "enemy projectile" || collision.gameObject.tag == "enemy")
                        return;
                this.gameObject.SetActive(false);
        }
}
