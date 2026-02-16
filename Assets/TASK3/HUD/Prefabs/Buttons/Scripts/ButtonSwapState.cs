using AxGrid.Base;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class ButtonSwapState : MonoBehaviourExt, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private string _enableField;

    [Space]
    [SerializeField] private string _normal;
    [SerializeField] private string _highlighted;
    [SerializeField] private string _pressed;
    [SerializeField] private string _selected;
    [SerializeField] private string _disabled;

    private Animator _animator;

    private bool _isPointerEnter;
    private bool _isPointerDown;

    [OnAwake]
    private void Init()
    {
        _animator = GetComponent<Animator>();
    }

    [OnStart]
    private void AddActions()
    {
        Model.EventManager.AddAction($"On{_enableField}Changed", EnableFieldChandeHandler);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPointerDown = true;
        SetTrigger(_pressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPointerDown = false;
        SetTrigger(_normal);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerEnter = true;
        var trigger = _isPointerDown ? _pressed : _highlighted;
        SetTrigger(trigger);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerEnter = false;
        SetTrigger(_normal);
    }

    private void EnableFieldChandeHandler()
    {
        var trigger = _isPointerEnter ? _highlighted : _normal;
        SetTrigger(trigger);
    }

    private void SetTrigger(string trigger)
    {
        if (!IsInteractable())
        {
            _animator.SetTrigger(_disabled);
            return;
        }

        _animator.SetTrigger(trigger);
    }

    private bool IsInteractable()
    {
        return Model.GetBool(_enableField);
    }
}
