using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.TweenLib
{
    public abstract class TransformTween : VectorTween
    {      
        protected Transform targetTransform;

        public TransformTween(VectorData data) : base(data)
        { 
        }
        
        protected override void SetStartObject(GameObject go)
        {
            targetTransform = go.transform;
        }
    }
    
}