using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Scripts.LoadingScreen
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private GameObject m_loadingScreen;
        [SerializeField] private TMP_Text m_progressText;

        public void LoadLevel(int m_levelIndex)
        {
            StartCoroutine(LoadAsynchronously(m_levelIndex));
        }

        private IEnumerator LoadAsynchronously(int m_sceneIndex)
        {
            var operation = SceneManager.LoadSceneAsync(m_sceneIndex);

            m_loadingScreen.SetActive(true);

            while ((operation != null && !operation.isDone) && m_progressText != null)
            {
                var progress = Mathf.Clamp01(operation.progress / 0.9f) * 100;
                m_progressText.text = $"{progress:F0}%";

                yield return null;
            }
        }
    }
}