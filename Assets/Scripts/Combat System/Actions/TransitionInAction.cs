using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(menuName = "Combat State/State Actions/TransitionIn")]
    public class TransitionInAction : CombatStateAction
    {
        public override void CallAction(CombatManager manager)
        {
            manager.StartCoroutine(TestEnum(manager));
        }

        public IEnumerator TestEnum(CombatManager manager)
        {
            Debug.Log("Transitioning");
            yield return new WaitForSeconds(2);
            Debug.Log("Transition Over");
            //manager.ChangeState(1);
        }
    }
}
