using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public static class MathUtils
    {
        public static void AddRandom(this ref Vector3 myVector, Vector3 min, Vector3 max)
        {
            myVector += new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
        }
    }
}
