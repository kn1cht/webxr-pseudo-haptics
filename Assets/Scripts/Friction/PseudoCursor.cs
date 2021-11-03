using UnityEngine;

namespace WebXRPseudo.Friction {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        private bool isTouching;
        private bool isTouchingPre;
        private float magnification;
        private Vector3 originPosition;
        private Transform pseudoCursor;
        private SpriteRenderer pseudoCursorRenderer;
        private PseudoZone pseudoZone;


        void Start()
        {
            Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.ForceSoftware);
            this.originPosition = Input.mousePosition;
            this.pseudoCursor = this.GetComponentInChildren<Transform>();
            this.pseudoCursorRenderer = this.pseudoCursor.GetComponentInChildren<SpriteRenderer>();
            this.pseudoCursorRenderer.enabled = false;
        }

        public void TriggerTouch(bool isTouching, PseudoZone zone = null)
        {
            this.isTouching = isTouching;
            if(isTouching && !this.isTouchingPre)
            {
                Cursor.visible = false;
                this.pseudoCursorRenderer.enabled = true;
                this.pseudoZone = zone;
                this.magnification = zone.magnification;
                this.pseudoCursor.position = MousePos2WorldPos(Input.mousePosition);
                this.originPosition = MousePos2WorldPos(Input.mousePosition);
            }
            else if(!isTouching && isTouchingPre)
            {
                Cursor.visible = true;
                this.pseudoCursorRenderer.enabled = false;
            }
            this.isTouchingPre = this.isTouching;
        }

        private Vector3 MousePos2WorldPos(Vector3 mousePos) {
            mousePos.z = Camera.main.nearClipPlane + 0.5f;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }

        void Update()
        {
            Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.ForceSoftware);
            if(this.isTouching)
            {
                this.pseudoCursor.position = originPosition + (MousePos2WorldPos(Input.mousePosition) - originPosition) * this.magnification;

                // judge if pseudo-cursor exits from pseudo-haptics zone
                Vector3 pseudoScreenPos = Camera.main.WorldToScreenPoint(this.pseudoCursor.position);
                Ray pseudoRay = Camera.main.ScreenPointToRay(pseudoScreenPos);
                RaycastHit hit;
                if (Physics.Raycast(pseudoRay, out hit)) {
                    Transform objectHit = hit.transform;
                    if(hit.transform.gameObject.name != this.pseudoZone.gameObject.name)
                        this.TriggerTouch(false);
                }
                else {
                    this.TriggerTouch(false);
                }
            }
        }
    }
}
