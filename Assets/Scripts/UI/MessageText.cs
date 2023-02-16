using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public static class Constants
{
    public static string pathSettings = Application.persistentDataPath + "/settings.json";
    public const string UnitUnlocked = "New unit unlocked!";
    public const string SpellIsReady = "Spell is ready for use!";
    public const string HealthIncreased = "Health increased!";
    public const string AttackIncreased = "Attack damage increased!";
    public const string LimitReached = "Limit reached!";
    public const string HeroLvlUp = "New hero level!";
    public static List<string> Messages = new List<string>()
    {
        UnitUnlocked, SpellIsReady, HealthIncreased, AttackIncreased, LimitReached, HeroLvlUp
    };
    public static List<string> Congratulations = new List<string>()
    {
        "I admire your skill, bravo!",
        "I always knew that you would succeed!",
        "How many times have you trained to become a pro?",
        "I give the winner's crown to you, well done!",
        "You're either very lucky or just a pro...",
        "Your cat should be proud of you!",
        "It's your time to become the best in this game!"
    };
    public static List<string> Lose = new List<string>()
    {
        "Try again, you will succeed!",
        "Worse things happen in life, don't worry!",
        "I'll fix the AI, maybe it's too strong.",
        "Defeats happen to everyone, the main thing is to try again!",
        "I couldn't win the AI either."
    };

}
public class MessageText : MonoBehaviour
{
    public static Action<string> sendMessage;
    [SerializeField] private TMP_Text _textUser;
    [SerializeField] private AudioSource _audioSource;
    private bool _flag = false;
    private void OnEnable()
    {
        sendMessage += ShowMessageUser;
    }
    private void OnDisable()
    {
        sendMessage -= ShowMessageUser;
    }
    private void ShowMessageUser(string message)
    {
        if (!_flag)
        {
            if (Constants.Messages.Contains(message))
            {
                _audioSource.Play();
            }
            _textUser.text = message;
            _textUser.gameObject.SetActive(true);
            StartCoroutine(ClassicFade(_textUser));
        }
    }

    private IEnumerator ClassicFade(TMP_Text text)
    {
        _flag = true;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        yield return new WaitForSeconds(2f);
        for (float i = 1; i > 0; i -= Time.deltaTime * 2)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        text.gameObject.SetActive(false);
        _flag = false;
    }
}
