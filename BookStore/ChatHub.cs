
using Microsoft.AspNetCore.SignalR;

namespace BookStore
{
    public class ChatHub:Hub
    {
        public async void SendMessage(string name, string massage) 
        {
          await  Clients.All.SendAsync("NewMessge",name, massage);
            
        }
        public async void JoinToGroup(string GroupName,string username)
        {
            //add to group 
		  await	Groups.AddToGroupAsync(Context.ConnectionId,GroupName);
            //BroadCast
          var task1= Clients.OthersInGroup(GroupName).SendAsync("newmember",username,GroupName);
          var task2= Clients.Caller.SendAsync("message", username, GroupName);
          await Task.WhenAll(task1,task2);
		}


    }
}
