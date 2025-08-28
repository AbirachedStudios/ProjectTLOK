using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MouseSettings
{
    Texture2D[] _mouseTexture;
    private Dictionary<CursorType, Texture2D> cursors = new Dictionary<CursorType, Texture2D>();


    private void SetCursorTexture(CursorType type, Texture2D texture)
    {
        cursors[type] = texture;
    }
    public MouseSettings(Texture2D[] mouseTexture)
    {
        _mouseTexture = mouseTexture;

        foreach (var texture in _mouseTexture)
        {
            if (texture != null)
            {
                CursorType cursorType = (CursorType)System.Array.IndexOf(_mouseTexture, texture);
                cursors[cursorType] = texture;
            }
        }
        
        CursorLock(CursorType.Basic);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void CursorUnlock(CursorType type)
    {
    
        ChangeCursor(type);
        Cursor.lockState = CursorLockMode.None;
    }
    public void CursorLock(CursorType type)
    {
        SetCursorTexture(type, _mouseTexture[0]);
        ChangeCursor(type);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ChangeCursor(CursorType type)
    {
        if (cursors.TryGetValue(type, out Texture2D texture))
        {
            Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
