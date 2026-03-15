using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

public abstract class AbstractController
{
    private bool isRun = true;
    private bool isInit;
    private bool hasCompletedFirstRun;
    private bool isBeforeRunStart;
    private bool isAfterRunStart;

    public void GameUpdate()
    {
        if (!isInit)
        {
            isInit = true;
            OnInit();
        }

        if (!isRun)
        {
            return;
        }

        OnBeforeRunUpdate();

        if (hasCompletedFirstRun)
        {
            OnAfterRunUpdate();
        }
        else
        {
            hasCompletedFirstRun = true;
        }

        AlwaysUpdate();
    }

    protected virtual void OnInit() { }

    protected virtual void OnBeforeRunStart() { }

    protected virtual void OnBeforeRunUpdate()
    {
        if (!isBeforeRunStart)
        {
            isBeforeRunStart = true;
            OnBeforeRunStart();
        }
    }

    protected virtual void OnAfterRunStart() { }

    protected virtual void OnAfterRunUpdate()
    {
        if (!isAfterRunStart)
        {
            isAfterRunStart = true;
            OnAfterRunStart();
        }
    }

    protected virtual void AlwaysUpdate() { }

    public void TurnOnController()
    {
        isRun = true;
    }

    public void TurnOffController()
    {
        isRun = false;
    }
}
