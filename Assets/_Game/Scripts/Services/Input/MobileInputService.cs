using UnityEngine;

namespace _Game.Scripts.Services.Input
{
    class MobileInputService : IInputService
    {
        public Vector2 Axis
        {
            get
            {
                return new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
            }
        }

        public bool IsCrouching { get; set; }
    }
}