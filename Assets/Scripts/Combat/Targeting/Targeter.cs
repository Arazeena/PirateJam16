using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    public Target CurrentTarget { get; private set; }
    private List<Target> targets = new List<Target>();
    private Camera camera;

    private void Start(){
        camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other){
        
        if(!other.TryGetComponent<Target>(out Target target))
            return;
        
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other){
        if(!other.TryGetComponent<Target>(out Target target) || !targets.Contains(target))
            return;
        
        RemoveTarget(target);
    }

    public bool SelectTarget(){
        if(targets.Count == 0)
            return false;

        Target closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach(Target target in targets){
            Vector2 viewPos = camera.WorldToViewportPoint(target.transform.position);
            if(viewPos.x > 1f || viewPos.x < 0f
                || viewPos.y > 1f || viewPos.y < 0f)
                continue;

            float dist = (viewPos - new Vector2(0.5f, 0.5f)).SqrMagnitude();
            if(dist < closestDistance){
                closestTarget = target;
                closestDistance = dist;
            }
        }  

        if(closestTarget == null)
            return false;

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel(){
        if(CurrentTarget == null)
            return;
        
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target){
        Cancel();
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
