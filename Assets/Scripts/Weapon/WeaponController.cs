using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
        [SerializeField] public WeaponStats weaponStats;

        public State state;
        public int damage;
        public float cooldown;
        public float ammo;
        public bool canAttack = false;
        
        // Start is called before the first frame update
        void Start()
        {
                state = new StateWeaponAttack(this.gameObject);
                damage = weaponStats.damage;
                cooldown = weaponStats.cooldown;
                ammo = weaponStats.ammo;
        }

        // Update is called once per frame
        void Update()
        {
                state = state.Process();
        }

        public IEnumerator StopCooldown()
        {
                yield return new WaitForSecondsRealtime (cooldown);
                canAttack = true;
                
        }

}
