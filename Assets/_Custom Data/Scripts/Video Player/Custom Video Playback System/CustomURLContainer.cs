using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CustomURLContainer : ScriptableObject
{
    public List<CustomURL> CustomURLs = new List<CustomURL>();
}
