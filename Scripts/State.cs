using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    [SerializeField] string topText;
    [SerializeField] string bottomText;

    public string GetStateTextTop()
    {
        return topText;
    }
    public string GetStateBottomTop()
    {
        return bottomText;
    }
}
