using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class AbstractFacade
{
    private bool isInit;
    public void GameUpdate()
    {
       OnUpdate();
    }
    protected virtual void OnInit() { }
    protected virtual void OnUpdate() 
    {
            if (!isInit)
            {
                isInit = true;
                OnInit();
        }
    }
}
