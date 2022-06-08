using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
        [SerializeField] public PlaceableInformation placeableInformation;
        [SerializeField] GameObject gameOverScreen;

        public float immunityTime = 1f;
        private bool canGetDamage = true;
        public int hp;

        // Start is called before the first frame update
        void Start()
        {
                hp = placeableInformation.hp;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.tag != "enemy projectile")
                        return;

                if (!canGetDamage)
                        return;

                hp -= collision.gameObject.GetComponent<PlaceableProjectile>().damage;
                if (hp <= 0)
                {
                        StopAllCoroutines();
                        gameOverScreen.SetActive(true);
                        Destroy(this.gameObject);
                }
                else
                {
                        canGetDamage = false;
                        StartCoroutine(StopCooldown());
                }
        }

        public IEnumerator StopCooldown()
        {
                yield return new WaitForSecondsRealtime(immunityTime);

                 canGetDamage = true;
                
        }
}
