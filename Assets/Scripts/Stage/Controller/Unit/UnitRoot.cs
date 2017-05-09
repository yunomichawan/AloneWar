using AloneWar.Stage.Range;
using AloneWar.Unit.Component;
using System.Collections.Generic;
using UnityEngine;

namespace AloneWar.Stage.Controller.Unit
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitRoot
    {
        #region property

        private UnitBaseComponent UnitBaseComponent { get; set; }

        /// <summary>
        /// key is positionId
        /// </summary>
        public List<RangeCommand> RootList { get { return this.rootList; } set { this.rootList = value; } }
        private List<RangeCommand> rootList = new List<RangeCommand>();

        /// <summary>
        /// 
        /// </summary>
        public List<Vector3> RootPositionList
        {
            get
            {
                return this.RootList.ConvertAll<Vector3>(r => r.MassComponent.transform.position);
            }
        }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitBaseStatus"></param>
        /// <param name="monoBehaviour"></param>
        public UnitRoot(UnitBaseComponent unitBaseComponent)
        {
            this.UnitBaseComponent = unitBaseComponent;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void InitRoot()
        {
            this.RootList.Clear();
        }

        /// <summary>
        /// 移動値の最大を超える場合は、新しく再度ルート設定
        /// </summary>
        /// <param name="rangeCommand"></param>
        public void AddRoot(RangeCommand rangeCommand)
        {
            if (this.RootList.Contains(rangeCommand))
            {
                this.BackRoot(rangeCommand);
            }
            else
            {
                if (this.RootList.Count > this.UnitBaseComponent.UnitBaseStatus.BaseStatus.Move)
                {
                    this.AddRootAndOther(rangeCommand);
                }
                else
                {
                    this.SetNewRoot(rangeCommand);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        public void AddRoot(int positionId)
        {
            RangeCommand rangeCommand;
            if (this.UnitBaseComponent.StageRange.MainRangeCommandList.TryGetValue(positionId, out rangeCommand))
            {
                this.AddRoot(rangeCommand);
            }
        }

        /// <summary>
        /// 引数のルートまで戻る
        /// </summary>
        /// <param name="rangeCommand"></param>
        private void BackRoot(RangeCommand rangeCommand)
        {
            for (int i = 0; i < this.RootList.Count; i++)
            {
                if (this.RootList[i].Equals(rangeCommand))
                {
                    this.RemoveRootAndOther(i + 1, this.RootList.Count - i - 1);
                    break;
                }
            }
        }

        /// <summary>
        /// 引数のルートの最短距離を設定
        /// </summary>
        /// <param name="rangeCommand"></param>
        public void SetNewRoot(RangeCommand rangeCommand)
        {
            this.RemoveRootAndOther(0, this.RootList.Count);
            int positionId = this.UnitBaseComponent.PositionId;
            rangeCommand.DirectionList.ForEach(d => {
                positionId = StageManager.Instance.StageInformation.GetDirectionPositionId(positionId, d);
                this.AddRootAndOther(this.UnitBaseComponent.StageRange.MainRangeCommandList[positionId]);
            }); 
        }

        /// <summary>
        /// 引数のルートの最短距離を設定
        /// </summary>
        /// <param name="rangeCommand"></param>
        public void SetNewRoot(int targetPositionId)
        {
            RangeCommand rangeCommand = this.UnitBaseComponent.StageRange.MainRangeCommandList[targetPositionId];
            this.SetNewRoot(rangeCommand);
        }

        /// <summary>
        /// ルート追加+α
        /// </summary>
        /// <param name="rangeCommand"></param>
        private void AddRootAndOther(RangeCommand rangeCommand)
        {
            this.RootList.Add(rangeCommand);
            // SetColor,etc...
            
        }

        /// <summary>
        /// ルート削除+α
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        private void RemoveRootAndOther(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                //this.RootList[i + index];
                // RemoveColor,etc...
            }
            this.RootList.RemoveRange(index, count);
        }

        /// <summary>
        /// クリア
        /// </summary>
        public void ClearRoot()
        {
            this.RemoveRootAndOther(0, this.RootList.Count);
            this.RootList.Clear();
        }
    }
}
