using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceCivil.Controllers
{
public class MainSpaceship : MonoBehaviour
    {
        const float MAX_Z_ROT = 5f;

        Rigidbody2D rb;
        [SerializeField]
        float maxSpeed;
        [SerializeField]
        float rotateMaxSpeed;
        [SerializeField]
        float shipBoundaryRadius;
        [SerializeField]
        float shootingDelay;
        [SerializeField]
        float shootingClock = 0f;
        [SerializeField]
        Transform leftWeapon;
        [SerializeField]
        Transform rightWeapon;

        [SerializeField]
        GameObject projectilePrefab;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Move();
            Shoot();
        }

        #region Movement

        void Move()
        {

            transform.rotation = MovementIncline();

            var velocity = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * maxSpeed, 0f , 0f);

            var position = transform.position;

            position += velocity;

            position = MovementBoundaries(position);

            transform.position = position;

        }

        Vector3 MovementBoundaries(Vector3 position)
        {
            var screenRatio = (float) Screen.width / (float) Screen.height;
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

            return position;
        }

        Quaternion MovementIncline()
        {
            var rotation = transform.rotation;

            float z_deg = rotation.eulerAngles.z;
            
            if(Mathf.Abs(Input.GetAxis("Horizontal")) != 0)
            {
                z_deg -= Input.GetAxis("Horizontal") * rotateMaxSpeed * Time.deltaTime;
                rotation = InclineBoundaries(z_deg);
            }
            else
            {
                rotation = Quaternion.Lerp(rotation, Quaternion.Euler(0f, 0f, 0f), 1.0f * Time.deltaTime * (rotateMaxSpeed / 5f));
            }

            return rotation;
        }

        Quaternion InclineBoundaries(float z_deg)
        {
            if(z_deg >= MAX_Z_ROT && z_deg < 90f)
            {
                z_deg = MAX_Z_ROT;
            }
            else if(z_deg <= 360f -MAX_Z_ROT && z_deg > 270f)
            {
                z_deg = 360f - MAX_Z_ROT;
            }

            return Quaternion.Euler(0, 0, z_deg);
        }

        #endregion

        void Shoot()
        {
            shootingClock -= Time.deltaTime;

            if(Input.GetButton("Fire1") && shootingClock <= 0f)
            {
                shootingClock = shootingDelay;
                Instantiate(projectilePrefab, leftWeapon.position, leftWeapon.rotation);
                Instantiate(projectilePrefab, rightWeapon.position, rightWeapon.rotation);
            }
        }

    }
}
