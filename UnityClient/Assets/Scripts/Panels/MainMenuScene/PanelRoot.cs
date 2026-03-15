using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PanelRoot : IPanel
{
    public PanelRoot() : base(null)
    {

    }

    protected override string GetPanelObjectName()
    {
        return "Canvas";
    }

    protected override void OnInit()
    {
        base.OnInit();

        if (gameObject == null)
        {
            UnityEngine.Debug.LogWarning("PanelRoot GameObject was not found in scene.");
            return;
        }

        Button startButton = UnityTool.Instance.GetComponentFromChild<Button>(gameObject, "StartButton");
        if (startButton == null)
        {
            UnityEngine.Debug.LogWarning("StartButton was not found under PanelRoot.");
            return;
        }

        startButton.onClick.AddListener(() =>
        {
            OnBtnStartClick();
        });

        gameObject.SetActive(true);
        Resume();
    }

    protected override void OnEnter()
    {
        base.OnEnter();
    }

    private void OnBtnStartClick()
    {
        UnityEngine.Debug.Log("StartButton Clicked");
    }
}