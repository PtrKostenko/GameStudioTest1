using UnityEngine;
using UnityEngine.Events;

namespace GameStudioTest1
{
    [RequireComponent(typeof(GuidComponent))]
    public class Health : MonoBehaviour, IMemorizable
    {
        [SerializeField, Min(0)] private int _maxValue = 100;
        [SerializeField, Min(0)] private int _currentValue = 100;



        public IntEvent ValueChanged = new IntEvent();
        public IntEvent ValueFilled = new IntEvent();
        public UnityEvent ValueExpired;

        public int CurrentValue => _currentValue;
        public int MaxValue { get => _maxValue; set { _maxValue = value; } }
        public string ID => GetComponent<GuidComponent>().GetGuid().ToString();

        public void Set(int value)
        {
            var oldValue = _currentValue;
            var newValue = Mathf.Clamp(value, 0, _maxValue);
            _currentValue = newValue;
            if (newValue != oldValue)
                ValueChanged?.Invoke(newValue);
            if (newValue > oldValue && newValue == _maxValue)
                ValueFilled?.Invoke(newValue);
            else if (newValue < oldValue && newValue == 0)
                ValueExpired?.Invoke();
        }
        public void ChangeBy(int delta)
        {
            var newValue = _currentValue + delta;
            Set(newValue);
        }


        public Memento MakeMemento()
        {
            var mem = new Memento(ID, this.GetType().ToString());
            mem.AddKeyValue(nameof(_maxValue), _maxValue);
            mem.AddKeyValue(nameof(_currentValue), _currentValue);
            return mem;
        }

        public void SetFromMemento(Memento memento)
        {
            _maxValue = System.Convert.ToInt32(memento.TryGetValue(nameof(_maxValue)));
            _currentValue = System.Convert.ToInt32(memento.TryGetValue(nameof(_currentValue)));
        }
    }
}