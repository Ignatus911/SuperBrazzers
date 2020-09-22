using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1000f;
    [SerializeField] float jumpForce = 5f;
    float xVelocity;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        ChangeSpriteDirection();
        Jump();
    }

    private void Run()
    {
        xVelocity = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, 0f);
        GetComponent<Animator>().SetBool("isRunning", xVelocity != 0);
    }

    private void ChangeSpriteDirection()
    {
        GetComponent<Transform>().localScale = new Vector3
            (Mathf.Sign(xVelocity), transform.localScale.y);
    }

    private void Jump()
    {
        //if (Input.GetButtonDown("Jump"))
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("press Jump");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce * Time.deltaTime);
        }

    }
}
