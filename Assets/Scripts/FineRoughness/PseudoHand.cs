using UnityEngine;

namespace WebXRPseudo.FineRoughness {
    public class PseudoHand : MonoBehaviour
    {
        public Transform fingerTip;
        public Transform pseudoHandModel;
        private SkinnedMeshRenderer trueHandRenderer;
        private bool isTouching;
        private float roughnessLevel;
        private MeshRenderer pseudoHandRenderer;
        private Vector3 lastPos;


        void Start()
        {
            this.trueHandRenderer = this.GetComponent<SkinnedMeshRenderer>();
            this.trueHandRenderer.enabled = true;
            this.pseudoHandRenderer = this.pseudoHandModel.gameObject.GetComponent<MeshRenderer>();
            this.pseudoHandRenderer.enabled = false;
        }

        public void TriggerTouch(bool isTouching, float roughnessLevel = 0f)
        {
            this.isTouching = isTouching;
            if(isTouching)
            {
                this.pseudoHandModel.position = this.transform.position;
                this.pseudoHandModel.rotation = this.transform.rotation;
                this.pseudoHandModel.localScale = Vector3.one;
                this.lastPos = this.transform.position;
                this.roughnessLevel = roughnessLevel;
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
            if(!this.isTouching)
                return;
            this.pseudoHandModel.rotation = this.transform.rotation;
            RaycastHit hit;
            Vector3 currentPos = this.transform.position;
            int layerMask = 1 << 3;
            layerMask = ~layerMask;
            if(Physics.Raycast(currentPos, Vector3.forward, out hit, 5.0f, layerMask))
            {
                float velocity = Mathf.Min(((this.transform.position - this.lastPos) / Time.deltaTime).magnitude, 0.3f);
                float x = currentPos.x + Perturber.perturber(this.roughnessLevel, velocity);
                float y = currentPos.y + Perturber.perturber(this.roughnessLevel, velocity);
                float z = hit.point.z + this.pseudoHandModel.position.z - this.fingerTip.position.z;
                this.pseudoHandModel.position = new Vector3(x, y, z);
            }
            this.lastPos = this.transform.position;
        }
    }
}
