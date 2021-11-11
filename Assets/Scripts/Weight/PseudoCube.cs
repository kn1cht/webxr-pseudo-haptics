using UnityEngine;


namespace WebXRPseudo.Weight {
    public class PseudoCube : MonoBehaviour
    {
        public Transform trueCube;
        public float magnification = 0.2f;
        private bool isGrabbed;
        private bool isMouse;
        private Vector3 originPosition;


        void Start()
        {
            this.originPosition = this.trueCube.position;
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        public void GrabStart(bool isMouse = false)
        {
            this.isGrabbed = true;
            this.isMouse = isMouse;
            this.originPosition = trueCube.position;
            this.GetComponent<MeshRenderer>().enabled = true;
            this.trueCube.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        public void GrabEnd()
        {
            this.isGrabbed = false;
            this.trueCube.position = this.transform.position;
            this.trueCube.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        void Update()
        {
            this.transform.rotation = this.trueCube.rotation;
            if(this.isGrabbed)
                this.transform.position = this.originPosition + (this.trueCube.position - this.originPosition) * this.magnification * (this.isMouse? 0.7f : 1f);
            else
                this.transform.position = this.trueCube.position;
        }
    }
}
