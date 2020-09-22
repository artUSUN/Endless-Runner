using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private float inputIgnoreSize = 10f;
    private bool isNeedRecordVector = false;
    private Vector2 input;
    private bool isActive;

    public void OnBeginDrag(PointerEventData eventData)
    {
        input = Vector2.zero;
        isNeedRecordVector = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isNeedRecordVector && isActive)
        {
            input += eventData.delta.normalized;
            if (Mathf.Abs(input.x) > inputIgnoreSize || Mathf.Abs(input.y) > inputIgnoreSize)
            {
                ConvertToEvent();
                isNeedRecordVector = false;
                Debug.Log("Input off");
            }
        }
    }

    private void ConvertToEvent()
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            if (input.x > 0) StateBus.Input_Horizontal += 1;
            else StateBus.Input_Horizontal += -1;
        }
        else
        {
            if (input.y > 0) StateBus.Input_Vertical += 1;
            else StateBus.Input_Vertical += -1;
        }
    }

    private void Start()
    {
        isActive = true;
    }

    private void Update()
    {
        if (StateBus.Input_Disable) isActive = false;
        if (StateBus.Input_Enable) isActive = true;
    }
}
