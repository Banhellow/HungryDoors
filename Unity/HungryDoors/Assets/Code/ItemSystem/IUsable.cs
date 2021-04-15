using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    Item Use();
    bool isInUsage { get; set; }
}
