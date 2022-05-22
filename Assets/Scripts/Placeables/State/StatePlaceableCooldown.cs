using UnityEngine;

public class StatePlaceableCooldown : StatePlaceableState
{
        public StatePlaceableCooldown(GameObject go,GameObject obj)
                : base(go, obj) { }

        public override void Enter()
        {
                base.Enter();
                state = STATE.placeableCooldown;
                hostilePlaceable.canAttack = false;
                hostilePlaceable.StartCoroutine(
                        hostilePlaceable.StopCooldown(hostilePlaceable.attackCooldown)
                );
        }

        public override void Update()
        {
                base.Update();

                if (!hostilePlaceable.canAttack)
                        return;

                stage = STAGE.Exit;
        }

        public override void Exit()
        {
                base.Exit();
                nextState = new StatePlaceableSetTarget(myGameObject,objective);
        }
}
