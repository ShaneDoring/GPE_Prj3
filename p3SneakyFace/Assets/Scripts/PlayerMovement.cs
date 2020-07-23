using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform tf;
    public float turnSpeed = 180f;
    public float moveSpeed = 4f;
    public float backStepMoveSpeed = 2f;
   
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.player = this.gameObject;
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            tf.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            tf.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            tf.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            tf.Translate(Vector3.left * backStepMoveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
