using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AloneWar.Common.TaskHelper
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TaskCoroutineBeahavior : MonoBehaviour
    {
        #region プロパティ

        /// <summary>
        /// タスクキュー
        /// </summary>
        public Queue<Func<IEnumerator>> TaskQueue { get { return this._TaskQueue; } set { this._TaskQueue = value; } }
        private Queue<Func<IEnumerator>> _TaskQueue = new Queue<Func<IEnumerator>>();

        /// <summary>
        /// タスク完了フラグ
        /// </summary>
        public bool TaskCompleted { get; set; }

        #endregion

        /// <summary>
        /// タスクの初期化
        /// </summary>
        protected void TaskInit()
        {
            this.TaskQueue = new Queue<Func<IEnumerator>>();
            this.TaskCompleted = false;
        }

        /// <summary>
        /// タスク実行
        /// </summary>
        protected void TaskRun()
        {
            this.TaskCompleted = false;
            StartCoroutine(this.TaskStart());
        }

        /// <summary>
        /// タスクを順々に実行
        /// </summary>
        /// <returns></returns>
        private IEnumerator TaskStart()
        {
            while (true)
            {
                // タスク取り出し
                Func<IEnumerator> task = this.TaskQueue.Dequeue();
                yield return StartCoroutine(task());
                // タスクが無くなり次第終了
                if (this.TaskQueue.Count.Equals(0))
                {
                    this.TaskCompleted = true;
                    break;
                }
            }
        }

    }
}
