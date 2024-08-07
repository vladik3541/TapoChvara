using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // Подія для кліків
    public delegate void ClickAction(Vector3 position);
    public static event ClickAction OnClick;

    void Update()
    {
        if (IsPointerOverUIElement() || !GameStateManager.instance.isGame) return;
        // Перевірка лівої кнопки миші
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            if (OnClick != null)
            {
                OnClick(clickPosition);
            }
        }

        // Перевірка торкань сенсорного екрану
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = touch.position;
                    if (OnClick != null)
                    {
                        OnClick(touchPosition);
                    }
                }
            }
        }
    }
    private bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
