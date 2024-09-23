using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeAOE : AOE
{
    public override void Init(Entity caster, Vector3 launchPosition)
    {
        base.Init(caster, launchPosition);
        transform.position = caster.transform.position;
        
        Vector3 direction = Statics.GetVector(caster.transform.position, launchPosition);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        _refresh = 0f;
        _touchingEntity = false;
        
        StopAllCoroutines();
        StartCoroutine(ManageLifeTime());
    }
}
