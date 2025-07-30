using UnityEngine;
using UnityEngine.InputSystem; // New Input System

public class CameraDragMobile : MonoBehaviour
{
    public float dragSpeed = 0.1f;
    public float minX = -50f, maxX = 50f;
    public float minZ = -50f, maxZ = 50f;

    private Vector2 lastPointerPos;
    private bool isDragging = false;

    void Update()
    {
#if UNITY_EDITOR
        HandleEditorDrag();
#else
        HandleTouchDrag();
#endif
    }

    private void HandleTouchDrag()
    {
        if (Touchscreen.current == null)
            return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.isPressed)
        {
            Vector2 currentPos = touch.position.ReadValue();

            if (!isDragging)
            {
                lastPointerPos = currentPos;
                isDragging = true;
            }
            else
            {
                Vector2 delta = currentPos - lastPointerPos;
                lastPointerPos = currentPos;

                MoveCamera(delta);
            }
        }
        else
        {
            isDragging = false;
        }
    }

    private void HandleEditorDrag()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.isPressed)
        {
            isDragging = false;
            return;
        }

        Vector2 currentPos = Mouse.current.position.ReadValue();

        if (!isDragging)
        {
            lastPointerPos = currentPos;
            isDragging = true;
        }
        else
        {
            Vector2 delta = currentPos - lastPointerPos;
            lastPointerPos = currentPos;

            MoveCamera(delta);
        }
    }

    private void MoveCamera(Vector2 delta)
    {
        Vector3 move = new Vector3(-delta.x * dragSpeed, 0, -delta.y * dragSpeed);
        Vector3 newPos = transform.position + move;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.z = Mathf.Clamp(newPos.z, minZ, maxZ);

        transform.position = newPos;
    }
}
