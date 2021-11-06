using UnityEngine;


namespace WebXRPseudo.MacroRoughness {
    public class PseudoZone : MonoBehaviour
    {
        private PseudoCursor cursor;
        private Transform curve;
        private float maxdepth;

        void Start()
        {
            this.cursor = GameObject.Find("PseudoCursor").GetComponent<PseudoCursor>();
            this.curve = this.transform.Find("half_cylinder");
            this.maxdepth = Mathf.Abs(this.curve.Find("maxdepth").position.z - this.curve.position.z);
        }

        void OnMouseOver()
        {
            this.cursor.TriggerTouch(true, this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Hand")
                return;
            other.gameObject.GetComponent<PseudoHand>().TriggerTouch(true, this.curve.position.z, this.maxdepth);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != "PseudoHand")
                return;
            other.GetComponentInParent<PseudoHand>().TriggerTouch(false);
        }


        void Update()
        {
        }
    }
}
