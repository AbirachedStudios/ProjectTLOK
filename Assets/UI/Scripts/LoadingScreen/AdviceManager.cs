using TMPro;
using UnityEngine;

namespace UI.Scripts.LoadingScreen
{
    public class AdviceManager : MonoBehaviour
    {
        [SerializeField] private string[] m_adviceList;
        [SerializeField] private TMP_Text m_adviceText;

        private void Start()
        {
            m_adviceText.text = GetRandomAdvice();
        }

        private string GetRandomAdvice()
        {
            var randomIndex = Random.Range(0, m_adviceList.Length);
            return m_adviceList[randomIndex];
        }
    }
}
