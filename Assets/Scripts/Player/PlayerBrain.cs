using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
        public Dictionary<ItemInformation, int> inventory;

        [SerializeField] public PlayerStats playerStats;
        [SerializeField] public WeaponController weaponController;
        [SerializeField] TMPro.TextMeshProUGUI playerHP;
        public int currentHP;
        public int defense;
        public float moveSpeed;
        bool canGetDamaged = true;

        // Start is called before the first frame update
        void Start()
        {
                playerHP.text = playerStats.hp.ToString();
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


        private void OnCollisionEnter2D(Collision2D collision)
        {
                if (collision.gameObject.tag != "enemy projectile")
                        return;
                
                if (!canGetDamaged)
                        return;

                GetComponent<Animator>().SetTrigger("player_damaged");
                canGetDamaged = false;
                currentHP --;
                playerHP.text = currentHP.ToString();
                if (currentHP <= 0)
                        GameObject.Find("UIManager").GetComponent<UIManager>().EnableUIElement(5);
                else
                        Invoke("EnableDamage", 1);
        }

        private void EnableDamage()
        {
                canGetDamaged = true;
        }


}
