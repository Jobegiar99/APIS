using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
 #region Attributes
        #region Serialized Fields
        [SerializeField] public EnemyInformation enemyInformation;
        
        #endregion

        #region  EnemyStats
        public int attack;
        public int maxHP;
        public int hp;
        public float attackSpeed;
        public float moveSpeed;
        public float defense;
        public float attackRange;
        public float healRate;
        #endregion

        #region StateHelpers
        public bool canHeal = true;
        public bool canAttack = true;
        public bool alive = true;
        private int semaphore = 1;
        #endregion

        #region Object Pooling
        public Transform projectilePool;
        public Queue<GameObject> projectiles;

        #endregion
#endregion

        // Start is called before the first frame update
        void Start()
        {
                this.attack = enemyInformation.attack;
                this.maxHP = enemyInformation.hp;
                this.hp = maxHP;
                this.attackSpeed = enemyInformation.attackSpeed;
                this.moveSpeed = enemyInformation.moveSpeed;
                this.defense = enemyInformation.defense;
                this.attackRange = enemyInformation.attackRange;
                this.healRate = enemyInformation.healRate;
                
                
        }

        public void InitPool()
        {
                projectiles = new Queue<GameObject>();
                for (int i = 0; i < projectilePool.childCount; i++)
                {
                        projectiles.Enqueue(projectilePool.GetChild(i).gameObject);
                }
        }

        public IEnumerator HealCooldown()
        {
                yield return new WaitForSecondsRealtime(healRate);
                canHeal = true;
                hp += 5;
                if (hp > 100)
                        hp = 100;  
        }

        public IEnumerator AttackCooldown()
        {
                yield return new WaitForSecondsRealtime(attackSpeed);
                canAttack = true;
        }

        public void Attack(GameObject objective)
        {
                canAttack = false;
                GameObject projectile = projectiles.Dequeue();
                projectile.transform.position = transform.position;
 
                Vector3 direction = objective.transform.position - transform.position;

                projectile.GetComponent<EnemyProjectile>().enemyController = this;
                projectile.GetComponent<EnemyProjectile>().direction = direction;
                projectile.GetComponent<EnemyProjectile>().damage = this.attack;
                projectile.SetActive(true);

                StartCoroutine(AttackCooldown());
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {

                if (!this.gameObject.activeSelf)
                {
                        return;
                }

                else if(collision.gameObject.tag == "player projectile")
                {
                        int totalDamage = collision.gameObject.GetComponent<PlayerProjectile>().damage;
                        totalDamage = (int)( totalDamage * defense);

                        if (totalDamage <= 0)
                                totalDamage = 1;

                        this.hp -= collision.gameObject.GetComponent<PlayerProjectile>().damage;

                        if (hp <= 0 && alive) 
                        {
                                alive = false;
                                GameObject.Find("The Queen").GetComponent<PopulationManager>().RemoveEnemy();
                                this.gameObject.SetActive(false);
                        }
                        
                }


        }

}
