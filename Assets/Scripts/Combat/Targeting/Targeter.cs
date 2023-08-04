using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    private Camera mainCamera;
    public List<Target> targets = new();

    public Target CurrentTarget{get; private set;}

     private void Start() {
        mainCamera =Camera.main;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        RemoveTarget(target);
    }


    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closesTarget  = null;
        float closesTargetDistance = Mathf.Infinity;
        foreach(Target target in targets)
        {
            Vector2 vievPos = mainCamera.WorldToViewportPoint(target.transform.position);
            //if(vievPos.x <0 || vievPos.x > 1 ||vievPos.y <0 || vievPos.y > 1 )
            if(!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }
            Vector2 toCenter = vievPos - new Vector2(0.5f, 0.5f);
            if(toCenter.sqrMagnitude < closesTargetDistance)
            {
                closesTarget = target;
                closesTargetDistance = toCenter.sqrMagnitude;
            }
        }
        if(closesTarget == null){ return false;}
        CurrentTarget = closesTarget;
        cinemachineTargetGroup.AddMember(CurrentTarget.transform,1,2);
        return true;
    }

    public void Cancel()
    {
        if(CurrentTarget == null){return;}
        cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
             cinemachineTargetGroup.RemoveMember(CurrentTarget.transform);
             CurrentTarget = null;
        }
         target.OnDestroyed -= RemoveTarget;
         targets.Remove(target);
    }
}
