using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMathHelper
{
        GameObject myGameObject;
        GameObject objective;
        EnemyInformation myInfo;

        Dictionary<StateEnemyState.STATE, float> myStateValues = new Dictionary<StateEnemyState.STATE, float>
                {
                        { StateEnemyState.STATE.enemyAttack ,1 },
                        {StateEnemyState.STATE.enemyMoveToTarget,0.5f},
                        {StateEnemyState.STATE.enemyFlee, 0.5f },
                        {StateEnemyState.STATE.enemyHeal, 0f }
                };

        Dictionary<StatePlaceableState.STATE, float> placeableValues = new Dictionary<StatePlaceableState.STATE, float>
        {
                { StatePlaceableState.STATE.placeableSetTarget,0.25f },
                {StatePlaceableState.STATE.placeableAttack, 1f },
                {StatePlaceableState.STATE.placeableReload, 0.5f },
                {StatePlaceableState.STATE.placeableNoAmmo, 0f }
        };

        Dictionary<StateWeaponState.STATE, float> weaponValues = new Dictionary<StateWeaponState.STATE, float>
        {
                {StateWeaponState.STATE.weaponAttack , 1f},
                {StateWeaponState.STATE.weaponCooldown, 0.5f },
                {StateWeaponState.STATE.weaponReload, 0f },
                {StateWeaponState.STATE.weaponNoAmmo,0f }
        };


        public EnemyMathHelper(GameObject go, GameObject objective)
        {
                myGameObject = go;
                this.objective = objective;
                myInfo = myGameObject.GetComponent<EnemyInformation>();
        }
        public float GetSuccessGuess()
        {
                float myInfo = GetMyInfo();
                float fellowInfo = GetFellowInfo();
                float worldInfo = GetWorldInfo();
                float objectiveInfo = GetObjectiveInfo();
                float threatToTargetPercentage = (myInfo + fellowInfo) / 2f;
                float threatToMePercentage = (worldInfo + objectiveInfo) / 2f;
                float succesGuess = threatToTargetPercentage * (1 - threatToMePercentage);
                return succesGuess;
        }

        private float GetMyInfo()
        {

                EnemyInformation myInfo =
                         myGameObject.GetComponent<EnemyInformation>();

                Brain myBrain = myGameObject.GetComponent<Brain>();

                int currentHP = myGameObject.GetComponent<EnemyObject>().currentHP;
                float hpPercentage = (float)currentHP / myInfo.hp;

                float highestDefense = myInfo.defense;
                float strongestEnemy = myInfo.attack;
                float fastestEnemy = myInfo.moveSpeed;
                float highestAttackRange = myInfo.attackRange;

                for (int i = 0; i < myInfo.otherEnemyInfo.Count; i++)
                {
                        GetMyInfoHelper(ref highestDefense, myInfo.otherEnemyInfo[i].defense);
                        GetMyInfoHelper(ref strongestEnemy, myInfo.otherEnemyInfo[i].attack);
                        GetMyInfoHelper(ref fastestEnemy, myInfo.otherEnemyInfo[i].moveSpeed);
                        GetMyInfoHelper(ref highestAttackRange, myInfo.otherEnemyInfo[i].attackRange);

                }
                float resistancePercentage = myInfo.defense / highestDefense;

                float attackPercentage = myInfo.attack / strongestEnemy;
                float attackRangePercentage = myInfo.attackRange / highestAttackRange;
                float speedPercentage = myInfo.moveSpeed / fastestEnemy;
                float strengthPercentage = (attackPercentage + attackRangePercentage + speedPercentage) / 3f;
                float condition = hpPercentage + resistancePercentage + strengthPercentage;
                condition /= 3f;
                if (myStateValues[myBrain.enemyState.state] != 0)
                {
                        float dangerPercentage = myStateValues[myBrain.enemyState.state] - myBrain.dna.hostility;
                        if (dangerPercentage != 1)
                        {
                                condition *= (1 - Mathf.Abs(dangerPercentage));
                        }
                }
                return condition;
        }

        private void GetMyInfoHelper(ref float currentStat, float statToCompare)
        {
                if (currentStat < statToCompare)
                        currentStat = statToCompare;
        }

        public float GetFellowInfo()
        {
                PopulationManager theQueen = GameObject.Find("The Queen").GetComponent<PopulationManager>();
                List<GameObject> fellows = new List<GameObject>();
                int sameTargetCount = 0;
                for (int i = 0; i < theQueen.population.Count; i++)
                {
                        if (!theQueen.population[i].GetComponent<Brain>().alive)
                                continue;

                        float distance = Vector2.Distance(
                                        theQueen.population[i].transform.position,
                                        myGameObject.transform.position);

                        if (distance > 10)
                                continue;

                        fellows.Add(theQueen.population[i]);
                }

                if (fellows.Count == 0)
                        return 0;

                for (int i = 0; i < fellows.Count; i++)
                {
                        Brain currentFellowBrain = fellows[i].GetComponent<Brain>();
                        bool isAttacking = currentFellowBrain.enemyState.state == StateEnemyState.STATE.enemyAttack;
                        bool isSameTarget = currentFellowBrain.enemyState.state == StateEnemyState.STATE.enemyAttack;
                        if (isAttacking && isSameTarget)
                                sameTargetCount++;
                }

                return (float)sameTargetCount / (float)fellows.Count;
        }
        private float GetWorldInfo()
        {
                LevelInformation level = GameObject.Find("LevelManager").GetComponent<LevelInformation>();
                Vector2Int position =
                        new Vector2Int(
                                (int)myGameObject.transform.position.x,
                                (int)myGameObject.transform.position.y
                       );
                float free = 0;
                float total = 0;
                float hostile = 0;
                for (int row = position.x - 10; row < position.x + 10; row++)
                {
                        for (int col = position.y - 10; col < position.y + 10; col++)
                        {
                                total++;
                                if (level.terrainMatrix[row][col] == 1)
                                {
                                        free++;
                                }
                                if (level.terrainMatrix[row][col] == 2)
                                {
                                        hostile++;
                                }
                        }
                }

                float freePercentage = free / total;
                float hostilePercentage = hostile / total;
                float worldDanger = hostile / free;
                return worldDanger;
        }

        private float GetObjectiveInfo()
        {
                try
                {
                        return GetPlayerInfo();
                }
                catch
                {
                        Placeable placeable = objective.GetComponent<Placeable>();
                        if (placeable.placeableType == "hostile")
                        {
                                return GetHostileInfo(placeable);

                        }
                        return GetNeutralPlaceableInfo(placeable);
                }

        }

        private float GetPlayerInfo()
        {
                PlayerBrain playerBrain = objective.GetComponent<PlayerBrain>();
                StateWeaponState weaponController = (StateWeaponState) objective.GetComponent<WeaponController>().state;
                PlayerStats playerStats = objective.GetComponent<PlayerStats>();
                float hpPercentage = (float)playerBrain.currentHP / (float)playerStats.hp;
                float attackControl = (playerStats.attack >= 10)
                                                                ? Mathf.Pow(10, playerStats.attack.ToString().Length - 1)
                                                                : 10;
                float firePowerPercentage =
                        ((float)playerStats.attack * (float)playerStats.cooldown);
                firePowerPercentage /= attackControl;

                return firePowerPercentage * hpPercentage * weaponValues[weaponController.state];
        }

        private float GetHostileInfo(Placeable placeable)
        {
                HostilePlaceableInformation hostileInfo = objective.GetComponent<HostilePlaceableInformation>();
                float hpPercentage = (float)placeable.currentHP / (float)hostileInfo.hp;
                float attackControl = (hostileInfo.attack >= 10)
                                                        ? Mathf.Pow(10, hostileInfo.attack.ToString().Length - 1)
                                                        : 10;
                float firePowerPercentage =
                        ((float)hostileInfo.attack * (float)hostileInfo.cooldown);

                firePowerPercentage /= attackControl;
                StatePlaceableState placeableState = placeable.state;
                return firePowerPercentage * hpPercentage * placeableValues[placeableState.state];
        }

        private float GetNeutralPlaceableInfo(Placeable placeable)
        {
                PlaceableInformation placeableInfo = objective.GetComponent<PlaceableInformation>();
                float hpPercentage = (float)placeable.currentHP / (float)placeableInfo.hp;
                return hpPercentage;
        }
}
