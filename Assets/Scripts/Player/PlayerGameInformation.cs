using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameInformation 
{
        public string username;
        public string currentGameState;
        public PlayerGameInformation(string username, string currentGameState)
        {
                this.username = username;
                this.currentGameState = currentGameState;
        }
}
