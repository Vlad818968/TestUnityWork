using UnityEngine;
using AxGrid.Base;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(RectTransform))]
public class VerticalLoopScroll : MonoBehaviourExt
{
    public float Position
    {
        get => _position;
        set => SetPosition(value);
    }

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
