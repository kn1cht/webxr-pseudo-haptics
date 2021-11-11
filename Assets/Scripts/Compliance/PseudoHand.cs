using UnityEngine;

namespace WebXRPseudo.Compliance {
    public class PseudoHand : MonoBehaviour
    {
        public Transform pseudoHandModel;
        private bool isTouching;
        private bool isGrabbingPre;
        private FixedJoint parentHandJoint;
        private Transform pseudoCube;
        private MeshRenderer pseudoHandRenderer;
        private SkinnedMeshRenderer trueHandRenderer;


        void Start()
        {
            this.parentHandJoint = this.transform.parent.gameObject.GetComponent<FixedJoint>();
            this.pseudoHandRenderer = this.pseudoHandModel.gameObject.GetComponent<MeshRenderer>();
            this.trueHandRenderer = this.GetComponent<SkinnedMeshRenderer>();
            this.trueHandRenderer.enabled = true;
            this.pseudoHandRenderer.enabled = false;
        }

        public void TriggerTouch(bool isTouching, Transform pseudoCube = null)
        {
            this.isTouching = isTouching;
            if(isTouching) this.pseudoCube = pseudoCube;
        }

        void Update()
        {
            bool isGrabbing = this.parentHandJoint.connectedBody != null;
            if(this.isTouching)
            {
                if(this.isGrabbingPre == false && isGrabbing == true)
                {
                    this.pseudoHandModel.position = this.transform.position;
                    this.pseudoHandModel.rotation = this.transform.rotation;
                    this.trueHandRenderer.enabled = false;
                    this.pseudoHandRenderer.enabled = true;
                    this.pseudoHandModel.SetParent(pseudoCube);
                }
            }
            if(isGrabbing == false)
            {
                this.pseudoHandModel.parent = null;
                this.trueHandRenderer.enabled = true;
                this.pseudoHandRenderer.enabled = false;
            }
            this.isGrabbingPre = isGrabbing;
        }
    }
}
