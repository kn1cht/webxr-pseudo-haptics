using UnityEngine;
using UnityEngine.UI;

namespace WebXRPseudo.Friction {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        public Image pseudoHandImage;
        private bool isTouching;
        private bool isTouchingPre;
        private float magnification;
        private Vector3 originPosition;
        private PseudoZone pseudoZone;

        void Start()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            this.originPosition = Input.mousePosition;
            this.pseudoHandImage.enabled = false;
        }

        public void TriggerTouch(bool isTouching, PseudoZone zone = null)
        {
            this.isTouching = isTouching;
            if(isTouching && !this.isTouchingPre)
            {
                Cursor.visible = false;
                this.pseudoHandImage.enabled = true;
                this.pseudoZone = zone;
                this.magnification = zone.magnification;
                (this.pseudoHandImage.gameObject.transform as RectTransform).position = Input.mousePosition;
                this.originPosition = Input.mousePosition;
            }
            else if(!isTouching && isTouchingPre)
            {
                Cursor.visible = true;
                this.pseudoHandImage.enabled = false;
            }
            this.isTouchingPre = this.isTouching;
        }

        void Update()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            if(!this.isTouching) return;

            (this.pseudoHandImage.gameObject.transform as RectTransform).position = originPosition + (Input.mousePosition - originPosition) * this.magnification;

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
        }
    }
}
