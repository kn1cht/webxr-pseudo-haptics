using UnityEngine;
using UnityEngine.UI;
using System;
using WebXR;

namespace WebXRPseudo.Weight {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;
        public Image pseudoHandImage;
        private bool isTouching;
        private PseudoCube pseudoCube;
        private Vector3 handOffSet;
        private WebXRManager webXRManager;

        void Start()
        {
            Cursor.SetCursor(handCursor, new Vector2(32f, 32f), CursorMode.ForceSoftware);
            this.pseudoHandImage.enabled = false;
            this.webXRManager = GameObject.Find("WebXRCameraSet").GetComponent<WebXRManager>();
        }
        public void TriggerTouch(bool isTouching, PseudoCube pseudoCube=null)
        {
            this.isTouching = isTouching;
            this.pseudoHandImage.enabled = isTouching;
            if(this.isTouching && pseudoCube)
            {
                this.pseudoCube = pseudoCube;
                this.handOffSet = Input.mousePosition - Camera.main.WorldToScreenPoint(pseudoCube.transform.position);;
            }
        }

        void Update()
        {
            try
            {
                Cursor.visible = !(isTouching || webXRManager.XRState == WebXRState.VR); // disable cursor in VR mode
            }
            catch
            {
                Cursor.visible = !isTouching;
            }
            if(!this.isTouching) return;
            Vector3 pseudoCubePos = Camera.main.WorldToScreenPoint(this.pseudoCube.transform.position);
            (this.pseudoHandImage.gameObject.transform as RectTransform).position = pseudoCubePos + this.handOffSet;
        }
    }
}
