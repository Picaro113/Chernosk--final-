using UnityEngine;

[CreateAssetMenu(fileName = "Factions", menuName = "Factions/Factions")]
public class FactionNames : ScriptableObject
{
    public enum Faction
    {


        Dementors, //0
        Forgotten, //1
        CCC, //2
        Wanderers //3

    }

    public Faction myfaction;

    public bool GetFaction(Faction faction) 
    {

        if (faction != myfaction)
        {
            return true;
        }

        if (faction == myfaction)
        {
            return false;
        }

        switch (faction)
        {
            case Faction.Dementors:
                if (faction == myfaction)
                    return false;
                break;
            case Faction.Forgotten:
                if (faction == myfaction)
                    return false;
                break;
            case Faction.CCC:
                if (faction == myfaction)
                    return false;
                break;
            case Faction.Wanderers:
                if (faction == myfaction)
                    return false;
                break;


        }
        return false;
    }
}
