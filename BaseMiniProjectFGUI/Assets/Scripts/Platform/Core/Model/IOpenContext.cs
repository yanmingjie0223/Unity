using System.Collections.Generic;

namespace Assets.Scripts.Platform
{

    public interface IOpenContext
    {

        /// <summary>
        /// 展示开放域
        /// </summary>
        void Show();

        /// <summary>
        /// 隐藏开放域
        /// </summary>
        void Hide();

        /// <summary>
        /// 发送开放域显示好友信息
        /// </summary>
        void SendFriendMessage();

        /// <summary>
        /// 发送开放域显示群好友信息
        /// </summary>
        void SendGroupFriendMessage();

        /// <summary>
        /// 发送开放域显示世界排名信息
        /// </summary>
        /// <param name="ranks"></param>
        void SendWorldMessage(List<Msg.User.RankData> ranks);

        /// <summary>
        /// 发送开放域记录信息
        /// </summary>
        /// <param name="record"></param>
        void SendRecord(int record);

        /// <summary>
        /// 发送用户数据到开放域
        /// </summary>
        void SendUserData();

    }

}
