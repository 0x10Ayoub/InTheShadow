using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShadowPiece",menuName = "SO/shadowPiece")]
public class ShadowPiece : ScriptableObject
{
    public Transform [] gameObject;
    public Transform [] correctTransform;
    public bool canRotateHorizontal;
    public bool canRotateVertical;
    public bool canMove;
}
