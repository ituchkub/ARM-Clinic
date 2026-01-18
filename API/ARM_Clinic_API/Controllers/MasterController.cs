using ARM_Clinic_API.Model;
using ARM_Clinic_API.Repo;
using ARM_Clinic_API_Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ARM_Clinic_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MasterController : ControllerBase
    {

        MasterRepo masterRepo = new MasterRepo();

        [HttpPost]
        [Route("BanchList")]
        public IActionResult CashAdvanceList([FromBody] RequestMessage<MASBanchList> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.BanchList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }


        [HttpPost]
        [Route("CustomerList")]
        public IActionResult CustomerList([FromBody] RequestMessage<MASCustomerList> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.CustomerList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }

        [HttpPost]
        [Route("ProductList")]
        public IActionResult ProductList([FromBody] RequestMessage<MASProductInfo> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.ProductList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }

        [HttpPost]
        [Route("ProductStockList")]
        public IActionResult ProductStockList([FromBody] RequestMessage<MASProductStock> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.ProductStockList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }


        [HttpPost]
        [Route("ServiceList")]
        public IActionResult ServiceList([FromBody] RequestMessage<MASServiceInfo> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.ServiceList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }


        [HttpPost]
        [Route("BuyProductList")]
        public IActionResult BuyProductList([FromBody] RequestMessage<List<BuyProduct>> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.BuyProductList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }

        [HttpPost]
        [Route("BuyServiceList")]
        public IActionResult BuyServiceList([FromBody] RequestMessage<List<BuyService>> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.BuyServiceList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }


        [HttpPost]
        [Route("UseServiceList")]
        public IActionResult UseServiceList([FromBody] RequestMessage<UseService> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.UseServiceList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }


        [HttpPost]
        [Route("EmployeeList")]
        public IActionResult EmployeeList([FromBody] RequestMessage<MASEmployeeList> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.EmployeeList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }

        [HttpPost]
        [Route("DDL")]
        public IActionResult DDL([FromBody] RequestMessage<M_DDL> request)
        {
            if (request != null)
            {
                return Ok(masterRepo.DDLGetList(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }
    }
}
