using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameStudioTest1
{
    public class AIUnit : Unit
    {
        protected virtual Unit ChooseTarget(List<Unit> availableTargets)
        {
            return availableTargets.OrderBy(at => Vector3.Distance(transform.position, at.transform.position)).FirstOrDefault();
        }
    } 
}
