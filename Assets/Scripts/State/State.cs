using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
        protected State nextState;

        protected GameObject myGameObject;

        
        public enum STATE
        {

        };

        public  enum STAGE
        {
                Enter, Update, Exit, None
        };

        public  STAGE stage;
        public STATE state;


        public State(GameObject go)
        {
                this.myGameObject = go;
        }

        public virtual void Enter()
        {
                stage = STAGE.Update;
        }

        public virtual void Update()
        {
                stage = STAGE.Update;
        }

        public virtual void Exit()
        {
                stage = STAGE.Exit;
        }

        public State Process()
        {
                if (stage == STAGE.Enter)
                        Enter();

                if (stage == STAGE.Update)
                        Update();

                if (stage == STAGE.Exit)
                {
                        Exit();
                        return nextState;
                }

                return this;
        }

}
