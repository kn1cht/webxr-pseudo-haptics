using UnityEngine;


namespace WebXRPseudo.FineRoughness {
    public class PseudoZone : MonoBehaviour
    {
        private PseudoCursor cursor;
        public float roughnessLevel = 0f;

        void Start()
        {
            this.cursor = GameObject.Find("PseudoCursor").GetComponent<PseudoCursor>();
        }

        void OnMouseOver()
        {
            this.cursor.TriggerTouch(true, this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Hand")
                return;
            other.gameObject.GetComponent<PseudoHand>().TriggerTouch(true, roughnessLevel);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != "Hand")
                return;
            other.gameObject.GetComponent<PseudoHand>().TriggerTouch(false);
        }

        void Update()
        {
        }
    }
}
