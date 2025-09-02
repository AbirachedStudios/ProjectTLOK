using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchivementController : MonoBehaviour
{
    [SerializeField] Archivement archivementList;

    [SerializeField] TMP_InputField input;
    public void SentPressed()
    {
        GetArchivement(input.text);
    }

    public void GetArchivement(string id)
    {
        foreach (var item in archivementList.archivements)
        {
            if (item.id == id)
            {
                PlayerPrefs.SetInt("achv" + id, 1);
            }
        }
    }

    public void PrintArchivement()
    {
        foreach (var item in archivementList.archivements)
        {
            if (PlayerPrefs.GetInt("achv" + item.id) == 1)
            {
                print("Title " + item.title + "\nId " + item.id);
            }
        }
    }

    public void ClearArchivement()
    {
        foreach (var item in archivementList.archivements)
        {
            PlayerPrefs.SetInt("achv" + item.id, 0);
        }
    }
}
