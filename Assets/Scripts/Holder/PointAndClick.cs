using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClick : Holder
{
    [SerializeField] private float refreshPeriod;
    
    private float _refresh;
    private Entity _carrier;

    public override void Init(Entity caster, Entity target)
    {
        base.Init(caster, target);
        _carrier = target;
        _refresh = 0f;
        transform.position = _carrier.transform.position;
        
        StopAllCoroutines();
        StartCoroutine(ManageLifeTime());
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_refresh > 0f)
        {
            _refresh -= Time.deltaTime;
        }
        else
        {
            ApplyEffects(_carrier);
            _refresh += refreshPeriod;
        }
        transform.position = _carrier.transform.position;
    }
}
