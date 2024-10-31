using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TeleportArrival : Holder
{
   [SerializeField] private float delay;
   private bool _teleportDone;
   private Entity _targetDestination = null;
   
   public override void Init(Entity caster, Vector3 launchPosition)
   {
      Caster = caster;
      NavMeshHit hit;
      if (NavMesh.SamplePosition(launchPosition, out hit, 2.0f, NavMesh.AllAreas)) {
         transform.position = hit.position;
      }
      _teleportDone = false;
      
      StopAllCoroutines();
      StartCoroutine(TeleportCaster());
   }

   public override void Init(Entity caster, Entity target)
   {
      Caster = caster;
      _targetDestination = target;
      transform.position = _targetDestination.transform.position;
      _teleportDone = false;
      
      StopAllCoroutines();
      StartCoroutine(TeleportCaster());
   }

   private void Update()
   {
      if (spellData.targeted)
      {
         transform.position = _targetDestination.transform.position;
      }
      
      
      if (_teleportDone)
      {
         StartCoroutine(ManageLifeTime());
      }
   }

   private IEnumerator TeleportCaster()
   {
      yield return new WaitForSeconds(delay);
      if (spellData.targeted)
      {
         Vector3 direction = Statics.GetDirection(Caster, _targetDestination);
         Caster.transform.position = transform.position + direction;
         ApplyEffects(_targetDestination);
      }
      else
      {
         Caster.transform.GetComponent<NavMeshAgent>().enabled = false;
         NavMeshHit hit;
         if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, NavMesh.AllAreas)) {
            Caster.transform.position = hit.position;
         }
         Caster.transform.GetComponent<NavMeshAgent>().enabled = true;
      }
      _teleportDone = true;
   }
}
