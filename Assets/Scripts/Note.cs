using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int NoteId;

    [TextArea] public string MyNote = "Please write a note";

    private void OnMouseUp()
    {
        Diary.FoundNote(this);

    }
}
