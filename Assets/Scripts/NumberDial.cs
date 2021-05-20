using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDial : MonoBehaviour
{
    public Transform NumbersContainer;
    public Transform[] Numbers;
    private float[] NumbersDelta = new float[9];
    private float totalDelta;

    public int TargetNumber = 1;
    public float DialSpeed = 1;
    private bool movingDial = false;
    public int DisplayedNum;
    public int CurrentNum;

    public NumberLock NumberLock;
    public int[] numlist = new int[9];
    public bool movingUp = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 9; i += 1)
        {
            NumbersDelta[i] = Numbers[i].localPosition.y;
        }
        totalDelta = NumbersDelta[8];
        if (NumberLock.Completed) SetDial(DisplayedNum);
        else SetDial(TargetNumber);
    }

    // Update is called once per frame
    void Update()
    {
        handleNumberPlacement();
        moving();
    }

    //public bool movingDown = false;
    void handleNumberPlacement()
    {
        float yPos = -NumbersContainer.localPosition.y % totalDelta;
        int currentIndex = 0;
        for (int i = 0; i < Numbers.Length; i += 1)
        {

            //if(i == 0 && (yPos > 0 || yPos > NumbersDelta[8]))
            //if(i == 8 && (yPos < NumbersDelta[7] || yPos < NumbersDelta[0]))
            //check moving up
            bool middle = (i != 0 && i != 8 && (movingUp && yPos < NumbersDelta[i + 1] && yPos > NumbersDelta[i - 1] || !movingUp && yPos > NumbersDelta[i - 1] && yPos <= NumbersDelta[i]));
            bool top = (i == 8 && (yPos > NumbersDelta[7] || yPos < NumbersDelta[0]));
            bool bottom = (i == 0 && ((!movingUp && yPos > 0 && yPos < NumbersDelta[1])));
            if (middle || bottom || top)
            //if (yPos < NumbersDelta[i + 1])

            {
                currentIndex = i;
                numlist[4] = i + 1;
                CurrentNum = numlist[4];
                //top of dial
                for (int j = 0; j < 4; j += 1)
                {
                    int dialVal = (i + j + 1) % 9 + 1;
                    numlist[4 + j + 1] = dialVal;

                    if (dialVal < currentIndex + 1)
                    {
                        Numbers[dialVal - 1].localPosition = new Vector2(Numbers[dialVal - 1].localPosition.x, NumbersDelta[dialVal - 1] + totalDelta);
                    }
                    else
                    {
                        Numbers[dialVal - 1].localPosition = new Vector2(Numbers[dialVal - 1].localPosition.x, NumbersDelta[dialVal - 1]);
                    }
                }
                //bottom of dial
                for (int k = 0; k < 4; k += 1)
                {
                    int dialVal = i - 1 - k + 1;
                    if (dialVal < 1) dialVal += 9;
                    numlist[4 - k - 1] = dialVal;
                    if (dialVal > currentIndex + 1)
                    {
                        Numbers[dialVal - 1].localPosition = new Vector2(Numbers[dialVal - 1].localPosition.x, NumbersDelta[dialVal - 1] - totalDelta);
                    }
                    else
                    {
                        Numbers[dialVal - 1].localPosition = new Vector2(Numbers[dialVal - 1].localPosition.x, NumbersDelta[dialVal - 1]);
                    }
                }

                if (currentIndex == 8) Numbers[8].localPosition = new Vector2(Numbers[8].localPosition.x, NumbersDelta[8]);
                else if (currentIndex == 0) Numbers[0].localPosition = new Vector2(Numbers[0].localPosition.x, NumbersDelta[0]);
                Numbers[numlist[0] - 1].gameObject.SetActive(false);
                Numbers[numlist[1] - 1].gameObject.SetActive(false);
                Numbers[numlist[2] - 1].gameObject.SetActive(false);
                Numbers[numlist[3] - 1].gameObject.SetActive(true);
                Numbers[numlist[4] - 1].gameObject.SetActive(true);
                Numbers[numlist[5] - 1].gameObject.SetActive(true);
                Numbers[numlist[6] - 1].gameObject.SetActive(false);
                Numbers[numlist[7] - 1].gameObject.SetActive(false);
                Numbers[numlist[8] - 1].gameObject.SetActive(false);
                break;
            }


        }

        //NumbersContainer.localPosition = new Vector2(NumbersContainer.localPosition.x, -yPos);

    }

    private void SetDial(int num)
    {
        NumbersContainer.localPosition = new Vector2(NumbersContainer.localPosition.x, -NumbersDelta[num - 1]);
        TargetNumber = num;
    }


    public void MoveDialUp()
    {
        if (NumberLock.CanTouch())
        {
            if (TargetNumber == 1 && CurrentNum == 1) TargetNumber = 9;
            else if (TargetNumber == CurrentNum) TargetNumber -= 1;
        }


    }

    public void MoveDialDown()
    {
        if (NumberLock.CanTouch())
        {
            if (TargetNumber == 9 && CurrentNum == 9) TargetNumber = 1;
            else if (TargetNumber == CurrentNum) TargetNumber += 1;
        }


    }

    private void moving()
    {
        float targetY = -NumbersDelta[TargetNumber - 1];
        movingUp = targetY > NumbersContainer.localPosition.y;
        //movingDown = targetY < NumbersContainer.localPosition.y;

        if (TargetNumber == 1 && NumbersContainer.localPosition.y < -NumbersDelta[6]) targetY = -NumbersDelta[8] - NumbersDelta[0];
        else if (TargetNumber == 9 && NumbersContainer.localPosition.y > -NumbersDelta[2]) targetY = 0;


        NumbersContainer.localPosition = Vector2.MoveTowards(NumbersContainer.localPosition, new Vector2(0, targetY), DialSpeed * Time.deltaTime);

        if (NumbersContainer.localPosition.y > -NumbersDelta[0] && TargetNumber == 9) NumbersContainer.localPosition += new Vector3(0, -totalDelta);
        else if (NumbersContainer.localPosition.y < -NumbersDelta[8] && TargetNumber == 1) NumbersContainer.localPosition += new Vector3(0, totalDelta);


    }
}



