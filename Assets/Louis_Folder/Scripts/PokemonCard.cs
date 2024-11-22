using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="new_card", menuName ="Pokemon/Card",order =0)]
public class PokemonCard : ScriptableObject
{

    public string cardName;
    public PokemonType type;
    public Rarete rarete;
    public int hpBase;
    public int evolutionStage;
    public Sprite cardImage;
}

