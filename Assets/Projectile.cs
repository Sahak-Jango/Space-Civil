using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float lifeTime;

    void Update()
    {
        MoveForward();
        SelfDestruct();
    }

    void MoveForward()
    {
        Vector3 position = transform.position;

        Vector3 velocity = new Vector3(0f , 0f , maxSpeed * Time.deltaTime);

        position += transform.rotation * velocity;

        transform.position = position;
    }

    void SelfDestruct()
    {
        Destroy(gameObject, lifeTime);
    }

}
