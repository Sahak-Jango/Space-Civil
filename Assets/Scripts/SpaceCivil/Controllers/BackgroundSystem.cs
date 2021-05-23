using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SpaceCivil.Objects;

namespace SpaceCivil.Controllers {
    public class BackgroundSystem : MonoBehaviour {
        public List<BackgroundObject> backgroundSet;
        private const float BackgroundScrollSpeed = 5f;
        private const int MAXBackgroundAmount = 6;

        #region Start
        private void Awake() {
            backgroundSet = Generate_Backgrounds();
        }

        private static List<BackgroundObject> Generate_Backgrounds() {
            var backgrSet = GameObject.FindGameObjectsWithTag("Background").
                Select(obj => obj.GetComponent<BackgroundObject>()).ToList();
            if (backgrSet.Count != 0) {
                if(backgrSet.Count != MAXBackgroundAmount) {
                    Debug.LogWarning("Not all backgrounds are in the scene!");
                }
                return backgrSet;
            }
            Debug.LogError("Backgrounds does not exist in this scene!");
            return null;
        }
        #endregion

        void Update() {
            if (backgroundSet.Count != 0) {
                ScrollBackgrounds();
                SetBackgroundsVisibility();
                SetBackgroundsStatus();
                RepositionOf_BelowScreenBackground();
            } else {
                Debug.LogError("Set of background objects is empty!");
            }
        }

        private void ScrollBackgrounds() {
            var velocity = new Vector3(0f, BackgroundScrollSpeed * Time.deltaTime, 0f);
            foreach (var bgrObj in backgroundSet) {
                bgrObj.gameObject.transform.position -= velocity;
            }
        }

        private void SetBackgroundsVisibility() {
            Vector3 tempBottomCenterPos, tempTopCenterPos, tempBottomViewportPoint, tempTopViewportPoint;
            foreach(var bgrObj in backgroundSet) {
                tempBottomCenterPos = bgrObj.GetBottomCenterPosition();
                tempTopCenterPos = bgrObj.GetTopCenterPosition();
                tempBottomViewportPoint = CameraController.Cam.WorldToViewportPoint(tempBottomCenterPos);
                tempTopViewportPoint = CameraController.Cam.WorldToViewportPoint(tempTopCenterPos);
                if ((tempBottomViewportPoint.y >= 0f && tempBottomViewportPoint.y <= 1f)
                || (tempTopViewportPoint.y >= 0f && tempTopViewportPoint.y <= 1f) 
                || (tempTopViewportPoint.y > 1f && tempBottomViewportPoint.y <= 1f)
                || (tempBottomViewportPoint.y < 0f && tempTopViewportPoint.y >= 0f)) {
                    bgrObj.isVisibleOnScreen = true;
                    continue;
                }

                bgrObj.isVisibleOnScreen = false;
            }
        }

        private void SetBackgroundsStatus() {
            foreach (var bgrObj in backgroundSet) {
                if (!bgrObj.isVisibleOnScreen) {
                    bgrObj.gameObject.SetActive(false);
                    continue;
                }
                bgrObj.gameObject.SetActive(true);
            }
        }

        private void RepositionOf_BelowScreenBackground() {
            var lowestBgr = backgroundSet[0];
            var tempTopCenterPos = lowestBgr.GetTopCenterPosition();
            var tempTopViewportPoint = CameraController.Cam.WorldToViewportPoint(tempTopCenterPos);
            if (!(tempTopViewportPoint.y < 0f)) {
                return;
            }
            var highestBgr = backgroundSet[MAXBackgroundAmount - 1];
            var bgrHeight = CameraController.SprRenderer.bounds.size.y;
            var bgrObj = lowestBgr.gameObject;
            var position = new Vector3(0f, highestBgr.gameObject.transform.position.y + bgrHeight, 0f);
            bgrObj.transform.position = position;
            backgroundSet.RemoveAt(0);
            backgroundSet.Add(lowestBgr);
        }
    }
}
