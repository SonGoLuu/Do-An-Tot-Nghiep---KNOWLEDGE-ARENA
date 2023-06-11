using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An_Tot_Nghiep
{
    public class ConnectedUser
    {
        public static List<UserAndRoom> Ids = new List<UserAndRoom>();
        public static string GetUser(string username)
        {
            return Ids.FirstOrDefault(x => x.UserName == username)?.UserName;
        }
        public static string GetCurrenRoom(string username)
        {
            return Ids.FirstOrDefault(x => x.UserName == username)?.CurrentRoom;
        }
        public static string GetConnectionIdOfUser(string username)
        {
            return Ids.FirstOrDefault(x => x.UserName == username)?.ConnectionId;
        }
        public static void UpdateCurrentRoom(string username, string newRoom)
        {
            var userAndRoom = Ids.FirstOrDefault(x => x.UserName == username);

            if (userAndRoom != null)
            {
                userAndRoom.CurrentRoom = newRoom;
            }
        }
        public static void UpdateConnectionId(string username, string newConnectionId)
        {
            var userAndRoom = Ids.FirstOrDefault(x => x.UserName == username);

            if (userAndRoom != null)
            {
                userAndRoom.ConnectionId = newConnectionId;
            }
        }
    }

}
