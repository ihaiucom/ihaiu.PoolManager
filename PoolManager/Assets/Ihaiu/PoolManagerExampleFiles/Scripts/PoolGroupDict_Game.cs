using UnityEngine;
using System.Collections;

namespace Ihaius
{
    public partial class PoolGroupDict
    {
        /** 战斗对象池组 */
        public PoolGroup war
        {
            get
            {
                if (!ContainsKey("WarPoolGroup"))
                {
                    Create("WarPoolGroup");
                }

                return this["WarPoolGroup"];
            }
        }

    }
}