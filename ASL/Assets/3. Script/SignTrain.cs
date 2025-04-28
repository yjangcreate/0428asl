using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EHandState
{
    None,                       
    Tired,                      
    Good,                       
    Bad,                        
}

public class SignTrain : MonoBehaviour
{
    public GlobalManager GlobalManager;
    public GameObject LeftHand;
    public GameObject RightHand;

    [Serializable]
    public class HandStep
    {
        public GameObject Root;
        public GameObject TargetLeftHand;
        public GameObject TargetRightHand;
    }
    public List<HandStep> TiredHandSteps = new List<HandStep>();
    public List<HandStep> GoodHandSteps = new List<HandStep>();
    public List<HandStep> BadHandSteps = new List<HandStep>();
    public int CurrentStepIndex = 0;
    public EHandState CurrentHandState = EHandState.None;

    public float vectorToleration = 0.05f;
    public float rotationToleration = 3f;

    public void SetNewState(EHandState newState)
    {
        TiredHandSteps.ForEach(step => {
            step.Root.SetActive(false);
        });

        GoodHandSteps.ForEach(step =>
        {
            step.Root.SetActive(false);
        });

        BadHandSteps.ForEach(step =>
        {
            step.Root.SetActive(false);
        });

        CurrentStepIndex = 0;
        CurrentHandState = newState;
    }

    public List<HandStep> GetHandSteps(EHandState state)
    {
        switch (state)
        {
            case EHandState.Tired:
                return TiredHandSteps;
            case EHandState.Good:
                return GoodHandSteps;
            case EHandState.Bad:
                return BadHandSteps;
            default:
                return null;
        }
    }

    public void Update()
    {
        var targetSteps = GetHandSteps(CurrentHandState);
        targetSteps.ForEach(step =>
        {
            step.Root.SetActive(false);
        });
        var stepHand = targetSteps[CurrentStepIndex];
        stepHand.Root.SetActive(true);

        var targetLeftHand = stepHand.TargetLeftHand;
        var targetRightHand = stepHand.TargetRightHand;

        bool condition = false;
        bool leftCondition = false;
        bool rightCondition = false;

        if (targetLeftHand.transform.parent.gameObject.activeSelf)
        {
            leftCondition = Vector3.Distance(LeftHand.transform.position, targetLeftHand.transform.position) < vectorToleration;
            leftCondition &= Quaternion.Angle(LeftHand.transform.rotation, targetLeftHand.transform.rotation) < rotationToleration;
        }
        else
        {
            leftCondition = true;
        }

        if (targetRightHand.transform.parent.gameObject.activeSelf)
        {
            rightCondition = Vector3.Distance(RightHand.transform.position, targetRightHand.transform.position) < vectorToleration;
            rightCondition &= Quaternion.Angle(RightHand.transform.rotation, targetRightHand.transform.rotation) < rotationToleration;
        }
        else
        {
            rightCondition = true;
        }
        condition = leftCondition && rightCondition;


        if (condition)
        {
            CurrentStepIndex += 1;
        }

        bool endCondition = CurrentStepIndex >= targetSteps.Count;
        if (endCondition)
        {
            GlobalManager.StepInputFeeling();
        }
    }
}
