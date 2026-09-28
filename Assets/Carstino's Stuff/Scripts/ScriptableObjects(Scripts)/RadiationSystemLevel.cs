using UnityEngine;

[CreateAssetMenu(fileName = "RadLevel", menuName = "RadiationSystemLevel/RadSystem")]
public class RadiationSystemLevel : ScriptableObject
{
    public int CurrentRadLevel;

    public enum CurrentRadState
    {
        RadLevelOne, 
        RadLevelTwo, 
        RadLevelThree,
    }

    public CurrentRadState RadStatus;
}
