using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    public int CurHealth, MaxHealth;
    public Vector2 targetPos;

    #region Late Update
    void LateUpdate()
    {
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        if(CurHealth < 0)
        {
            CurHealth = 0;
        }
        if(CurHealth > MaxHealth)
        {
            CurHealth = MaxHealth;
        }
    }
    #endregion
    #region GUI HealthBar
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        // Displays the max health
        GUI.Box(new Rect(targetPos.x + -0.5f * scrW, -targetPos.y + scrH * 8.25f, scrW, scrH * 0.25f), "");
        // Displays the current health
        GUI.Box(new Rect(targetPos.x + -0.5f * scrW, -targetPos.y + scrH * 8.25f, CurHealth * scrW/ MaxHealth, scrH * 0.25f), "");
    }
    #endregion
}
