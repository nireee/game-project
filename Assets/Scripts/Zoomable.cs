using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomable : MonoBehaviour
{
    public bool Completed;

    public bool Expanded;
    public bool expanding = false;
    //private bool open;

    public SpriteButton CancelButton;

    protected Vector2 DefaultScale;
    protected float TargetScale;
    public float ExpandedScale = 4;
    public float ExpansionRate = 1;

    [SerializeField] protected BoxCollider2D ZoomCollider;
    private void Start()
    {
        ParentStart();
    }

    private void Update()
    {
        ParentUpdate();
    }
    // Start is called before the first frame update
    protected void ParentStart()
    {
        DefaultScale = transform.lossyScale;
        CancelButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected void ParentUpdate()
    {
        if (!Expanded && expanding)
        {
            TargetScale = ExpandedScale;
            Expanded = scaleItem(ExpansionRate, DefaultScale, TargetScale, transform);
            if (Expanded)
            {
                ZoomCollider.enabled = false;
                CancelButton.gameObject.SetActive(true);
            }
        }
        else if (Expanded && !expanding)
        {
            TargetScale = 1;
            Expanded = !scaleItem(-ExpansionRate, DefaultScale, TargetScale, transform);
            if (!Expanded) ZoomCollider.enabled = true;
        }
        else if (!ZoomCollider.enabled && completedCondition())
        {
            ZoomCollider.enabled = true;
        }
    }
    protected virtual void closeZoomable()
    {
        transform.localScale = DefaultScale;
    }

    private void SpriteButton()
    {

        expanding = false;
        Expanded = false;
        CancelButton.gameObject.SetActive(false);
        ZoomCollider.enabled = true;

        closeZoomable();
    }

    protected virtual bool completedCondition()
    {
        print("override error");
        return false;
    }

    public static bool scaleItem(float rate, Vector2 DefaultScale, float TargetScale, Transform transform)
    {
        rate *= DefaultScale.x;
        float currentScale = transform.localScale.x / DefaultScale.x;
        if (rate < 0 && TargetScale - currentScale > rate * Time.deltaTime || rate > 0 && TargetScale - currentScale < rate * Time.deltaTime)
        {
            transform.localScale = TargetScale * DefaultScale;
            return true;
        }
        else
        {
            float yScale = DefaultScale.y / DefaultScale.x;
            transform.localScale += rate * Time.deltaTime * new Vector3(1, yScale, 1);
            return false;
        }
    }

    protected bool ParentCanTouch()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject) && !Completed && Expanded && expanding) return true;
        else return false;
    }

    private void OnMouseDown()
    {
        if (FindObjectOfType<TouchHandler>().CanTouch(gameObject) && openPuzzleCheck())
        {
            if (Completed)
            {

                ZoomCollider.enabled = false;

                completedOpen();

            }
            else if (!Expanded && !expanding) expanding = true;
        }
    }

    protected virtual void completedOpen()
    {

    }

    protected virtual bool openPuzzleCheck()
    {
        return true;
    }
}