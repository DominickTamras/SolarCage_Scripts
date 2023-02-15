using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Log", menuName = "Terminal Log")]
public class TerminalLog : ScriptableObject
{
    public string title;
    [TextArea(15,20)]
    public string log;
}
