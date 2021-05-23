using UnityEngine;

namespace SpaceCivil.Objects {
    public class Projectile : MonoBehaviour {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float lifeTime;
        [SerializeField] private float damage;

        private void Update() {
            MoveForward();
            SelfDestruct();
        }

        private void OnTriggerEnter(Collider otherCol) {
            if (otherCol.CompareTag("Asteroid")) {
                var asteroid = otherCol.gameObject.GetComponent<Asteroid>();
                asteroid._health.ReceiveHealthDamage(damage);
                if (!asteroid._health.HealthStatus)
                    Destroy(otherCol.gameObject);
                Destroy(gameObject);
            }
        }

        private void MoveForward() {
            var velocity = new Vector3(0f, 0f, maxSpeed * Time.deltaTime);
            transform.position += transform.rotation * velocity;
        }

        private void SelfDestruct() {
            Destroy(gameObject, lifeTime);
        }
    }
}
