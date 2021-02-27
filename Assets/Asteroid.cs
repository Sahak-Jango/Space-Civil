using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    const float MOV_NORMAL_SPEED = 15f;
    const float ROT_NORMAL_SPEED = 60f;

    const float MIN_SCALE = 0.75f;
    const float MAX_SCALE = 2.5f;

    Transform transform;
    BoxCollider2D col;

    float moveMaxSpeed = 0f;
    float rotateMaxSpeed = 0f;

    [SerializeField]
    float scaleMulti = 1f;

    void Awake()
    {
        transform = GetComponent<Transform>();
        col = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        scaleMulti = Random.Range(MIN_SCALE, MAX_SCALE);
        col.size *= scaleMulti;

        moveMaxSpeed = MOV_NORMAL_SPEED * scaleMulti;
        rotateMaxSpeed = ROT_NORMAL_SPEED * scaleMulti;
        transform.localScale *= scaleMulti;
    }


    void Update() 
    {
        MoveDown();
        RotateOnItsAxis();
        
        if(IsUnderScreen())
            Destroy(gameObject);
    } 

    void MoveDown()
    {
        var position = transform.position;

        position += Vector3.Scale(new Vector3(0f, moveMaxSpeed, 0f), Vector3.down) * Time.deltaTime;

        transform.position = position;
    }

    void RotateOnItsAxis()
    {
        transform.RotateAround(transform.position, Vector3.right, rotateMaxSpeed * Time.deltaTime);
    }

    bool IsUnderScreen()
    {
        Bounds boxBounds = col.bounds;

        Vector3 topCenter_pos = new Vector3(boxBounds.center.x, boxBounds.center.y + boxBounds.extents.y, 0f);

        Vector3 topViewportPoint = Camera.main.WorldToViewportPoint(topCenter_pos);

        return (topViewportPoint.y >= 0f) ? false : true;
    }
    
    
}
