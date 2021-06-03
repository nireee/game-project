using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : MonoBehaviour
{
    public static Diary StaticDiary;
    [SerializeField] private bool[] notesFound = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        if (StaticDiary == null)
        {
            DontDestroyOnLoad(gameObject);
            StaticDiary = this;
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int GetNoteFoundCount()
    {
        int count = 0;
        foreach(bool hasNote in StaticDiary.notesFound)
        {
            if (hasNote) count += 1;
        }
        return count;
    }

    public static void FoundNote(Note note)
    {
        StaticDiary.notesFound[note.NoteId] = true;
        StaticDiary.DisplayNote(note);
    }

    private void DisplayNote(Note note)
    {
        //find text window to display
        FindObjectOfType<DialogWindow>().DisplayDialog(note.MyNote);
    }


}
