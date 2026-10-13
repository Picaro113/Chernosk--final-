using UnityEngine;

[CreateAssetMenu(fileName = "Factions", menuName = "Factions/Factions")]
public class FactionNames : ScriptableObject
{
    public bool neutral = false;
    public enum Faction
    {


        Dementors, 
        Forgotten, 
        CCC, 
        Wanderers, 
        HiveMind

    }

    public Faction myfaction;

    public bool GetFaction(Faction faction) 
    {
        switch (faction)
        {
            case Faction.Dementors:
                if (faction == myfaction)
                    return true & neutral;
                if (faction != myfaction)
                {
                    if (myfaction == Faction.Forgotten) return neutral = true;
                    else return neutral;
                }
                break;
            case Faction.Forgotten:
                if (faction == myfaction)
                    return true & neutral;
                if (faction != myfaction)
                {
                    if (myfaction == Faction.Dementors) return neutral = true;
                    else return neutral;
                }
                break;
            case Faction.CCC:
                if (faction == myfaction)
                    return true;
                break;
            case Faction.Wanderers:
                if (faction == myfaction)
                    return true;
                break;
            case Faction.HiveMind:
                if (faction == myfaction)
                    return true;
                break;


        }
        return false;
    }
}
