using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AloneWar.Common.Component;
using UnityEngine;
using AloneWar.Stage.FieldObject;

namespace AloneWar.Stage
{
    /// <summary>
    /// 
    /// </summary>
    public class StageManager : SingletonComponent<StageManager>
    {

        #region poroperty

        public StageInformation StageInformation { get; set; }

        #endregion

        void Start()
        {

        }

        public void StageInit()
        {
            
        }

        /// <summary>
        /// 座標Id
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetPositionId(int x, int y)
        {
            return this.StageInformation.StageTableData.Width * y + x;
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class StageBuilder
    {
        public List<MassStatus> MassStatusList { get; set; }

        public StageBuilder(string constitutionJson)
        {
            this.MassStatusList = JsonUtility.FromJson<List<MassStatus>>(constitutionJson);
        }

        public void CreateStage()
        {
            this.CreateField();
        }

        private void CreateField()
        {

        }
    }
}
