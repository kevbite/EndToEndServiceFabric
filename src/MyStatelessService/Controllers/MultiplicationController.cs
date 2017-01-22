using System.Web.Http;

namespace MyStatelessService.Controllers
{
    [ServiceRequestActionFilter]
    public class MultiplicationController : ApiController
    {
        public int Get(int a, int b)
        {
            return a*b;
        }
    }
}
