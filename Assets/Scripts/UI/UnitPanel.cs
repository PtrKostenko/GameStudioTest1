using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameStudioTest1.UI
{
    public class UnitPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _unitNameTMP;
        [SerializeField] private ProgressBar _healthBar;
        [SerializeField] private Button _nextUnitButton;
        private Unit _unit;
        public UnityEvent NextUnitClicked => _nextUnitButton.onClick;
        private void OnValidate()
        {
            Debug.Assert(_unitNameTMP != null, $"{nameof(_unitNameTMP)} need to be assigned", this);
            Debug.Assert(_healthBar != null, $"{nameof(_healthBar)} need to be assigned", this);
            Debug.Assert(_nextUnitButton != null, $"{nameof(_nextUnitButton)} need to be assigned", this);
        }

        public void SetUnit(Unit unit)
        {
            if (_unit != null)
            {
                _unit.Health.ValueChanged.RemoveListener(UpdateHealthBar);
            }
            _unit = unit;
            UpdateHealthBar(_unit.Health.CurrentValue);
            _unit.Health.ValueChanged.AddListener(UpdateHealthBar);
            _unitNameTMP.text = _unit.UnitName;
        }

        private void UpdateHealthBar(int value)
        {
            _healthBar.SetProgress01((float)value / (float)_unit.Health.MaxValue);
        }
    }
}