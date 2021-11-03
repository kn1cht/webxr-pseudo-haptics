using UnityEngine;

namespace WebXRPseudo.Friction {
    public class PseudoHand : MonoBehaviour
    {
        public Transform pseudoHandModel;
        public SkinnedMeshRenderer trueHandRenderer;
        private bool isTouching;
        private float magnification;
        private MeshRenderer pseudoHandRenderer;
        private Vector3 originPosition;


        void Start()
        {
            this.originPosition = this.transform.position;
            this.trueHandRenderer = this.GetComponent<SkinnedMeshRenderer>();
            this.trueHandRenderer.enabled = true;
            this.pseudoHandRenderer = this.pseudoHandModel.gameObject.GetComponent<MeshRenderer>();
            this.pseudoHandRenderer.enabled = false;
        }

        public void TriggerTouch(bool isTouching, float magnification = 0f)
        {
            this.isTouching = isTouching;
            if(isTouching)
            {
                this.magnification = magnification;
                this.pseudoHandModel.position = this.transform.position;
                this.pseudoHandModel.rotation = this.transform.rotation;
                this.originPosition = this.transform.position;
                this.trueHandRenderer.enabled = false;
                this.pseudoHandRenderer.enabled = true;
            }
            else
            {
                this.trueHandRenderer.enabled = true;
                this.pseudoHandRenderer.enabled = false;

            }
        }

        void Update()
        {
            this.pseudoHandModel.rotation = this.transform.rotation;
            if(this.isTouching)
                this.pseudoHandModel.position = originPosition + (this.transform.position - originPosition) * this.magnification;
        }
    }
}
