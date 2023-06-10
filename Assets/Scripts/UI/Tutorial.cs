using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _arrow;
    [SerializeField] private RectTransform[] _places;
    [SerializeField] private GameObject _spider;
    private bool _cameraTranslated = false;
    private int _menu = -1;
    private void OnEnable()
    {
        CameraController.OnCameraTranslated += CameraTrans;
        ButtonController.TutorialMenu += GetMenu;
        TextController.updatePlayerUI += GetUnit;
    }
    private void OnDisable()
    {
        CameraController.OnCameraTranslated -= CameraTrans;
        ButtonController.TutorialMenu -= GetMenu;
        TextController.updatePlayerUI -= GetUnit;
    }
    public IEnumerator StartTutorial()
    {
        _spider.SetActive(true);
        yield return StartCoroutine(TypeText("Hi! Let me show you how to play.", 1f));
        yield return StartCoroutine(TypeText("Let's learn how to control the camera. Press A or D to move the camera!", 0f));
        yield return new WaitUntil(() => _cameraTranslated);
        _arrow.gameObject.SetActive(true);
        _arrow.rectTransform.position = _places[0].position;
        yield return StartCoroutine(TypeText("All right! Now let's look at this.", 1f));
        yield return StartCoroutine(TypeText("It's your money. You can use it to buy troops, as well as various improvements and spells."));
        _arrow.rectTransform.position = _places[1].position;
        yield return StartCoroutine(TypeText("This is your experience. You can get it by killing enemies. It unlocks new troops and spells."));
        yield return StartCoroutine(TypeText("Now let's try to recruit troops.", 1f));
        _arrow.transform.Rotate(0, 0, 90f);
        _arrow.rectTransform.position = _places[2].position;
        yield return StartCoroutine(TypeText("Click on this menu", 0f));
        yield return new WaitUntil(() => _menu == 0);
        _arrow.transform.Rotate(0, 0, -90f);
        _arrow.rectTransform.position = _places[3].position;
        yield return StartCoroutine(TypeText("Now click on a unit", 0f));
        yield return new WaitUntil(() => _menu == -1);
        _arrow.rectTransform.position = _places[4].position;
        _arrow.transform.Rotate(0, 0, 180f);
        yield return StartCoroutine(TypeText("Look! This is the unit hiring queue. There can only be a maximum of 15 units on the field, so hire them wisely!", 3f));
        _arrow.gameObject.SetActive(false);
        yield return StartCoroutine(TypeText("Cool! Okay, you can figure out the rest of the menus on your own."));
        yield return StartCoroutine(TypeText("Good luck! And remember, spiders cannot be trusted..."));
        _spider.SetActive(false);
    }
    private IEnumerator TypeText(string s, float pause = 2f)
    {
        _text.text = "";
        for (int i = 0; i < s.Length; i++)
        {
            _text.text += s[i];
            yield return new WaitForSeconds(0.06f);
        }
        yield return new WaitForSeconds(pause);
    }
    private void CameraTrans(Vector3 v)
    {
        _cameraTranslated = true;
    }
    private void GetMenu(int index)
    {
        _menu = index;
    }
    private void GetUnit()
    {
        _menu = -1;
    }
}
