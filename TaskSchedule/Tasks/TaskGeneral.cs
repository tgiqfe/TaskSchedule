
namespace TaskSchedule.Tasks
{
    internal class TaskGeneral
    {
        /// <summary>
        /// タスクの名前
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// タスクの場所
        /// </summary>
        public string TaskPath { get; set; } = "\\";

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

        public _TASK_LOGON_TYPE GetLogonType()
        {
            if (RunOnlyWhenUserLoggedOn == null || RunOnlyWhenUserLoggedOn == true)
            {
                return _TASK_LOGON_TYPE.TASK_LOGON_INTERACTIVE_TOKEN;
            }
            if (DoNotSavePassword == null || DoNotSavePassword == false)
            {
                return _TASK_LOGON_TYPE.TASK_LOGON_S4U;
            }
            return _TASK_LOGON_TYPE.TASK_LOGON_PASSWORD;
        }
    }
}
