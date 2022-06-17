using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class CursorManager : MonoBehaviour
    {
        [System.Serializable]
        struct RPGCursor
        {
            public CursorType type;
            public Texture2D texture;
            public Vector3 hotspot;
        }

        [SerializeField] RPGCursor[] cursors;

        public void SetCursor(CursorType type)
        {
            if (cursors.Length == 0) return;

            RPGCursor cursor = GetCursor(type);

            Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
        
        }

        RPGCursor GetCursor(CursorType type)
        {
            foreach (RPGCursor cursor in cursors)
            {
                if (cursor.type == type)
                {
                    return cursor;
                }
            }

            return cursors[0];
        }
    }
}
