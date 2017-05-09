using System;
//using System.Threading.Tasks;
namespace AloneWar.Common.TaskHelper
{
    /// <summary>
    /// 非同期実行するタスクのヘルパークラス
    /// 非同期処理を1箇所に集めるだけ
    /// </summary>
    public class AsyncTaskHelper
    {
        /// <summary>
        /// タスク名
        /// </summary>
        public string TaskTitle { get; set; }

        /// <summary>
        /// 開始
        /// </summary>
        public Action StartTask { get; set; }

        /// <summary>
        /// 非同期で実行するタスク
        /// </summary>
        public Action MainTask { get; set; }

        /// <summary>
        /// 終了
        /// </summary>
        public Action EndTask { get; set; }

        #region コンストラクタ

        public AsyncTaskHelper()
        {
            this.TaskTitle = string.Empty;
        }

        public AsyncTaskHelper(Action mainTask):this()
        {
            this.MainTask = mainTask;
        }

        public AsyncTaskHelper(Action startTask, Action mainTask):this()
        {
            this.StartTask = startTask;
            this.MainTask = mainTask;
        }

        public AsyncTaskHelper(Action startTask, Action mainTask, Action endTask)
            : this()
        {
            this.StartTask = startTask;
            this.MainTask = mainTask;
            this.EndTask = endTask;
        }

        #endregion

        /// <summary>
        /// 開始~メインタスク実行
        /// </summary>
        //public void TaskRun()
        //{
        //    this.StartTask.SafeCall();
        //    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //    sw.Start();
        //    Task.Factory.StartNew(this.MainTask);
        //    sw.Stop();
        //    //Debug.Log(string.Format("{0}:{1}", this.TaskTitle, sw.ElapsedMilliseconds));
        //}

        ///// <summary>
        ///// 開始~終了タスク実行
        ///// </summary>
        //async public void TaskRunAwait()
        //{
        //    this.StartTask.SafeCall();
        //    await Task.Run(() =>
        //    {
        //        this.MainTask.SafeCall();
        //    });
        //    this.EndTask.SafeCall();
        //}

        //async public Task<T> TaskRunAwait<T>(Func<T> function)
        //{
        //    this.StartTask.SafeCall();
        //    //Task<T> task = new Task<T>(function);
        //    //task.Start();
        //    T result = await Task<T>.Run(() =>
        //    {
        //        return function();
        //    });
        //    return result;
        //}
    }
}
