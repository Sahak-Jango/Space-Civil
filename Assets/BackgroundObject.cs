using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceCivil.Objects
{
    public class BackgroundObject : MonoBehaviour
    {
        [SerializeField]
        public bool IsVisibleOnScreen;

        void Awake()
        {
            IsVisibleOnScreen = false;
        }

        public Vector3 GetBottomCenterPosition()
        {
            float height = SpaceCivil.Controllers.CameraController.spr_renderer.bounds.size.y;

            Vector3 center_pos = gameObject.transform.position;

            return new Vector3(center_pos.x, center_pos.y - (height / 2f));
        }

        public Vector3 GetTopCenterPosition()
        {
            float height = SpaceCivil.Controllers.CameraController.spr_renderer.bounds.size.y;
            Vector3 center_pos = gameObject.transform.position;

            return new Vector3(center_pos.x, center_pos.y + (height / 2f));
        }

        public void SetVisibility(bool visibility)
        {
            IsVisibleOnScreen = visibility;
        }
    }
}
