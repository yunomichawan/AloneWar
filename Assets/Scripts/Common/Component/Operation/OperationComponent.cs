using AloneWar.Common.Component.Operation.State;
using UnityEngine;

namespace AloneWar.Common.Component.Operation
{
    /// <summary>
    /// 操作クラス
    /// </summary>
    public class OperationComponent : SingletonComponent<OperationComponent>
    {
        #region property

        public OperationStateInfo Operation { get { return this.operation; } set { this.operation = value; } }
        private OperationStateInfo operation = new OperationStateInfo();

        #endregion

        void Update()
        {
            if (this.Operation.Enabled)
            {
                this.SetOperationState();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetOperationState()
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = touch.position;
            Vector3 mousePosition = Input.mousePosition;
            this.InvokeOperation(touchPosition, this.Operation.TouchState);
            this.InvokeOperation(mousePosition, this.Operation.MouseState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="baseOperationState"></param>
        private void InvokeOperation(Vector3 position, BaseOperationState baseOperationState)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            Collider2D collider2D = Physics2D.OverlapPoint(worldPosition);
            if (collider2D != null)
            {
                baseOperationState.SetState(collider2D.gameObject);
            }
            else
            {
                baseOperationState.SetState(null);
            }
        }

    }
}