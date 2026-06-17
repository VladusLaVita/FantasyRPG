using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "RPGStats", menuName = "Scriptable Objects/RPGStats")]
public class RPGStats : ScriptableObject
{
    public string Name;
    public Texture sprite;
    public float ATK = 1.0f;
    public float DEF = 0.0f;
    public float MGK = 1.0f;
    public float RNG = 1.0f;
    
    
    public int LVL = 1;
    public float HP = 100;
    public float currentHP = 100;
}
