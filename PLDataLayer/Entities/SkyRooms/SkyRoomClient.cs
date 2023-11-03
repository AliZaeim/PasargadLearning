

namespace PLDataLayer.Entities.SkyRooms
{
    public class SkyRoomClient
    {
        public static readonly string VERSION = "1.1";

        public static readonly int ROOM_STATUS_DISABLED = 0;
        public static readonly int ROOM_STATUS_ENABLED = 1;

        public static readonly int USER_STATUS_DISABLED = 0;
        public static readonly int USER_STATUS_ENABLED = 1;

        public static readonly int USER_GENDER_UNKNOWN = 0;
        public static readonly int USER_GENDER_MALE = 1;
        public static readonly int USER_GENDER_FEMALE = 2;

        /// <summary>
        /// کاربر عادی
        /// </summary>
        public static readonly int USER_ACCESS_NORMAL = 1;
        /// <summary>
        /// ارایه کننده
        /// </summary>
        public static readonly int USER_ACCESS_PRESENTER = 2;
        /// <summary>
        /// اپراتور
        /// </summary>
        public static readonly int USER_ACCESS_OPERATOR = 3;
        /// <summary>
        /// مدیر
        /// </summary>
        public static readonly int USER_ACCESS_ADMIN = 4;
    }
}
