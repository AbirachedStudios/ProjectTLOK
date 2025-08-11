using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Scripts.MainMenu
{
    public class UI_ChapterImagePreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image m_previewImage;
        [SerializeField] private Sprite m_chapterImage;
    
        //Variable hecha únicamente para testear la funcionalidad del script
        //Va a ser eliminada luego de que estén las imágenes de referencia de los capítulos
        [SerializeField] private Color m_chapterColor;

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_previewImage.color = m_chapterColor;
        
            if(m_chapterImage != null)
            {
                m_previewImage.sprite = m_chapterImage;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_previewImage.sprite = null;
            m_previewImage.color = Color.clear;
        }
    }
}