using UnityEngine;
using UnityEngine.UI;
using System;
using WebXR;

namespace WebXRPseudo.MacroRoughness {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        public Image pseudoHandImage;
        private bool isCursorHidden = true;
        private bool isTouching;
        private bool isTouchingPre;
        private float curveZ;
        private float maxdepth;
        private PseudoZone pseudoZone;
        private WebXRManager webXRManager;

        void Start()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            this.pseudoHandImage.enabled = false;
            this.webXRManager = GameObject.Find("WebXRCameraSet").GetComponent<WebXRManager>();
        }

        public void TriggerTouch(bool isTouching, PseudoZone zone = null, float curveZ = 0f, float maxdepth = 1f)
        {
            this.isTouching = isTouching;
            if(isTouching && !this.isTouchingPre)
            {
                this.isCursorHidden = true;
                this.curveZ = curveZ;
                this.maxdepth = maxdepth;
                this.pseudoHandImage.enabled = true;
                this.pseudoZone = zone;
                (this.pseudoHandImage.gameObject.transform as RectTransform).position = Input.mousePosition;
                (this.pseudoHandImage.gameObject.transform as RectTransform).localScale = Vector3.one;
            }
            else if(!isTouching && isTouchingPre)
            {
                this.isCursorHidden = false;
                this.pseudoHandImage.enabled = false;
                (this.pseudoHandImage.gameObject.transform as RectTransform).localScale = Vector3.one;
            }
            this.isTouchingPre = this.isTouching;
        }

        void Update()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            try
            {
                Cursor.visible = !(isCursorHidden || webXRManager.XRState == WebXRState.VR); // disable cursor in VR mode
            }
            catch
            {
                Cursor.visible = !isCursorHidden;
            }
            if(!this.isTouching) return;

            Vector3 currentPos = Input.mousePosition;
            (this.pseudoHandImage.gameObject.transform as RectTransform).position = currentPos;
            Ray pseudoRay = Camera.main.ScreenPointToRay(currentPos);
            RaycastHit hit;
            int layerMask = 1 << 3;
            layerMask = ~layerMask;
            if (Physics.Raycast(pseudoRay, out hit, 100f, layerMask))
            {
                float depthRate = (hit.point.z - this.curveZ) / this.maxdepth;
                float scale = 1 - Mathf.Sin(depthRate * Mathf.PI / 2f) / 3f;
                (this.pseudoHandImage.gameObject.transform as RectTransform).localScale = Vector3.one * scale;
            }
            // judge if pseudo-cursor exits from pseudo-haptics zone
            if (Physics.Raycast(pseudoRay, out hit, 100f))
            {
                // judge if pseudo-cursor exits from pseudo-haptics zone
                if(hit.transform.gameObject.name != this.pseudoZone.gameObject.name)
                    this.TriggerTouch(false);
            }
            else {
                this.TriggerTouch(false);
            }
        }
    }
}
