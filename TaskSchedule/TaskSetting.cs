﻿using TaskScheduler;

namespace TaskSchedule
{
    internal class TaskSetting
    {
        /// <summary>
        /// タスク要求時に実行する
        /// </summary>
        public bool? AllowDemandStart { get; set; }

        /// <summary>
        /// スケジュールされた時刻にタスクを開始できなかった場合、すぐにタスクを実行する
        /// </summary>
        public bool? StartWhenAvailable { get; set; }

        /// <summary>
        /// タスクが失敗した場合の再起動の間隔
        /// </summary>
        public TimeSpan? RestartInterval { get; set; }

        /// <summary>
        /// 再起動試行の最大数
        /// </summary>
        public int? RestartCoutn { get; set; }

        /// <summary>
        /// タスクを停止するまでの時間
        /// </summary>
        public TimeSpan? ExecutionTimeLimit { get; set; }

        /// <summary>
        /// 要求時に実行中のタスクが終了しえちない場合に削除されるまでの時間
        /// </summary>
        public bool? AllowHardTerminate { get; set; }

        /// <summary>
        /// タスクの再実行がスケジュールされていない場合に削除されるまでの時間
        /// </summary>
        public TimeSpan? DeleteExpiredTaskAfter { get; set; }

        /// <summary>
        /// タスクが既に実行中の場合に適用される規則
        /// </summary>
        public enum TaskInstancePolicy
        {
            IgnoreNew,      //  新しいインスタンスを開始しない
            Parallel,       //  新しいインスタンスを並列で実行
            Queue,          //  新しいインスタンスをキューに追加
            StopExisting,   //  既存のインスタンスの停止
        }
        public TaskInstancePolicy MultipleInstances { get; set; }


    }
}
