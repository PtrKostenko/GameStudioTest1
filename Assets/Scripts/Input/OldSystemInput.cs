using System.Collections;
using UnityEngine;

namespace GameStudioTest1
{
    public class OldSystemInput : Input
    {
        public override Vector2 GetMoveAxis()
        {
            var horiz = UnityEngine.Input.GetAxis("Horizontal");
            var vert = UnityEngine.Input.GetAxis("Vertical");
            return new Vector2(horiz, vert);
        }

        public override bool IsChangeUnitInput()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Tab);
        }

        
    }
}