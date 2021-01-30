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
            float ortho_size = spr_renderer.bounds.size.x * Screen.height / Screen.width * 0.5f;
            Camera.main.orthographicSize = ortho_size;
        }
    }
}