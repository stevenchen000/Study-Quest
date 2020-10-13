using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    
    public abstract void ActivateUI();
    public abstract void DeactivateUI();
    public abstract bool IsActive();
    
}
