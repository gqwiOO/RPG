using UnityEngine;

namespace Cinemachine.Examples
{
    public class PlayerMovement : MonoBehaviour
    {
        public float movementSpeed = 10f;
        public float lookatspeed = 5f;

        void Update()
        {
#if ENABLE_LEGACY_INPUT_MANAGER
            if (UnityEngine.Input.GetKey("w"))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            }
            else if (UnityEngine.Input.GetKey("s"))
            {
                transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            }

            if (UnityEngine.Input.GetKey("a") && !UnityEngine.Input.GetKey("d"))
            {
                transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
            }
            else if (UnityEngine.Input.GetKey("d") && !UnityEngine.Input.GetKey("a"))
            {
                transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
            }

            //mouse look at
            float horizontal = UnityEngine.Input.GetAxis("Mouse X") * lookatspeed;
            float vertical = UnityEngine.Input.GetAxis("Mouse Y") * lookatspeed;

            transform.Rotate(0f, horizontal, 0f, Space.World);

            //transform.Rotate(-vertical, 0f, 0f, Space.Self);
#else
        InputSystemHelper.EnableBackendsWarningMessage();
#endif
        }
    }
}