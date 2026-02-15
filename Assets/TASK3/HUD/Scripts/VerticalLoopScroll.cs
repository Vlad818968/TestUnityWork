using UnityEngine;
using AxGrid.Base;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using AxGrid.Path;
using System;
using AxGrid.FSM;
using AxGrid;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class VerticalLoopScroll : MonoBehaviourExt
{
    [SerializeField] private float _acelerationTime;
    [SerializeField] private float _scrollSpeed;

    private float _position;
    private float _cellSize;
    private RectTransform _content;
    private List<RectTransform> _items;

    [OnAwake]
    public void Init()
    {
        _content = GetComponent<RectTransform>();
        _items = GetComponentsInChildren<RectTransform>()
            .Where(c => c != _content)
            .ToList();

        var layoutGroup = GetComponent<GridLayoutGroup>();
        _cellSize = layoutGroup.cellSize.y + layoutGroup.padding.top;
        MoveToFirst(_items.Last());
        _content.AnchoredPositionSetY(_cellSize);
    }

    [OnStart]
    public void AddActions()
    {
        Model.EventManager.AddAction("OnIsScrollingChanged", IsScrolling);
    }

    private void IsScrolling()
    {
        if (Model.GetBool("IsScrolling"))
        {
            StartScroll();
        }
        else
        {
            StopScroll();
        }
    }

    private void StartScroll()
    {
        Path = new CPath()
            .EasingLinear(_acelerationTime, 0f, _scrollSpeed, (f) =>
            {
                SetPosition(_position + Time.deltaTime * f);
            })
            .Add(() =>
            {
                SetPosition(_position + Time.deltaTime * _scrollSpeed);
                return Status.Continue;
            });
    }

    private void StopScroll()
    {
        var stopPosition = (float)Math.Ceiling(_position);
        Path = new CPath()
            .Add(() =>
            {
                SetPosition(Mathf.MoveTowards(_position, stopPosition, Time.deltaTime * _scrollSpeed));
                if (_position == stopPosition)
                {
                    Settings.Fsm?.Invoke("OnScrollEnded");
                    return Status.OK;
                }

                return Status.Continue;
            });
    }

    private void SetPosition(float newPosition)
    {
        var steps = (int)newPosition - (int)_position;
        for (int i = 0; i < steps; i++)
        {
            MoveToFirst(_items.Last());
        }

        _content.AnchoredPositionSetY(Mathf.Lerp(_cellSize, 0, newPosition % 1f));
        _position = newPosition;
    }

    private void MoveToFirst(params RectTransform[] transforms)
    {
        foreach (var t in transforms)
        {
            if (!_items.Contains(t))
            {
                continue;
            }

            t.SetAsFirstSibling();
            _items.Remove(t);
            _items.Insert(0, t);
        }
    }
}
