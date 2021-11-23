using UnityEngine;
using UnityEngine.UI;
using System;
using WebXR;

namespace WebXRPseudo.FineRoughness {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        public Image pseudoHandImage;
        private bool isCursorHidden = true;
        private bool isTouching;
        private bool isTouchingPre;
        private float roughnessLevel;
        private MovingAverage movingAverage;
        private PseudoZone pseudoZone;
        private Vector3 lastPos;
        private WebXRManager webXRManager;

        void Start()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            this.movingAverage = new MovingAverage();
            this.pseudoHandImage.enabled = false;
            this.webXRManager = GameObject.Find("WebXRCameraSet").GetComponent<WebXRManager>();
        }

        public void TriggerTouch(bool isTouching, PseudoZone zone = null)
        {
            this.isTouching = isTouching;
            if(isTouching && !this.isTouchingPre)
            {
                this.isCursorHidden = true;
                this.pseudoHandImage.enabled = true;
                this.pseudoZone = zone;
                this.lastPos = Input.mousePosition;
                (this.pseudoHandImage.gameObject.transform as RectTransform).position = Input.mousePosition;
                this.roughnessLevel = zone.roughnessLevel;
            }
            else if(!isTouching && isTouchingPre)
            {
                this.isCursorHidden = false;
                this.pseudoHandImage.enabled = false;
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

            float velocity = movingAverage.average(Mathf.Min(((Input.mousePosition - this.lastPos) / Time.deltaTime).magnitude, 500f));
            float x = Input.mousePosition.x + Perturber.perturber(this.roughnessLevel, velocity, 0.025f);
            float y = Input.mousePosition.y + Perturber.perturber(this.roughnessLevel, velocity, 0.025f);
            (this.pseudoHandImage.gameObject.transform as RectTransform).position = new Vector2(x, y);

            // judge if pseudo-cursor exits from pseudo-haptics zone
            Vector3 pseudoScreenPos = (this.pseudoHandImage.gameObject.transform as RectTransform).position;
            Ray pseudoRay = Camera.main.ScreenPointToRay(pseudoScreenPos);
            RaycastHit hit;
            if (Physics.Raycast(pseudoRay, out hit)) {
                if(hit.transform.gameObject.name != this.pseudoZone.gameObject.name)
                    this.TriggerTouch(false);
            }
            else {
                this.TriggerTouch(false);
            }
            this.lastPos = Input.mousePosition;
        }
    }
}
