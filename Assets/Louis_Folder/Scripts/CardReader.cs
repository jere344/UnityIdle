using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardReader : MonoBehaviour
{

    private int _currentHp;
    [SerializeField]
    private TextMeshProUGUI _hpText,_nameText,_typeText;
    [SerializeField]
    private Image _cardImage;

    [SerializeField]
    private PokemonCard _currentCard;

    [SerializeField]
    private PokemonCard[] _deck;
    
    void Start()
    {
        ReadCard(_deck[Random.Range(0,_deck.Length)]);
    }

    public void ReduceHp()
    {
        _currentHp -= 10;
        _hpText.text = _currentHp.ToString("000");
        if( _currentHp <= 0 )
        {
            ReadCard(_deck[Random.Range(0, _deck.Length)]);
        }
    }
    
    private void ReadCard(PokemonCard newCard)
    {
        _currentCard = newCard;

        _currentHp = _currentCard.hpBase;

        _hpText.text = _currentCard.hpBase.ToString("00");
        _nameText.text = _currentCard.cardName.ToString();
        _typeText.text = _currentCard.type.ToString();

        _cardImage.sprite = _currentCard.cardImage;

        switch (_currentCard.type)
        {
            case PokemonType.Eau:
                Camera.main.backgroundColor = Color.blue;
                break;
            case PokemonType.Feu:
                Camera.main.backgroundColor = Color.red;
                break;
            case PokemonType.Psy:
                Camera.main.backgroundColor = Color.magenta;
                break;
            case PokemonType.Obscurite:
                Camera.main.backgroundColor = Color.black;
                break;
            case PokemonType.Metal:

                Camera.main.backgroundColor = Color.gray;
                break;
            case PokemonType.Combat:
                Camera.main.backgroundColor = Color.grey;
                break;
            case PokemonType.Incolore:
                Camera.main.backgroundColor = Color.white;
                break;
            case PokemonType.Electric:
                Camera.main.backgroundColor = Color.yellow;
                break;
            case PokemonType.Plante:
                Camera.main.backgroundColor = Color.green;
                break;
            case PokemonType.Dragon:
                Camera.main.backgroundColor = Color.white;
                break;
            default:
                break;
        }
    }

}
