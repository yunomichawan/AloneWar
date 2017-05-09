using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.FieldObject;
using System.Collections.Generic;
using UnityEngine;

namespace AloneWar.Stage.Component
{
    public class MassComponent : BaseStageObject
    {
        #region property

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public override int StageObjectId
        {
            get { return this.MassStatus.PositionId; }
        }

        public override int PositionId { get { return this.MassStatus.PositionId; } set { this.MassStatus.PositionId = value; } }

        public override string Area { get { return this.MassStatus.Area; } }

        public override GameObjectCategory GameObjectCategory
        {
            get
            {
                return GameObjectCategory.Mass;
            }
        }
        
        public MassStatus MassStatus { get; set; }

        #region Event

        public List<StageEventHandler> StageEventHandlerList { get { return this.StageEventHandlerList; } set { this.StageEventHandlerList = value; } }
        private List<StageEventHandler> stageEventHandlerList = new List<StageEventHandler>();

        #endregion

        public bool IsClose
        {
            get
            {
                return this.MassStatus.IsClose;
            }
            set
            {
                this.MassStatus.IsClose = value;
                this.StageEventHandlerList.ForEach(s =>
                {
                    if (s.StageTriggerSender.IsValidTrigger(this.PositionId))
                    {
                        s.EnqueueEventTask();
                    }
                });

            }
        }

        #endregion

        #region material

        /// <summary>
        /// Materialで行う必要性はある？SpriteRenderでもいけるのでは？要検証
        /// </summary>
        /// <param name="commandCategory"></param>
        public void SetMaterialColor(CommandCategory commandCategory)
        {
            this.SetMaterialColor(this.GetColorFromCommand(commandCategory));
        }

        public void SetMaterialColor(Color color)
        {
            EditorDebug.DebugAlert("マス色の設定方法");
            if (this.spriteRenderer.material.HasProperty(AloneWarConst.MaterialProperty.MassColor))
            {
                // ↓でもできるのでは？制約として、Animationを実行できなくなる。(Animationによる色で上書きされるため。←色を指定しなければ大丈夫では？)
                //this.spriteRenderer.color = this.GetColorFromCommand(commandCategory);
                this.spriteRenderer.material.SetColor(AloneWarConst.MaterialProperty.MassColor, color);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandCategory"></param>
        /// <returns></returns>
        private Color GetColorFromCommand(CommandCategory commandCategory)
        {
            EditorDebug.DebugAlert("マス色の定数化");
            switch (commandCategory)
            {
                case CommandCategory.Attack:
                    return Color.red;
                case CommandCategory.Move:
                    return Color.blue;
                case CommandCategory.Summon:
                case CommandCategory.HpSummon:
                case CommandCategory.MpSummon:
                case CommandCategory.HalfSummon:
                    return Color.magenta;
                default:
                    return new Color(0, 0, 0, 1);
            }
        }

        #endregion
    }
}
