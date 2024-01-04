using Microsoft.AspNetCore.Mvc;
using UnitTests.Common;
using UnitTests.Services;

namespace UnitTests.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return _clientService.GetClients();
        }
    }
}
