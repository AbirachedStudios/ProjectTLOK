using UnityEngine;

namespace UI.Scripts.MainMenu
{
    public class UI_TitleScreen : MonoBehaviour
    {
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        [SerializeField] private Animator m_titleAnimator;
        [SerializeField] private GameObject m_nextScene;

        private void Update()
        {
            if (Input.anyKeyDown) m_titleAnimator.SetTrigger(FadeIn);
        }

        public void SetActiveNextScene()
        {
            m_nextScene.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}