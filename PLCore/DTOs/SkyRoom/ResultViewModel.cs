using PLDataLayer.Entities.SkyRooms;
using PLDataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.SkyRoom
{
    public class ResultViewModel
    {
        public int? room_id { get; set; }
        public int? user_id { get; set; }

        public PLDataLayer.Entities.User.User InsUser { get; set; }
        public Role InsRole { get; set; }
        public UserRole InsUserRole { get; set; }

        public string OperationName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
