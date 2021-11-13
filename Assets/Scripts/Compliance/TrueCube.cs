using UnityEngine;


namespace WebXRPseudo.Compliance {
    public class TrueCube : MonoBehaviour
    {
        public PseudoCube pseudoCube;
        public PseudoCursor pseudoCursor;

        void Start()
        {
        }

        void OnMouseDown()
        {
            pseudoCube.GrabStart(true);
            pseudoCursor.TriggerTouch(true, pseudoCube);
        }

        void OnMouseUp()
        {
            pseudoCube.GrabEnd();
            pseudoCursor.TriggerTouch(false);
        }

        void Update()
        {
        }
    }
}
