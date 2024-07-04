using TMPro;
using UnityEngine;

public class BlackHole_HotKey_Controller : MonoBehaviour
{
    private KeyCode myHotkey;
    private TextMeshProUGUI myText;
    private SpriteRenderer sr;

    private Transform myEnemy;
    private BlackHole_Skill_Controller blackHole;
    public void SetupHotKey(KeyCode _myNewHotkey, Transform _myEnemy, BlackHole_Skill_Controller _myBlackHole)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        blackHole = _myBlackHole;

        myHotkey = _myNewHotkey;
        myText.text = _myNewHotkey.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(myHotkey))
        {
            blackHole.AddEnemyToList(myEnemy);
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
