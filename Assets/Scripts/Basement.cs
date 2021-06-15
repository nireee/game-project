using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : MonoBehaviour
{
    public float time = 2;
    private float start;
    private Diary diary;
    public DialogWindow dialogWindow;
    private bool endingDisplayed = false;

    [TextArea] public string Ending1Text, Ending2Text, Ending3Text, Ending4Text;
    // Start is called before the first frame update
    void Start()
    {
        start = Time.fixedTime;
        print("Welcome to the end");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > time + start) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        if (!diary) diary = Diary.StaticDiary;
        else if(!endingDisplayed)
        {
            HandleEndings();
            Destroy(Diary.StaticDiary.gameObject);
            endingDisplayed = true;
        }
    }

    private void HandleEndings()
    {
        int notesFound = Diary.GetNoteFoundCount();
        if (notesFound == 1) dialogWindow.DisplayDialog(Ending1Text);
        else if (notesFound == 2) dialogWindow.DisplayDialog(Ending2Text);
        else if (notesFound == 3) dialogWindow.DisplayDialog(Ending3Text);
        else dialogWindow.DisplayDialog(Ending4Text);

    }

}
