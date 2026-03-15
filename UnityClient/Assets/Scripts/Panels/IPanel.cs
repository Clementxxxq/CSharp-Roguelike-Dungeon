using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public abstract class IPanel
{
    public GameObject gameObject { get; protected set; }
    public Transform transform => gameObject != null ? gameObject.transform : null;

    public RectTransform rectTransform { get; protected set; }
    protected IPanel parent;
    protected List<IPanel> children;
    private bool isInit;
    private bool isEnter;
    private bool isSuspend;
    protected bool isShowAfterExit;
    
    public IPanel(IPanel panel)
    {
        parent = panel;
        children = new List<IPanel>();
    }
    
    public void GameUpdate()
    {
        if (!isInit)
        {
            OnInit();
            isInit = true;
        }
        foreach (IPanel panel in children)
        {
            panel.GameUpdate();
        }
        if (!isSuspend)
        {
            OnUpdate();
        }
    }
    
    protected virtual void OnInit() 
    {
        Suspend();
        if (gameObject == null) 
        { 
            gameObject = GameObject.Find(GetPanelObjectName());
        }
        
        if (gameObject != null)
        {
            rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = gameObject.AddComponent<RectTransform>();
            }
        }
    }

    protected virtual string GetPanelObjectName()
    {
        return GetType().Name;
    }
    
    protected virtual void OnEnter() { }
    
    protected virtual void OnUpdate()
    {
        if (!isEnter)
        {
            isEnter = true;
            OnEnter();
        }
    }
    
    public virtual void OnExit() 
    {
        if (!isShowAfterExit) 
        {
            gameObject?.SetActive(false);
        }
        isEnter = false;
        Suspend();
        

        if (parent != null)
        {
            parent.isEnter = false;
            parent.Resume();
        }
    }

    public void EnterPanel<T>() where T : IPanel
    {
        IPanel panel = GetPanel<T>();
        if (panel != null)
        {
            panel.Resume();
            Suspend();
        }
    }

    public T GetPanel<T>() where T : IPanel
    {

        return children.FirstOrDefault(x => x is T) as T;
    }
    
    public void Suspend()
    {
        isSuspend = true;
    }
    
    public void Resume()
    {
        isSuspend = false;
    }
}
