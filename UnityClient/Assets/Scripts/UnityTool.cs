using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UnityEngine;

public class UnityTool
{
    private static UnityTool instance;
    public static UnityTool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UnityTool();
            }
            return instance;
        }
    }

    public T GetComponentFromChild<T>(GameObject obj, string name) where T : Component
    {
        if (obj == null)
        {
            return null;
        }

        foreach (Transform t in obj.GetComponentsInChildren<Transform>(true))
        {
            if (t.name == name)
            {
                T component = t.GetComponent<T>();
                if (component != null)
                {
                    return component;
                }
            }
        }

        return null;
    }

}
