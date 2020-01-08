using UnityEngine;
using System.Collections;

public class GuiManager : Singleton<GuiManager>
{
    public HealthBarUI HealthBarUIPrefab;
    public Transform HealthBarParent;

    public static HealthBarUI CreateHealthBarUI()
    {
        HealthBarUI newUI = Instantiate(Instance.HealthBarUIPrefab, Instance.HealthBarParent);
        return newUI;
    }
}
