using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostilePlaceable : MonoBehaviour
{
        [SerializeField] public HostilePlaceableInformation hostilePlaceableInfo;
        [SerializeField] public GameObject projectilePrefab;
        [SerializeField] GameObject GameOverScreen;
        public bool canAttack;
        public bool canGetDamage;

        public State state;

        public int hp;
        public int rounds;
        public int projectilesPerRound;
        public int attack;
        public float attackCooldown;
        public float attackRange;
        public float roundCooldown;
        public float getDamageCooldown;
        // Start is called before the first frame update
        void Start()
        {
                state = new StatePlaceableSetTarget(gameObject, null);
                hp = hostilePlaceableInfo.hp;
                rounds = hostilePlaceableInfo.rounds;
                projectilesPerRound = hostilePlaceableInfo.projectilesPerRound;
                attack = hostilePlaceableInfo.attack;
                attackCooldown = hostilePlaceableInfo.attackCooldown;
                attackRange = hostilePlaceableInfo.attackRange;
                getDamageCooldown = 1f;
        }

        // Update is called once per frame
        void Update()
        {
                state = state.Process();
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
                        GameOverScreen.SetActive(true);
                        Destroy(this.gameObject);
                }
                else
                {
                        canGetDamage = false;
                        StartCoroutine(StopCooldown(getDamageCooldown,"get damage"));
                }
        }

        public IEnumerator StopCooldown(float cooldown, string targetCooldown = "attack")
        {
                yield return new WaitForSecondsRealtime(cooldown);
                if(targetCooldown == "attack")
                        canAttack = true;
                else
                {
                        canGetDamage = true;
                }
        }

        public void CreateProjectile(GameObject objective)
        {
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
}
