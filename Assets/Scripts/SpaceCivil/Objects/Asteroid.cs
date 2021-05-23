using SpaceCivil.Controllers;
using SpaceCivil.Attributes;
using UnityEngine;

namespace SpaceCivil.Objects {
    public class Asteroid : MonoBehaviour {
        private const float NormalHealth = 20f;
        private const float Density = 5.3f;
        private const float MovNormalSpeed = 10f;
        private const float RotNormalSpeed = 60f;
        private const float MINScale = 0.9f;
        private const float MAXScale = 2.5f;
        [SerializeField] private float scaleMulti;
        public HealthSystem _health;
        private new Transform _transform;
        private MeshCollider _col;
        private float _moveMaxSpeed = 0f;
        private float _rotateMaxSpeed = 0f;
        private float _mass;
        public float damage;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _col = GetComponent<MeshCollider>();
        }

        private void Start() {
            scaleMulti = Random.Range(MINScale, MAXScale);
            _moveMaxSpeed = MovNormalSpeed * scaleMulti;
            _rotateMaxSpeed = RotNormalSpeed * scaleMulti;
            _transform.localScale *= scaleMulti;
            _mass = Density * scaleMulti;
            damage = _mass * _moveMaxSpeed;
            _health = new HealthSystem(NormalHealth * _mass, 0f);
        }

        private void Update() {
            MoveDown();
            RotateOnItsAxis();
            if (IsUnderScreen()) {
                Destroy(gameObject);
            }
        }

        private void MoveDown() {
            var position = _transform.position;
            position += Vector3.Scale(new Vector3(0f, _moveMaxSpeed, 0f), Vector3.down) * Time.deltaTime;
            _transform.position = position;
        }

        void RotateOnItsAxis() {
            _transform.RotateAround(_transform.position, Vector3.right, _rotateMaxSpeed * Time.deltaTime);
        }

        bool IsUnderScreen() {
            var boxBounds = _col.bounds;
            var topCenterPos = new Vector3(boxBounds.center.x, boxBounds.center.y + boxBounds.extents.y, 0f);
            var topViewportPoint = CameraController.Cam.WorldToViewportPoint(topCenterPos);
            return (!(topViewportPoint.y >= 0f));
        }
    }
}
