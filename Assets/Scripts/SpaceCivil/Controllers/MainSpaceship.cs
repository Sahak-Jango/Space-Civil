using SpaceCivil.Attributes;
using UnityEngine;

namespace SpaceCivil.Controllers {

		public class MainSpaceship : MonoBehaviour { 
            
            private const float MAXRotZ = 5f;
		    private const float MAXHealth = 1000f;
            [SerializeField] private float maxSpeed;
            [SerializeField] private float rotateMaxSpeed;
            [SerializeField] private float shipBoundaryRadius;
            [SerializeField] private float shootingDelay;
            [SerializeField] private float shootingClock = 0f;
            [SerializeField] private GameObject projectilePrefab;
            [SerializeField] private Transform leftWeapon;
            [SerializeField] private Transform rightWeapon;
            private HealthSystem _health;
            private Rigidbody2D _rb;
            private CapsuleCollider _col;

            private void Awake() {
                _rb = GetComponent<Rigidbody2D>();
                _col = GetComponent<CapsuleCollider>();
                _health = new HealthSystem(MAXHealth, 0f);
            }

            private void Update() {
                Move();
                Shoot();
            }

            private void OnTriggerEnter(Collider otherCol) {
                if (otherCol.CompareTag("Asteroid")) {
                    var asteroid = otherCol.gameObject.GetComponent<Objects.Asteroid>();
                    _health.ReceiveHealthDamage(asteroid.damage);
                    Destroy(otherCol.gameObject);
                }
                if (!_health.HealthStatus) {
                    GameOver();
                }
            }

            #region Movement
            private void Move() {
                transform.rotation = MovementIncline();
                var velocity = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * maxSpeed, 0f , 0f);
                var position = transform.position;
                position += velocity;
                position = MovementBoundaries(position);
                transform.position = position;
            }

            private Vector3 MovementBoundaries(Vector3 position) {
                var screenRatio = (float) Screen.width / (float) Screen.height;
                var widthOrtho = CameraController.Cam.orthographicSize * screenRatio;
                if (!(Mathf.Abs(position.x) + shipBoundaryRadius > widthOrtho)) {
                    return position;
                }
                if (position.x > 0) {
                    position.x = widthOrtho - shipBoundaryRadius;
                } else {
                    position.x = -widthOrtho + shipBoundaryRadius;
                }
                return position;
            }

            private Quaternion MovementIncline() {
                var rotation = transform.rotation;
                var zDeg = rotation.eulerAngles.z;      
                if (Mathf.Abs(Input.GetAxis("Horizontal")) != 0) {
                    zDeg -= Input.GetAxis("Horizontal") * rotateMaxSpeed * Time.deltaTime;
                    rotation = InclineBoundaries(zDeg);
                } else {
                    rotation = Quaternion.Lerp(rotation,
                        Quaternion.Euler(0f, 0f, 0f), 1.0f * Time.deltaTime * (rotateMaxSpeed / 5f));
                }
                return rotation;
            }

            private Quaternion InclineBoundaries(float zDeg) {
                if (zDeg >= MAXRotZ && zDeg < 90f) {
                    zDeg = MAXRotZ;
                } else if (zDeg <= 360f -MAXRotZ && zDeg > 270f) {
                    zDeg = 360f - MAXRotZ;
                }
                return Quaternion.Euler(0, 0, zDeg);
            }
            #endregion

            private void Shoot() {
                shootingClock -= Time.deltaTime;
                if (!Input.GetButton("Fire1") || !(shootingClock <= 0f)) {
                    return;
                }
                shootingClock = shootingDelay;
                Instantiate(projectilePrefab, leftWeapon.position, leftWeapon.rotation);
                Instantiate(projectilePrefab, rightWeapon.position, rightWeapon.rotation);
            }

            private void GameOver() {
                Destroy(gameObject);
                Debug.Log("You have lost!");
                Time.timeScale = 0;
            }
    }
}
