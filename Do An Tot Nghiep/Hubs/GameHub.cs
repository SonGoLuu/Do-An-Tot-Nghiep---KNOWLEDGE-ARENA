using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Tasks;



namespace Do_An_Tot_Nghiep.Hubs
{
    public class GameHub : Hub
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        public GameHub(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        //public async Task CreateRoom(string roomName, string userName)
        //{
        //    // Tạo một phòng mới và đưa người dùng vào phòng đó
        //    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        //    // Thông báo cho các người dùng khác trong phòng biết rằng có một người dùng mới gia nhập phòng
        //    //await Clients.Group(roomName).SendAsync("UserJoined", userName);
        //    await Clients.All.SendAsync("ReceiveMessage", roomName, userName);
        //}

        //public async Task JoinRoom(string roomName)
        //{
        //    // Đưa người dùng vào phòng
        //    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

        //    // Thông báo cho người dùng mới biết danh sách các người dùng khác trong phòng
        //    //var userNames = await GetUsersInRoom(roomName);
        //    //await Clients.Caller.SendAsync("UsersInRoom", userNames);


        //    // Thông báo cho các người dùng khác trong phòng biết rằng có một người dùng mới gia nhập phòng
        //    await Clients.Group(roomName).SendAsync("UserJoined", Context.ConnectionId);
        //}

        //public async Task LeaveRoom(string roomName)
        //{
        //    // Rời khỏi phòng
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

        //    // Thông báo cho các người dùng khác trong phòng biết rằng có một người dùng rời khỏi phòng
        //    await Clients.Group(roomName).SendAsync("UserLeft", Context.ConnectionId);
        //}

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        public override async Task OnConnectedAsync()
        {
            string username = _userManager.GetUserName(Context.User);

            //test
            if(ConnectedUser.GetUser(username)==null)
            {
                UserAndRoom userroom = new UserAndRoom();
                userroom.UserName = username;
                userroom.CurrentRoom = "";
                userroom.ConnectionId = Context.ConnectionId;
                ConnectedUser.Ids.Add(userroom);
            }   
            else
            {
                ConnectedUser.UpdateConnectionId(username, Context.ConnectionId);
                string currentroom = ConnectedUser.GetCurrenRoom(username);
                if (currentroom != "")
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, currentroom);
                }
            }    

            
            await base.OnConnectedAsync();
        }

    }
}
