using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotatable
{
    void SetPosition(int position);
    int GetRotation();
    void SetRotationListener(IRotationListener listener);
}
