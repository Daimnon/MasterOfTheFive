using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Aspect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _onHoverOffsetHand = 160f, _onHoverOffsetBattleField = 80f;

    private GameObject _placeholder = null;

    public Transform ParentToReturn = null;
    public Transform ParentToReturnPlaceholder = null;
    
    public bool IsCardInHand = true, IsHoldingCard = false, IsOnBattlefield = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("Drag Began");

        IsHoldingCard = true;
        _placeholder = new GameObject($"{name}_Placeholder");
        _placeholder.transform.SetParent(transform.parent);
        //LayoutElement le = _placeholder.AddComponent<LayoutElement>();
        //le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        //le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        //le.flexibleWidth = 0;
        //le.flexibleHeight = 0;

        _placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        ParentToReturn = transform.parent;
        ParentToReturnPlaceholder = ParentToReturn;
        transform.SetParent(transform.parent.parent);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Dragging");

        transform.position = eventData.position;

        if (_placeholder.transform.parent != ParentToReturnPlaceholder)
            _placeholder.transform.SetParent(ParentToReturnPlaceholder);

        int newSiblingIndex = ParentToReturnPlaceholder.childCount;

        for (int i = 0; i < ParentToReturnPlaceholder.childCount; i++)
        {
            if (transform.position.x < ParentToReturnPlaceholder.GetChild(i).position.x)
            {
                newSiblingIndex = i;

                if (_placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }

        _placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("Stopped Dragging");

        transform.SetParent(ParentToReturn);
        transform.SetSiblingIndex(_placeholder.transform.GetSiblingIndex());
        _canvasGroup.blocksRaycasts = true;
        
        Destroy(_placeholder);
        IsHoldingCard = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Hovering");

        if (IsHoldingCard)
            return;

        // affects cards in battlefield
        else if (!IsCardInHand)
            transform.position = new Vector2(transform.position.x, transform.position.y + _onHoverOffsetBattleField);

        // affects cars in hand
        else
            transform.position = new Vector2(transform.position.x, transform.position.y + _onHoverOffsetHand);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Stopped Hovering");

        if (IsHoldingCard)
            return;

        // affects cards in battlefield
        else if (!IsCardInHand)
            transform.position = new Vector2(transform.position.x, transform.position.y - _onHoverOffsetBattleField);

        // affects cars in hand
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - _onHoverOffsetHand);
    }
}
