using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SadidaPuppet : Summon
{
   public override void LaunchAuto(Entity target)
   {
      spellManager.CastOnSelf(spellBook.AutoAttack);
      StartCoroutine(SelfDestruct());
   }

   private IEnumerator SelfDestruct()
   {
      yield return new WaitForSeconds(0.2f);
      hp = -1;
   }
}
