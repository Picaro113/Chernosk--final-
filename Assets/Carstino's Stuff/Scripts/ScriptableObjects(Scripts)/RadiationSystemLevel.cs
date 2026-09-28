using UnityEngine;

[CreateAssetMenu(fileName = "RadLevel", menuName = "RadiationSystemLevel/RadSystem")]
public class RadiationSystemLevel : ScriptableObject
{
    public int CurrentRadLevel;

    //i don't think i'll be using enums for this
    public enum CurrentRadState
    {
        RadLevelOne, 
        RadLevelTwo, 
        RadLevelThree,
    }

    public CurrentRadState RadStatus;
}
