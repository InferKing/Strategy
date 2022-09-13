using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextController : MonoBehaviour
{
    [SerializeField] private TMP_Text _money, _rep, _exp;
    [SerializeField] private TMP_Text[] _texts;
    [SerializeField] private TMP_Text _textUser;
    public static Action<GameObject, int> showUnitUI;
    public static Action updatePlayerUI;
    private void OnEnable()
    {
        showUnitUI += ShowDamageUI;
        updatePlayerUI += UpdateUI;
    }
    private void OnDisable()
    {
        showUnitUI -= ShowDamageUI;
        updatePlayerUI -= UpdateUI;
    }

    private void UpdateUI()
    {
        _money.text = Singleton.Instance.Player.GetMoney().ToString();
        _rep.text = Singleton.Instance.Player.GetRep().ToString();
        _exp.text = Singleton.Instance.Player.GetExp().ToString();
    }
    private void ShowDamageUI(GameObject unit, int damage)
    {
        TMP_Text text = null;
        foreach (var t in _texts)
        {
            if (!t.gameObject.activeSelf)
            {
                text = t;
                break;
            }
        }
        if (text != null)
        {
            text.gameObject.SetActive(true);
            text.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y + 1, unit.transform.position.z);
            text.text = damage.ToString();
            StartCoroutine(Fade(text.gameObject));
        }
    }

    private IEnumerator Fade(GameObject unit)
    {
        Color color = unit.GetComponent<TMP_Text>().color;
        int rand = UnityEngine.Random.Range(0,2)+1;
        for (float i = 1; i > 0; i -= Time.deltaTime*2)
        {
            unit.GetComponent<TMP_Text>().color = new Color(color.r, color.g, color.b, i);
            unit.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y + Time.deltaTime*rand,
                unit.transform.position.z);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        unit.SetActive(false);
    }
}
