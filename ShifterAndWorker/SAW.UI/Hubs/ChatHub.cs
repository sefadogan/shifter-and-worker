using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SAW.UI.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string message, string name)
        {
            Clients.All.sendMessage(name, message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();

            // Get UserID. Assumed the user is logged before connecting to chat and userid is saved in session.
            //string UserID = (string)HttpContext.Current.Session["userid"];

            //// Get ChatHistory and call the client function. See below
            //this.GetHistory(UserID);

            //// Get ConnID
            //string ConnID = Context.ConnectionId;

            // Save them in DB. You got to create a DB class to handle this. (Optional)
            //DB.UpdateConnID(UserID, ConnID);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }



        //private void GetHistory(UserID)
        //{
        //    // Get Chat History from DB. You got to create a DB class to handle this.
        //    string History = DB.GetChatHistory(UserID);

        //    // Send Chat History to Client. You got to create chatHistory handler in Client side.
        //    Clients.Caller.chatHistory(History);
        //}

        //// This method is to be called by Client 
        //public void Chat(string Message)
        //{
        //    // Get UserID. Assumed the user is logged before connecting to chat and userid is saved in session.
        //    string UserID = (string)HttpContext.Current.Session["userid"];

        //    // Save Chat in DB. You got to create a DB class to handle this
        //    DB.SaveChat(UserID, Message);

        //    // Send Chat Message to All connected Clients. You got to create chatMessage handler in Client side.
        //    Clients.All.chatMessage(Message);
        //}

    }
}