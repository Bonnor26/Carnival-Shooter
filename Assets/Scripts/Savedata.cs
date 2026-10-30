using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetData
{
    public Vector3 position;
    public float speed;
    public float size;
    public int points;
    public float direction;
    public Color color;
}

[Serializable]
public class TransformSaveData
{
    public Vector3 playerPosition;
    public List<TargetData> targets = new List<TargetData>();
}
