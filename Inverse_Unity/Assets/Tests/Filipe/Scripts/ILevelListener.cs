using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Interfaces
{

    public interface ILevelListener<T>
    {
        public void OnNotify(T enums);
    }

}
