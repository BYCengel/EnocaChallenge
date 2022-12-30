using EnocaChallenge.Application.Repositories.CompanyRep;
using EnocaChallenge.Application.Repositories.OrderRep;
using EnocaChallenge.Application.Repositories.ProductRep;
using EnocaChallenge.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EnocaChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        //Dependency Injection
        private readonly ICompanyReadRepository _companyReadRepository;
        private readonly ICompanyWriteRepository _companyWriteRepository;

        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;

        public APIController(ICompanyReadRepository companyReadRepository, ICompanyWriteRepository companyWriteRepository,
            IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository,
            IOrderReadRepository orderReadRepository, IOrderWriteRepository orderWriteRepository)
        {
            _companyReadRepository = companyReadRepository;
            _companyWriteRepository = companyWriteRepository;

            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;

            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        //HttpGet functions

        [HttpGet("/GetCompany/{id}")]
        public async Task<IActionResult> GetCompanyWithId(int id)
        {
            Company company = await _companyReadRepository.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound("There is no company with this id.");
            }

            return Ok(company);
        }

        [HttpGet("/GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            return Ok(_companyReadRepository.GetAll());
        }

        //HttpPost Functions

        [HttpPost("/CreateCompany/{name}/{startTimeString}/{endTimeString}/{isApproved}")]
        public async Task<IActionResult> CreateCompany(string name, string startTimeString, string endTimeString, bool isApproved)
        {
            DateTime orderStartTime = ConvertHoursToDateTime(startTimeString);
            DateTime orderEndTime = ConvertHoursToDateTime(endTimeString);

            if ((orderEndTime.TimeOfDay - orderStartTime.TimeOfDay).TotalSeconds < 0)
            {
                return Conflict("start time needs to be earlier than end time.");
            }

            Company company = new()
            {
                Name = name,
                IsApproved = isApproved,
                OrderStartTime = orderStartTime,
                OrderEndTime = orderEndTime
            };
            await _companyWriteRepository.AddAsync(company);
            return Ok(company);
        }

        [HttpPost("/CreateProduct/{name}/{stock}/{price}/{companyId}")]
        public async Task<IActionResult> CreateProduct(string name, int stock, int price, int companyId)
        {
            Product product = new()
            {
                Name = name,
                Stock = stock,
                Price = price,
                CompanyId = companyId
            };
            product.Company = await _companyReadRepository.GetByIdAsync(companyId);
            if(product.Company == null)
            {
                return NotFound("There is no company with this id.");
            }
            await _productWriteRepository.AddAsync(product);
            return Ok(product);
        }

        [HttpPost("/CreateOrder/{companyId}/{productId}/{name}")]
        public async Task<IActionResult> CreateOrder(int companyId, int productId, string name)
        {
            Company company = await _companyReadRepository.GetByIdAsync(companyId);

            if(company == null)
            {
                return NotFound("There is no company with this id.");
            }

            if (!company.IsApproved)
            {
                return Conflict("Company not approved");
            }

            Product product = await _productReadRepository.GetByIdAsync(productId);

            if(product == null)
            {
                return NotFound("There is no product with this id.");
            }

            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            if((currentTime - company.OrderStartTime.TimeOfDay).TotalSeconds <= 0 || (company.OrderEndTime.TimeOfDay - currentTime).TotalSeconds <= 0)
            {
                return Conflict("Out of company order time.");
            }

            Order order = new()
            {
                CompanyId = companyId,
                ProductId = productId,
                Name = name,
                OrderDate = DateTime.Now,
                Product = product,
                Company = company
            };

            await _orderWriteRepository.AddAsync(order);

            return Ok(order);
        }

        //HttpPut functions
        
        [HttpPut("/UpdateIsApproved/{id}/{isApproved}")]
        public async Task<IActionResult> UpdateIsApproved(int id, bool isApproved)
        {
            Company company = await _companyReadRepository.GetByIdAsync(id);

            if (company == null)
            {
                return NotFound("There is no company with this id.");
            }

            _companyWriteRepository.UpdateIsApproved(company, isApproved);
            return Ok(company);
        }

        [HttpPut("/UpdateBusinessHours/{id}/{startTimeString}/{endTimeString}")]
        public async Task<IActionResult> UpdateBusinessHours(int id, string startTimeString, string endTimeString)
        {
            Company company = await _companyReadRepository.GetByIdAsync(id);

            if(company == null)
            {
                return NotFound("There is no company with this id.");
            }

            DateTime OrderStartTime = ConvertHoursToDateTime(startTimeString);
            DateTime OrderEndTime = ConvertHoursToDateTime(endTimeString);

            if ((OrderEndTime.TimeOfDay - OrderStartTime.TimeOfDay).TotalSeconds < 0)
            {
                return Conflict("start time needs to be earlier than end time.");
            }

            _companyWriteRepository.UpdateBusinessHours(company, OrderStartTime, OrderEndTime);
            return Ok();
        }

        //Assistant Functions

        /// <summary>
        /// Converts the given hours to DateTime together with todays date.
        /// </summary>
        /// <param name="timeString"></param>
        /// <returns></returns>
        private DateTime ConvertHoursToDateTime(string timeString)
        {
            TimeOnly timeOnly = TimeOnly.Parse(timeString);
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, now.Day, timeOnly.Hour, timeOnly.Minute, 0);
        }
    }
}
