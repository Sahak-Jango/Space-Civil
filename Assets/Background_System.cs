using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SpaceCivil.Objects;

namespace SpaceCivil.Controllers
{
    public class Background_System : MonoBehaviour
    {
        public List<BackgroundObject> background_set;

        const float BACKGROUND_SCROLL_SPEED = 5f;

        const int MAX_BACKGROUND_AMOUNT = 6;

        #region Start

        void Awake() 
        {
            background_set = Generate_Backgrounds();
        }

        List<BackgroundObject> Generate_Backgrounds()
        {
            List<BackgroundObject> backgr_set = new List<BackgroundObject>();

            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Background"))
            {
                backgr_set.Add(obj.GetComponent<BackgroundObject>());
            }

            if(backgr_set.Count != 0)
            {
                if(backgr_set.Count != MAX_BACKGROUND_AMOUNT)
                {
                    Debug.LogWarning("Not all backgrounds are in the scene!");
                }
                return backgr_set;
            }

            Debug.LogError("Backgrounds does not exist in this scene!");
            return null;
        }

        #endregion

        void Update()
        {
            if(background_set.Count != 0)
            {
                ScrollBackgrounds();
                SetBackgroundsVisibility();
                SetBackgroundsStatus();
                RepositionOf_BelowScreenBackground();
            }
            else
            {
                Debug.LogError("Set of background objects is empty!");
            }
        }

        void ScrollBackgrounds()
        {
            Vector3 velocity = new Vector3(0f, BACKGROUND_SCROLL_SPEED * Time.deltaTime, 0f);

            foreach(BackgroundObject bgr_obj in background_set)
            {
                bgr_obj.gameObject.transform.position -= velocity;
            }
        }

        void SetBackgroundsVisibility()
        {
            Vector3 temp_bottom_center_pos, temp_top_center_pos, temp_bottomViewportPoint, temp_topViewportPoint;

            foreach(BackgroundObject bgr_obj in background_set)
            {
                temp_bottom_center_pos = bgr_obj.GetBottomCenterPosition();
                temp_top_center_pos = bgr_obj.GetTopCenterPosition();

                temp_bottomViewportPoint = Camera.main.WorldToViewportPoint(temp_bottom_center_pos);
                temp_topViewportPoint = Camera.main.WorldToViewportPoint(temp_top_center_pos);

                if((temp_bottomViewportPoint.y < 0f && temp_topViewportPoint.y >= 0f) 
                || (temp_topViewportPoint.y > 1f && temp_bottomViewportPoint.y <= 1f) 
                || (temp_bottomViewportPoint.y >= 0f && temp_bottomViewportPoint.y <= 1f) || (temp_topViewportPoint.y >= 0f && temp_topViewportPoint.y <= 1f))

                if((temp_bottomViewportPoint.y >= 0f && temp_bottomViewportPoint.y <= 1f)
                || (temp_topViewportPoint.y >= 0f && temp_topViewportPoint.y <= 1f) 
                || (temp_topViewportPoint.y > 1f && temp_bottomViewportPoint.y <= 1f)
                || (temp_bottomViewportPoint.y < 0f && temp_topViewportPoint.y >= 0f))
                {
                    bgr_obj.SetVisibility(true);
                    continue;
                }

                bgr_obj.SetVisibility(false);

            }
        }

        void SetBackgroundsStatus()
        {
            foreach(BackgroundObject bgr_obj in background_set)
            {
                if(!bgr_obj.IsVisibleOnScreen)
                {
                    bgr_obj.gameObject.SetActive(false);
                    continue;
                }
                bgr_obj.gameObject.SetActive(true);
            }
        }

        void RepositionOf_BelowScreenBackground()
        {
            Vector3 temp_top_center_pos, temp_topViewportPoint;

            BackgroundObject lowest_bgr = background_set[0];

            temp_top_center_pos = lowest_bgr.GetTopCenterPosition();

            temp_topViewportPoint = Camera.main.WorldToViewportPoint(temp_top_center_pos);

            if(temp_topViewportPoint.y < 0f)
            {
                BackgroundObject highest_bgr = background_set[MAX_BACKGROUND_AMOUNT - 1];

                float bgr_height = SpaceCivil.Controllers.CameraController.spr_renderer.bounds.size.y;

                lowest_bgr.gameObject.transform.position = new Vector3(0f, highest_bgr.gameObject.transform.position.y + bgr_height, 0f);

                background_set.RemoveAt(0);

                background_set.Add(lowest_bgr);
            }
        }
    }
}
