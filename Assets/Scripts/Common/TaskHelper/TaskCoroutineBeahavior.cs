using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        /// <summary>
        /// スキップ
        /// </summary>
        public bool IsSkip { get; set; }

        #endregion

        /// <summary>
        /// タスクの初期化
        /// </summary>
        protected void TaskInit()
        {
            this.TaskQueue = new Queue<Func<IEnumerator>>();
            this.TaskCompleted = false;
            this.IsSkip = false;
        }

        /// <summary>
        /// タスク実行
        /// </summary>
        public IEnumerator TaskRun()
        {
            this.TaskCompleted = false;
            this.IsSkip = false;
            yield return StartCoroutine(this.TaskStart());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator TaskNext()
        {
            this.TaskCompleted = false;
            Func<IEnumerator> task = this.TaskQueue.Dequeue();
            yield return StartCoroutine(task());
            if (this.IsSkip)
            {
                while (true)
                {
                    Func<IEnumerator> remaindTask = this.TaskQueue.Dequeue();
                    remaindTask();
                    if (this.TaskQueue.Count < 0)
                    {
                        break;
                    }
                }
                this.TaskEnd();
            }
            // タスクが無くなり次第終了
            if (this.TaskQueue.Count.Equals(0))
            {
                this.TaskEnd();
            }
            this.TaskCompleted = true;
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
                if (this.IsSkip)
                {
                    this.TaskEnd();
                    break;
                }
                // タスクが無くなり次第終了
                if (this.TaskQueue.Count.Equals(0))
                {
                    this.TaskEnd();
                    break;
                }
            }
        }

        /// <summary>
        /// タスク終了
        /// </summary>
        private void TaskEnd()
        {
            this.TaskCompleted = true;
            this.IsSkip = false;
            this.TaskQueue = new Queue<Func<IEnumerator>>();
        }
    }
}
