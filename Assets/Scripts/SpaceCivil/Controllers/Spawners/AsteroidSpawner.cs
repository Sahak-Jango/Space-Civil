using UnityEngine;

namespace SpaceCivil.Controllers.Spawners {
    public class AsteroidSpawner : MonoBehaviour {
        private const float OffsetFromScreen = 10f;
        [SerializeField] private GameObject asteroidPrefab;
        private float _spawnAxisY;
        [SerializeField] private float spawnTime;
        [SerializeField] private float curTime;

        private void Start() {
            SetSpawnAxis_Y();
            curTime = spawnTime;
        }

        private void SetSpawnAxis_Y() {
            _spawnAxisY = CameraController.Cam.orthographicSize + OffsetFromScreen;
        }

        private void Update() {
            RandomSpawn();
        }

        private void RandomSpawn() {
            if (curTime >= spawnTime) {
                curTime -= spawnTime;
                var orthographicSize = CameraController.Cam.orthographicSize;
                var spawnAxisX = Random.Range(-orthographicSize, orthographicSize);
                Instantiate(asteroidPrefab, new Vector3(spawnAxisX, _spawnAxisY, 0f), Quaternion.identity);
            }

            curTime += Time.deltaTime;
        }
    }
}