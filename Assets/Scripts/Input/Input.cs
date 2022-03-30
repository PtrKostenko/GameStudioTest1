using UnityEngine;

namespace GameStudioTest1
{
    public abstract class Input
    {
        /// <summary>
        /// Returns Move Vector2
        /// </summary>
        /// <returns>
        /// Vector2, where x - horizontal, y - vertical axis
        /// </returns>
        public abstract Vector2 GetMoveAxis();
        public abstract bool IsChangeUnitInput();
        
    }
}