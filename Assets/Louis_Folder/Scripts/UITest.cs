using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI monTextUI;
    public Image monImage;


    float hp;
    public float hpMax =100;


    // Start is called before the first frame update
    void Start()
    {
        hp = hpMax;
        StartCoroutine(GetDammage());
    }

    // Update is called once per frame
    void Update()
    {
        monTextUI.text = "HP : " + hp.ToString("00");
        monImage.fillAmount = hp/hpMax;
        monImage.color = Color.Lerp(Color.red,Color.green,hp/hpMax) ;
    }

    private IEnumerator GetDammage()
    {
        while (hp>0)
        {

            hp --;
            yield return new WaitForSeconds(0.5f);
        }
    }

}
