using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
        public Dictionary<ItemInformation, int> inventory;

        [SerializeField] public PlayerStats playerStats;
        public int currentHP;
        public int defense;
        public float moveSpeed;
        bool canGetDamaged = true;

        // Start is called before the first frame update
        void Start()
        {
                currentHP = playerStats.hp;
                defense = playerStats.defense;
                moveSpeed = playerStats.moveSpeed;
                for(int index = 0; index < playerStats.activeBoosts.Count;index++)
                {
                        BoostNode boost = playerStats.activeBoosts[index];

                        while(boost != null)
                        {
                                switch (boost.boostEffect.effectType)
                                {
                                        case "HP":
                                                {
                                                        boost.boostEffect.ApplyEffectInt(ref currentHP);
                                                        break;
                                                }
                                        case "DEFENSE":
                                                {
                                                        boost.boostEffect.ApplyEffectInt(ref defense);
                                                        break;
                                                }
                                        case "SPEED":
                                                {
                                                        boost.boostEffect.ApplyEffectFloat(ref moveSpeed);
                                                        break;
                                                }
                                }
                                boost = boost.nextBoost;
                        }
                }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
                if (collision.tag != "EnemyProjectile")
                        return;

                if (!canGetDamaged)
                        return;

                int damage = 0; //projectile damage
                canGetDamaged = false;
                currentHP -= damage;
                if (currentHP <= 0)
                        Debug.Log("GAME OVER");
                else
                        Invoke("EnableDamage", 1);
        }

        private void EnableDamage()
        {
                canGetDamaged = true;
        }


}
