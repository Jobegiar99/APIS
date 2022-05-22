using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName ="EnemyInformation",
        menuName = "ScriptableObjects/Enemies/Enemy Information",
        order = 1)]
public class EnemyInformation : ScriptableObject
{
        [SerializeField]
        public int attack;

        [SerializeField]
        public int hp;

        [SerializeField]
        public float attackSpeed;

        [SerializeField]
        public float moveSpeed;

        [SerializeField]
        public float defense;

        [SerializeField]
        public float attackRange;

        [SerializeField]
        public float healRate;

        [SerializeField]
        public List<EnemyInformation> otherEnemyInfo;

}
