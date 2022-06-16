using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] CursorTypeClass[] cursorTypes;
        Dictionary<CursorType, Texture2D> cursors;

        public void SetCursor(CursorType type)
        {
            BuildCursors();

            Texture2D cursorTexture = cursors[type];

            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        void BuildCursors()
        {
            if (cursorTypes == null) return;

            cursors = new Dictionary<CursorType, Texture2D>();

            foreach(CursorTypeClass cursorType in cursorTypes)
            {
                cursors.Add(cursorType.type, cursorType.texture);
            }
        }

        [System.Serializable]
        class CursorTypeClass
        {
            public CursorType type = CursorType.Unclickable;
            public Texture2D texture;
        }
    }
}
