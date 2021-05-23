using UnityEngine;

namespace SpaceCivil.Controllers {
    public class CameraController : MonoBehaviour {
        public static SpriteRenderer SprRenderer;
        public static Camera Cam { get; private set; }

        private void Awake() {
            Cam = Camera.main;
            AttachRenderer();
            SetOrthographicSize();
        }

        private static void AttachRenderer() {
            SprRenderer = GameObject.FindWithTag("Background").GetComponent<SpriteRenderer>();
            if (SprRenderer) {
                return;
            }
            Debug.LogError("Backgrounds does not exist in this scene!");
            SprRenderer = null;
        }

        private static void SetOrthographicSize() {
            var screenRatio = (float) Screen.width / (float) Screen.height;
            var bounds = SprRenderer.bounds;
            var bgrRatio = bounds.size.x / bounds.size.y;
            var orthoSize = bounds.size.y / 2f;
            if (!(Cam is null))
                Cam.orthographicSize =
                    (screenRatio >= bgrRatio) ? orthoSize : (orthoSize * (bgrRatio / screenRatio));
        }
    }
}