using Hotel.Business.Contracts.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Business.Services.Interfaces
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
