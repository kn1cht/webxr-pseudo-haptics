using UnityEngine;
using WebXR;

namespace WebXRPseudo.Compliance {
    public class PseudoHand : MonoBehaviour
    {
        public Transform pseudoHandModel;
        private FixedJoint parentHandJoint;
        private Transform pseudoCube;
        private MeshRenderer pseudoHandRenderer;
        private SkinnedMeshRenderer trueHandRenderer;
        private SphereCollider handCollider;
        private TrueCube trueCube;
        private WebXRController controller;


        void Start()
        {
            this.parentHandJoint = this.transform.parent.gameObject.GetComponent<FixedJoint>();
            this.pseudoHandRenderer = this.pseudoHandModel.gameObject.GetComponent<MeshRenderer>();
            this.trueHandRenderer = this.GetComponent<SkinnedMeshRenderer>();
            this.handCollider = this.GetComponent<SphereCollider>();
            this.controller = this.GetComponentInParent<WebXRController>();
            this.trueHandRenderer.enabled = true;
            this.pseudoHandRenderer.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Interactable")
                return;
            this.trueCube = other.gameObject.GetComponent<TrueCube>();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != "Interactable")
                return;
            this.trueCube = null;
        }

        private bool isControllerPickUp(WebXRController controller)
        {
            return controller.GetButtonDown(WebXRController.ButtonTypes.Trigger)
                || controller.GetButtonDown(WebXRController.ButtonTypes.Grip)
                || controller.GetButtonDown(WebXRController.ButtonTypes.ButtonA);
        }

        private bool isControllerDrop(WebXRController controller)
        {
            return controller.GetButtonUp(WebXRController.ButtonTypes.Trigger)
                || controller.GetButtonUp(WebXRController.ButtonTypes.Grip)
                || controller.GetButtonUp(WebXRController.ButtonTypes.ButtonA);
        }

        void Update()
        {
            if (isControllerPickUp(this.controller) && this.trueCube != null)
            {
                this.pseudoHandModel.position = this.transform.position;
                this.pseudoHandModel.rotation = this.transform.rotation;
                this.trueHandRenderer.enabled = false;
                this.pseudoHandRenderer.enabled = true;
                this.pseudoHandModel.SetParent(this.trueCube.pseudoCube.transform);
                this.trueCube.pseudoCube.GrabStart();
            }
            else if (isControllerDrop(this.controller) && this.trueCube != null)
            {
                this.pseudoHandModel.parent = null;
                this.trueHandRenderer.enabled = true;
                this.pseudoHandRenderer.enabled = false;
                this.trueCube.pseudoCube.GrabEnd();
            }
            this.pseudoHandModel.rotation = this.transform.rotation;
        }
    }
}
