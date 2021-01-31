using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceCivil.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public static SpriteRenderer spr_renderer;

        void Awake() 
        {
            spr_renderer = AttachRenderer();
            SetOrthographicSize();
        }

        SpriteRenderer AttachRenderer()
        {
            SpriteRenderer temp_renderer;

            temp_renderer = GameObject.FindWithTag("Background").GetComponent<SpriteRenderer>();

            if(temp_renderer) return temp_renderer;

            Debug.LogError("Backgrounds does not exist in this scene!");

            return null;
        }

        void SetOrthographicSize()
        {
            float screenRatio = (float) Screen.width / (float) Screen.height;
            float bgrRatio = spr_renderer.bounds.size.x / spr_renderer.bounds.size.y;

            float orthoSize = spr_renderer.bounds.size.y / 2f;

            Camera.main.orthographicSize = (screenRatio >= bgrRatio) ? orthoSize : (orthoSize * (bgrRatio / screenRatio));
        }
    }
}