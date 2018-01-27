using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        // ReSharper disable once NotAccessedField.Local
        private float _verticalScrollArea = 10f;

        [SerializeField]
        // ReSharper disable once NotAccessedField.Local
        private float _horizontalScrollArea = 10f;

        [SerializeField]
        private float _verticalScrollSpeed = 10f;

        [SerializeField]
        private float _horizontalScrollSpeed = 10f;

        [SerializeField]
        private float _zoomSensibility = 2f;

        [SerializeField]
        private float _verticalSensibility = 2f;

        [SerializeField]
        private float _horizontalSensibility = 2f;

        [SerializeField]
        private bool _zoomEnabled = true;

        [SerializeField]
        private bool _moveEnabled = true;

        [SerializeField]
        private bool _combinedMovement = true;

        private void FixedUpdate()
        {
            float xMove = 0;
            float yMove = 0;
            float zMove = 0;

            //Move camera if mouse is at the edge of the screen
            if (_moveEnabled)
            {
                //Move camera if mouse is at the edge of the screen
#if !UNITY_EDITOR
                Vector2 mousePos = Input.mousePosition;
                if (mousePos.x < _horizontalScrollArea)
                {
                    xMove = -1;
                }
                else if (mousePos.x >= Screen.width - _horizontalScrollArea)
                {
                    xMove = 1;
                }
                else
                {
                    xMove = 0;
                }

                if (mousePos.y < _verticalScrollArea)
                {
                    zMove = -1;
                }
                else if (mousePos.y >= Screen.height - _verticalScrollArea)
                {
                    zMove = 1;
                }
                else
                {
                    zMove = 0;
                }
#endif

                //Move camera if wasd or arrow keys are pressed
                float xAxisValue = Input.GetAxis("Horizontal");
                float zAxisValue = Input.GetAxis("Vertical");

                if (xAxisValue != 0)
                {
                    if (_combinedMovement)
                    {
                        xMove += xAxisValue;
                    }
                    else
                    {
                        xMove = xAxisValue;
                    }
                }

                if (zAxisValue != 0)
                {
                    if (_combinedMovement)
                    {
                        zMove += zAxisValue;
                    }
                    else
                    {
                        zMove = zAxisValue;
                    }
                }
            }
            else
            {
                xMove = 0;
                yMove = 0;
            }

            // Zoom Camera in or out
            if (_zoomEnabled)
            {
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    yMove = 1;
                }
                else if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    yMove = -1;
                }
                else
                {
                    yMove = 0;
                }
            }
            else
            {
                zMove = 0;
            }

            if (transform.localPosition.y < 11 && yMove < 0)
            {
                yMove = 0;
            }
            else if (transform.localPosition.y > 100 && yMove > 0)
            {
                yMove = 0;
            }

            //move the object
            MoveMe(xMove * _horizontalSensibility, yMove * _zoomSensibility, zMove * _verticalSensibility);
        }

        private void MoveMe(float x, float y, float z)
        {
            Vector3 moveVector = new Vector3(x * _horizontalScrollSpeed, y * _verticalScrollSpeed, z * _horizontalScrollSpeed) * Time.deltaTime;
            transform.Translate(moveVector, Space.World);
        }
    }
}