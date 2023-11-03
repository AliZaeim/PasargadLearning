using PLCore.DTOs.SkyRoom;
using PLDataLayer.Entities.SkyRooms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PLCore.Services.Interfaces
{
    public interface ISkyService
    {
        public Task<int?> CreateUserAsync(int? urId, string userNC);
        /// <summary>
        /// roomId = Teacher_Sky_roomId
        /// userId = Teacher_Sky_UserId
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddTeacherToRoom(int? roomId, int? userId);
        /// <summary>
        /// roomId = Sky_roomId
        /// userId = Sky_userId
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddStudentToRoom(int? roomId, int? userId);

        public Task<int?> CreateRoomAsync(int? urId);
        public string GetRoomLink(int? roomId);
        public List<PLDataLayer.Entities.SkyRooms.SkyUserViewModel> GetRoomUsers(int? roomId);
        public List<SkyRoomViewModel> GetRooms();
        public List<DTOs.SkyRoom.SkyUserViewModel> GetSkyUsers();
        public List<RoomReportViewModel> GetUserRooms(int? userId);
        public int RemoveStudentsFromRoom(int? roomId, List<int> users);
        public bool AddStudentsToRoom(int? roomId, List<int> Ids);
    }
}
