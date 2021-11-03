using UnityEngine;


namespace WebXRPseudo.Friction {
    public class PseudoZone : MonoBehaviour
    {
        public float magnification = 0.2f;
        public PseudoCursor cursor;

        void Start()
        {
            cursor = GameObject.Find("PseudoCursor").GetComponent<PseudoCursor>();
        }

        void OnMouseOver()
        {
            cursor.TriggerTouch(true, this);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Hand")
                return;
            other.gameObject.GetComponent<PseudoHand>().TriggerTouch(true, this.magnification);
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
