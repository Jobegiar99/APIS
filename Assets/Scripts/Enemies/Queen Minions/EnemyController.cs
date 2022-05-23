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
                StartCoroutine(AttackCooldown());
        }
}
