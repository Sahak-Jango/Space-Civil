using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpaceship : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    float maxSpeed = 3f;
    [SerializeField]
    float rotateMaxSpeed = 180f;
    [SerializeField]
    float shipBoundaryRadius = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Quaternion rot = transform.rotation;

        float z = rot.eulerAngles.z;
        z += Input.GetAxis("Horizontal") * rotateMaxSpeed * Time.deltaTime;

        rot = Quaternion.Euler(0, 0, z);

        transform.rotation = rot;

        Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * Time.deltaTime * maxSpeed, 0);

        Vector3 position = transform.position;

        position += rot * velocity;

        if(Mathf.Abs(position.y) + shipBoundaryRadius > Camera.main.orthographicSize)
        {
            if(position.y > 0)
            {
                position.y = Camera.main.orthographicSize - shipBoundaryRadius;
            }
            else
            {
                position.y = -Camera.main.orthographicSize + shipBoundaryRadius;
            }
        }

        float screenRatio = (float) Screen.width / (float) Screen.height;
        float widthOrtho = Camera.main.orthographicSize * screenRatio;

        if(Mathf.Abs(position.x) + shipBoundaryRadius > widthOrtho)
        {
            if(position.x > 0)
            {
                position.x = widthOrtho - shipBoundaryRadius;
            }
            else
            {
                position.x = -widthOrtho + shipBoundaryRadius;
            }
        }

        transform.position = position;
    }
}
