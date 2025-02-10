using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemViewDraggingModule : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action DraggingStarted;
    public event Action DraggingFailed;

    [SerializeField]
    private ItemView itemView;
    [SerializeField]
    private CanvasGroup viewCanvasGroup;
    [SerializeField, Range(0f, 1f)]
    private float draggingItemViewAlpha;
 
    private Canvas gameUIRoot;
    private Transform startParent;
   
    public static ItemView DraggingItem { get; private set; }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startParent = itemView.transform.parent;
        DraggingItem = itemView;

        transform.SetParent(gameUIRoot.transform);
        transform.SetAsLastSibling();
        ToggleCanvasGroupState(true);
        DraggingStarted?.Invoke();
    }

    public void OnDrag(PointerEventData eventData) => transform.position = Input.mousePosition;

    public void OnEndDrag(PointerEventData eventData)
    {
        ToggleCanvasGroupState(false);
        if (transform.parent == gameUIRoot.transform)
        {
            transform.SetParent(startParent);
            transform.localPosition = Vector3.zero;
            DraggingFailed?.Invoke();
        }       
        DraggingItem = null;
    }

    public void ClearDraggingEventsSubscriptions() => DraggingStarted = DraggingFailed = null;

    private void ToggleCanvasGroupState(bool isDraggingStarted)
    {
        viewCanvasGroup.alpha = isDraggingStarted ? draggingItemViewAlpha : 1f;
        viewCanvasGroup.blocksRaycasts = !isDraggingStarted;
    }

    private void OnEnable()
    {
        gameUIRoot = GetComponentInParent<Canvas>();
    }

    private void OnDestroy()
    {
        ClearDraggingEventsSubscriptions();
    }
}
