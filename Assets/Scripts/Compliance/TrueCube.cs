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

        void Update()
        {
        }
    }
}
