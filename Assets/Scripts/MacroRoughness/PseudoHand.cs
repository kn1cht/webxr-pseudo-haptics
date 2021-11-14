using UnityEngine;

namespace WebXRPseudo.MacroRoughness {
    public class PseudoHand : MonoBehaviour
    {
        public Transform fingerTip;
        public Transform pseudoHandModel;
        private SkinnedMeshRenderer trueHandRenderer;
        private bool isTouching;
        private float curveZ;
        private float maxdepth;
        private MeshRenderer pseudoHandRenderer;


        void Start()
        {
            this.trueHandRenderer = this.GetComponent<SkinnedMeshRenderer>();
            this.trueHandRenderer.enabled = true;
            this.pseudoHandRenderer = this.pseudoHandModel.gameObject.GetComponent<MeshRenderer>();
            this.pseudoHandRenderer.enabled = false;
        }

        public void TriggerTouch(bool isTouching, float curveZ = 0f, float maxdepth = 1f)
        {
            this.isTouching = isTouching;
            if(isTouching)
            {
                this.curveZ = curveZ;
                this.maxdepth = maxdepth;
                this.pseudoHandModel.position = this.transform.position;
                this.pseudoHandModel.rotation = this.transform.rotation;
                this.pseudoHandModel.localScale = Vector3.one;
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
            int layerMask = 1 << 2 | 1 << 3;
            layerMask = ~layerMask;
            if(Physics.Raycast(currentPos, Vector3.forward, out hit, 5.0f, layerMask))
            {
                float z = hit.point.z + this.pseudoHandModel.position.z - this.fingerTip.position.z;
                this.pseudoHandModel.position = new Vector3(currentPos.x, currentPos.y, z);
                float depthRate = (hit.point.z - this.curveZ) / this.maxdepth;
                float scale = 1 - Mathf.Sin(depthRate * Mathf.PI / 2f) / 4f;
                this.pseudoHandModel.localScale = Vector3.one * scale;
            }
        }
    }
}
