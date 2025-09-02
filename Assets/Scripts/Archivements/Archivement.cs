using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Archivement List", menuName = "Archivement")]
public class Archivement : ScriptableObject
{
    [SerializeField]
    public List<ArchivementItem> archivements;

    [System.Serializable]
    public class ArchivementItem
    {
        public string id;
        public Sprite cover;
        public string title;
        public string description;
    }
}
