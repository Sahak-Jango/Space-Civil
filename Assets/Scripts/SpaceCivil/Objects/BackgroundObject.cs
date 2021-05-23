using UnityEngine;

namespace SpaceCivil.Objects {
    public class BackgroundObject : MonoBehaviour {
        public bool isVisibleOnScreen;

        private void Awake() {
            isVisibleOnScreen = false;
        }

        public Vector3 GetBottomCenterPosition() {
            var height = Controllers.CameraController.SprRenderer.bounds.size.y;
            var centerPos = gameObject.transform.position;
            return new Vector3(centerPos.x, centerPos.y - (height / 2f));
        }

        public Vector3 GetTopCenterPosition() {
            var height = Controllers.CameraController.SprRenderer.bounds.size.y;
            var centerPos = gameObject.transform.position;
            return new Vector3(centerPos.x, centerPos.y + (height / 2f));
        }
    }
}
