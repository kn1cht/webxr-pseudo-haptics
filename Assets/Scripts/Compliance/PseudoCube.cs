using UnityEngine;


namespace WebXRPseudo.Compliance {
    public class PseudoCube : MonoBehaviour
    {
        public LineRenderer line;
        public Transform trueCube;
        public float magnification = 0.2f;
        public float originDistance;
        private bool isGrabbed;
        private bool isMouse;
        private float previousSpring;
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
            this.originPosition = this.trueCube.position;
            this.GetComponent<MeshRenderer>().enabled = true;
            this.trueCube.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            SpringJoint joint = this.trueCube.gameObject.GetComponent<SpringJoint>();
            this.previousSpring = joint.spring;
            joint.spring = 0f;
            this.trueCube.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        public void GrabEnd()
        {
            this.isGrabbed = false;
            this.trueCube.position = this.transform.position;
            this.trueCube.rotation = Quaternion.Euler(Vector3.zero);
            this.trueCube.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            this.trueCube.gameObject.GetComponent<SpringJoint>().spring = this.previousSpring;
            this.trueCube.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.GetComponent<MeshRenderer>().enabled = false;
        }

        void Update()
        {
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
            Vector3[] linePos = new Vector3[2];
            this.line.GetPositions(linePos);
            if(this.isGrabbed)
            {
                float y = this.originPosition.y + (this.trueCube.position.y - this.originPosition.y) * this.magnification * (this.isMouse? 0.7f : 1f);
                this.transform.position = new Vector3(this.originPosition.x, y, this.originPosition.z);
                linePos[1].y = this.transform.position.y;
            }
            else
            {
                this.transform.position = this.trueCube.position;
                linePos[1].y = this.trueCube.position.y;
            }
            this.line.SetPositions(linePos);
        }
    }
}
