using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        [SerializeField] public KeyCode up;
        [SerializeField] public KeyCode down;
        [SerializeField] public KeyCode left;
        [SerializeField] public KeyCode right;
        [SerializeField] public PlayerBrain playerBrain;
        [SerializeField] public WeaponController playerWeapon;
        [SerializeField] Animator playerAnimator;
        private bool playerMoved;
        private Vector3 targetPosition;
        private void OnEnable()
        {
                playerMoved = false;
                targetPosition = new Vector3(0, 0, 0);
        }
        // Update is called once per frame
        void Update()
        {
                playerMoved = false;
                targetPosition = new Vector3(0, 0, 0);
                if (Input.GetKey(up))
                {
                        targetPosition += new Vector3(0, 1, 0);
                        playerMoved = true;
                }

                else if (Input.GetKey(down))
                {
                        targetPosition += new Vector3(0, -1, 0);
                        playerMoved = true;
                }

                if (Input.GetKey(left)) 
                {
                        targetPosition += new Vector3(-1, 0, 0);
                        transform.localScale = new Vector3(-1, 1, 1);
                        playerMoved = true;
                }
                else if (Input.GetKey(right)) 
                {
                        targetPosition += new Vector3(1, 0, 0);
                        transform.localScale = new Vector3(1, 1, 1);
                        playerMoved = true;
                }
                if( playerMoved ) 
                {
                        transform.position += targetPosition * Time.deltaTime * playerBrain.moveSpeed;
                        playerAnimator.SetTrigger("player_move");
                }
                else
                {
                        playerAnimator.SetTrigger("player_iddle");
                }
               
        }
}
