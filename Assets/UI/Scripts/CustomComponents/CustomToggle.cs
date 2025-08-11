using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Scripts.CustomComponents
{
    public class CustomToggle : MonoBehaviour
    {
        [SerializeField] private Toggle m_toggle;

        [SerializeField] private UnityEvent onToggleOn;
        [SerializeField] private UnityEvent onToggleOff;
    
    
        private void Start()
        {
            m_toggle.onValueChanged.AddListener(ToggleFunction);
            ToggleFunction(m_toggle.isOn);
        }

        private void ToggleFunction(bool toggleValue)
        {
            if (toggleValue)
            {
                onToggleOff.Invoke();
            }
            else
            {
                onToggleOn.Invoke();
            }
        }
    }
}
