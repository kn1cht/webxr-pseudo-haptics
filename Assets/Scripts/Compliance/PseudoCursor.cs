using UnityEngine;

namespace Weight.Compliance {
    public class PseudoCursor : MonoBehaviour
    {
        public Texture2D handCursor;


        void Start()
        {
            Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.ForceSoftware);
        }

        void Update()
        {
        }
    }
}
