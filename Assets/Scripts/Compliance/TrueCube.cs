using UnityEngine;


namespace WebXRPseudo.Compliance {
    public class TrueCube : MonoBehaviour
    {
        public PseudoCube pseudoCube;

        void Start()
        {
        }

        void OnMouseDown()
        {
            pseudoCube.GrabStart(true);
            Cursor.visible = false;
        }

        void OnMouseUp()
        {
            pseudoCube.GrabEnd();
            Cursor.visible = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != "Hand")
                return;
            pseudoCube.GrabStart();
            other.gameObject.GetComponent<PseudoHand>().TriggerTouch(true, pseudoCube.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag != "Hand")
                return;
            pseudoCube.GrabEnd();
            other.gameObject.GetComponent<PseudoHand>().TriggerTouch(false);
        }

        void Update()
        {
        }
    }
}
