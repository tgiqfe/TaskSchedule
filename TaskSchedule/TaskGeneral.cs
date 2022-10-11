using TaskScheduler;

namespace TaskSchedule
{
    internal class TaskGeneral
    {
        /// <summary>
        /// 作成者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// タスク実行時に使うユーザーアカウント
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ユーザーがログオンしているときのみ実行する
        /// </summary>
        public bool? RunOnlyWhenUserLoggedOn { get; set; }

        /// <summary>
        /// パスワードを保存しない
        /// </summary>
        public bool? DoNotSavePassword { get; set; }

        /// <summary>
        /// 最上位特権で実行する
        /// </summary>
        public bool? RunWithHighest { get; set; }

        /// <summary>
        /// 表示しない
        /// </summary>
        public bool? Hidden { get; set; }
    }
}
