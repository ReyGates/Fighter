using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : Singleton<InGamePanel>
{
    public void ChangeShield()
    {
        if (Player.Instance != null)
            Player.Instance.SwitchShield();
    }
}
