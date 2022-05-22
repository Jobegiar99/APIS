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

        // Update is called once per frame
        void Update()
        {
                if (Input.GetKeyDown(up))
                        transform.position += new Vector3(0, 1, 0) * Time.deltaTime * playerBrain.moveSpeed;

                if(Input.GetKeyDown(down))
                        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * playerBrain.moveSpeed;

                if(Input.GetKeyDown(left))
                        transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * playerBrain.moveSpeed;

                if (Input.GetKeyDown(right))
                        transform.position += new Vector3(1, 0, 0) * Time.deltaTime * playerBrain.moveSpeed;
        }
}
