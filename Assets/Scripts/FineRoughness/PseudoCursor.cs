using UnityEngine;
using UnityEngine.UI;

namespace WebXRPseudo.FineRoughness {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        public Image pseudoHandImage;
        private bool isTouching;
        private bool isTouchingPre;
        private float roughnessLevel;
        private MovingAverage movingAverage;
        private PseudoZone pseudoZone;
        private Vector3 lastPos;

        void Start()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            this.movingAverage = new MovingAverage();
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
                this.lastPos = Input.mousePosition;
                (this.pseudoHandImage.gameObject.transform as RectTransform).position = Input.mousePosition;
                this.roughnessLevel = zone.roughnessLevel;
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

            float velocity = movingAverage.average(Mathf.Min(((Input.mousePosition - this.lastPos) / Time.deltaTime).magnitude, 500f));
            float x = Input.mousePosition.x + Perturber.perturber(this.roughnessLevel, velocity, 0.025f);
            float y = Input.mousePosition.y + Perturber.perturber(this.roughnessLevel, velocity, 0.025f);
            (this.pseudoHandImage.gameObject.transform as RectTransform).position = new Vector2(x, y);

            // judge if pseudo-cursor exits from pseudo-haptics zone
            Vector3 pseudoScreenPos = (this.pseudoHandImage.gameObject.transform as RectTransform).position;
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
            this.lastPos = Input.mousePosition;
        }
    }
}
