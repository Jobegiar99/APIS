using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMathHelper
{
        GameObject myGameObject;
        public GameObject objective;

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
                {StatePlaceableState.STATE.placeableCooldown, 0.5f },
                {StatePlaceableState.STATE.placeableNoAmmo, 0f }
        };

        Dictionary<StateWeaponState.STATE, float> weaponValues = new Dictionary<StateWeaponState.STATE, float>
        {
                {StateWeaponState.STATE.weaponAttack , 1f},
                {StateWeaponState.STATE.weaponCooldown, 0.5f },
                {StateWeaponState.STATE.weaponNoAmmo,0f }
        };

        Brain myBrain;
        EnemyController myController;
        List<Brain> otherBrains;
        List<EnemyController> otherControllers;

        public EnemyMathHelper(GameObject go, GameObject objective)
        {
                this.myGameObject = go;
                this.myBrain = go.GetComponent<Brain>();
                this.myController = go.GetComponent<EnemyController>();
                this.objective = objective;
                this.otherBrains = new List<Brain>();
                this.otherControllers = new List<EnemyController>();

                for (int i = 0; i < myBrain.fellowMinions.Count; i++)
                {
                        Brain otherBrain = myBrain.fellowMinions[i].GetComponent<Brain>();
                        EnemyController otherController = myBrain.fellowMinions[i].GetComponent<EnemyController>();

                        otherBrains.Add(otherBrain);
                        otherControllers.Add(otherController);
                }
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
                float hpPercentage = (float)myController.hp / (float)myController.maxHP;
                float highestDefense = myController.defense;
                float strongestEnemy = myController.attack;
                float fastestEnemy = myController.moveSpeed;
                float highestAttackRange = myController.attackRange;

                for (int i = 0; i < myBrain.fellowMinions.Count; i++)
                {
                        GetMyInfoHelper(ref highestDefense, otherControllers[i].defense);
                        GetMyInfoHelper(ref strongestEnemy, otherControllers[i].attack);
                        GetMyInfoHelper(ref fastestEnemy, otherControllers[i].moveSpeed);
                        GetMyInfoHelper(ref highestAttackRange, otherControllers[i].attackRange);
                }
                float resistancePercentage = myController.defense / highestDefense;

                float attackPercentage = (float)(myController.attack / strongestEnemy);
                float attackRangePercentage = (float)(myController.attackRange / highestAttackRange);
                float speedPercentage = (float)(myController.moveSpeed / fastestEnemy);
                float strengthPercentage = (attackPercentage + attackRangePercentage + speedPercentage) / 3f;
                float condition = hpPercentage + resistancePercentage + strengthPercentage;
                condition /= 3f;
                StateEnemyState.STATE state = myBrain.enemyState.state;
                float stateValue = myStateValues[state];
                if (stateValue != 0)
                {
                        float dangerPercentage = stateValue - myBrain.dna.hostility;
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

        private float GetFellowInfo()
        {
                float sameTargetCount = 0;
                for (int i = 0; i < myBrain.fellowMinions.Count; i++)
                {
                        EnemyController otherController = myBrain.fellowMinions[i].gameObject.GetComponent<EnemyController>();
                        Brain otherBrain = myBrain.fellowMinions[i].gameObject.GetComponent<Brain>();

                        if (myBrain.enemyState.objective != otherBrain.enemyState.objective)
                                continue;

                        if (!otherBrain.gameObject.activeSelf)
                                continue;

                        if (otherBrain.enemyState.objective == myBrain.enemyState.objective)
                                sameTargetCount++;
                }
                return (float)(sameTargetCount / myBrain.fellowMinions.Count);
        }
        private float GetWorldInfo()
        {
                LevelManager level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
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
                                if (row > 0 && row < level.levelInformation.terrainMatrix.Count && col > 0 && col < level.levelInformation.terrainMatrix.Count)
                                {
                                        if (level.levelInformation.terrainMatrix[row][col] == 1)
                                        {
                                                free++;
                                        }
                                        if (level.levelInformation.terrainMatrix[row][col] == 2)
                                        {
                                                hostile++;
                                        }
                                }
                        }
                }

                float freePercentage = free / total;
                float hostilePercentage = hostile / total;
                float worldDanger = hostilePercentage / freePercentage;
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
                        HostilePlaceable hostilePlaceable = objective.GetComponent<HostilePlaceable>();
                        if (placeable == null)
                        {
                                return GetHostileInfo(hostilePlaceable);

                        }
                        return GetNeutralPlaceableInfo(placeable);
                }
        }

        private float GetPlayerInfo()
        {
                PlayerBrain playerBrain = objective.GetComponent<PlayerBrain>();
                StateWeaponState weaponController = (StateWeaponState)objective.GetComponent<WeaponController>().state;
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

        private float GetHostileInfo(HostilePlaceable placeable)
        {
                if (placeable == null)
                        return 1;

                HostilePlaceableInformation hostileInfo = placeable.hostilePlaceableInfo;
                float hpPercentage = (float)placeable.hp / (float)hostileInfo.hp;
                float attackControl = (hostileInfo.attack >= 10)
                                                        ? Mathf.Pow(10, hostileInfo.attack.ToString().Length - 1)
                                                        : 10;
                float firePowerPercentage =
                        ((float)hostileInfo.attack * (float)hostileInfo.attackCooldown);

                firePowerPercentage /= attackControl;
                StatePlaceableState placeableState = (StatePlaceableState)placeable.state;
                return firePowerPercentage * hpPercentage * placeableValues[placeableState.state];
        }

        private float GetNeutralPlaceableInfo(Placeable placeable)
        {
                if (placeable == null)
                        return 1;

                PlaceableInformation placeableInfo = placeable.placeableInformation;
                float hpPercentage = (float)placeable.hp / (float)placeableInfo.hp;
                return hpPercentage;
        }
}
