using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    public IndividualShaderVariables IndividualShaderVariables;

}

[System.Serializable]
public class IndividualShaderVariables
{
    public bool _front;
}