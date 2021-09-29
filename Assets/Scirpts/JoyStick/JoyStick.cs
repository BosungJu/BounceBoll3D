using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    public RectTransform Stick;

    public bool nowDraging;

    public Action<bool> dragEvent = null;

    private float maxValue;
    public Vector3 direction { get; private set; }
    public Vector3 angle { get; private set; }
    public float value { get; private set; }

    private void Awake()
    {
        rectTransform = (RectTransform)transform;
        maxValue = rectTransform.sizeDelta.x / 4;
        nowDraging = false;

        StartCoroutine(DragFunc());
    }

    private IEnumerator DragFunc()
    {
        while (true)
        {
            if (dragEvent != null) dragEvent(nowDraging);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTouch(Vector2 touchPos)
    {
        Vector2 vec = new Vector2(touchPos.x - rectTransform.position.x, touchPos.y - rectTransform.position.y);

        vec = Vector2.ClampMagnitude(vec, maxValue);
        Stick.localPosition = vec;

        float fSqr = (rectTransform.position - Stick.position).sqrMagnitude / maxValue;

        direction = new Vector3(vec.x * Time.deltaTime * fSqr, 0f, vec.y * Time.deltaTime * fSqr).normalized;
        angle = new Vector3(0f, Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg, 0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        nowDraging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        nowDraging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Stick.localPosition = Vector2.zero;
        nowDraging = false;
        
    }
}
