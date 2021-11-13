using UnityEngine;
using UnityEngine.UI;

namespace WebXRPseudo.Weight {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        public Image pseudoHandImage;
        private bool isTouching;
        private PseudoCube pseudoCube;
        private Vector3 handOffSet;


        void Start()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            this.pseudoHandImage.enabled = false;
        }
        public void TriggerTouch(bool isTouching, PseudoCube pseudoCube=null)
        {
            this.isTouching = isTouching;
            Cursor.visible = !isTouching;
            this.pseudoHandImage.enabled = isTouching;
            if(this.isTouching && pseudoCube)
            {
                this.pseudoCube = pseudoCube;
                this.handOffSet = Input.mousePosition - Camera.main.WorldToScreenPoint(pseudoCube.transform.position);;
            }
        }

        void Update()
        {
            if(!this.isTouching) return;
            Vector3 pseudoCubePos = Camera.main.WorldToScreenPoint(this.pseudoCube.transform.position);
            (this.pseudoHandImage.gameObject.transform as RectTransform).position = pseudoCubePos + this.handOffSet;
        }
    }
}
