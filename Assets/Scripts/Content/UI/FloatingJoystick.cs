using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace Content.UI
{
    [AddComponentMenu("Input/Floating On-Screen Stick")]
    public class FloatingJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] private float _movementRange = 50;
        [SerializeField] private bool _alwaysShow;

        [InputControl(layout = "Vector2")] [SerializeField]
        private string _controlPath;

        [SerializeField] private RectTransform _raycastArea;
        [SerializeField] private RectTransform _containerTransform;
        [SerializeField] private RectTransform _joystickTransform;

        private Vector2 _pointerDownPos;
        private Vector2 _dragPos;

        public float MovementRange => _movementRange;


        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Start()
        {
            _containerTransform.gameObject.SetActive(_alwaysShow);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            if (!_alwaysShow)
            {
                TurnVisibility(true);
            }
        
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_raycastArea, eventData.position, eventData.pressEventCamera, out _pointerDownPos);
            _containerTransform.anchoredPosition = _pointerDownPos;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_raycastArea, eventData.position, eventData.pressEventCamera, out _dragPos);
            Vector2 delta = _dragPos - _pointerDownPos;

            delta = Vector2.ClampMagnitude(delta, MovementRange);
            _joystickTransform.anchoredPosition = delta;

            Vector2 newPos = new(delta.x / MovementRange, delta.y / MovementRange);
            SendValueToControl(newPos);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_alwaysShow)
            {
                TurnVisibility(false);
            }

            _pointerDownPos = Vector2.zero;
            _dragPos = Vector2.zero;
            _joystickTransform.anchoredPosition = Vector2.zero;
            SendValueToControl(Vector2.zero);
        }

        private void TurnVisibility(bool state)
        {
            _containerTransform.gameObject.SetActive(state);
        }
    }
}