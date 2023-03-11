using CommonLayer;
using MassTransit;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TicketConsumer.Services
{
    public class UserTicketConsumer:IConsumer<UserTicketModel>
    {
        public async Task Consume(ConsumeContext<UserTicketModel> context)
        {
            var data = context.Message;
            //Validate the Ticket Data
            //Store to Database
            //Notify the user via Email / SMS
        }
    }
}
