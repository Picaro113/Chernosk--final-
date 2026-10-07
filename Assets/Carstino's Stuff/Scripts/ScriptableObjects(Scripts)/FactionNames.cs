using UnityEngine;

[CreateAssetMenu(fileName = "Factions", menuName = "Factions/Factions")]
public class FactionNames : ScriptableObject
{
    public bool neutral;
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
                    return false;
                if (faction != myfaction)
                {
                    if (faction == Faction.Forgotten) return neutral == true;
                    if (faction != Faction.Forgotten) return neutral == false;
                }
                break;
            case Faction.Forgotten:
                if (faction == myfaction)
                    return false;
                if (faction != myfaction)
                {
                    if (faction == Faction.Dementors) return neutral == true;
                    if (faction != Faction.Dementors) return neutral == false;
                }
                break;
            case Faction.CCC:
                if (faction == myfaction)
                    return false;
                break;
            case Faction.Wanderers:
                if (faction == myfaction)
                    return false;
                break;
            case Faction.HiveMind:
                if (faction == myfaction)
                    return false;
                break;


        }
        return true;
    }
}
