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
    public const string NoMoney = "Not enough money!";
    public const string HeroIsReady = "Open Hero menu to see smth!";
    public const string HeroOnField = "Hero is already on the field!";
    public const string HeroNotOnField = "Hero is not on the field!";
    public const string CannonDamage = "~~~UPGRADE!~~~\nCannon damage and radius increased";
    public const string CannonSpeed = "~~~UPGRADE!~~~\nCannon attack speed increased";
    public const string TowerHealth = "~~~UPGRADE!~~~\nTower health increased";
    public const string TowerRepair = "~~~UPGRADE!~~~\nTower repair score increased";
    public const string CheatMoney = "zoolkmnb";
    public const string CheatXP = "bzzxxccm";
    public const string CheatRep = "zxcvvcxz";
    public static List<string> Messages = new List<string>()
    {
        UnitUnlocked, SpellIsReady, HealthIncreased, AttackIncreased, LimitReached, HeroLvlUp, CannonDamage, 
        CannonSpeed, TowerHealth, TowerRepair, HeroOnField, HeroNotOnField, HeroIsReady
    };
    public static List<string> PauseText = new List<string>()
    {
        "I get tired sometimes, too.",
        "Write to the developer with your impressions of the game. It's important to him!",
        "Are they interfering with the game again? I see.",
        "Don't waste your time. Destroy this bot, you can do it!",
        "Someday the developer will learn how to balance the game.",
        "How do you feel?",
        "Click on the bot's nose and you'll see the Easter egg! Oh... He doesn't have a nose.",
        "Call your grandmother, she'll be pleased.",
        "Don't forget to brush your teeth at night!",
        "I'm tired of coming up with messages to the player... Let me out of the basement!",
        "How much longer will the bot attack you?!",
        "Use spells, they are very useful in most cases.",
        "To win you need to destroy the bot tower , not your own :)",
        "I heard somewhere that you can improve your troops.",
        "Improving the cannon is just as important as improving the troops.",
        "Life advice: don't trust anyone, even me.",
        "You can change the volume level in the settings.",
        "There's only one Easter egg in the game, can you find it?",
        "I developed my first game when I was 15 years old. No one played it, though.",
        "How often do you tell your family that you love them?",
        "2 + 2 is not equal to 5",
        "I hate broccoli."
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
