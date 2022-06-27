using UnityEngine;

using RPG.UI;

namespace RPG.Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController player, RaycastHit hit);
    }
}
