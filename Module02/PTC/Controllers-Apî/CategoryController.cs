using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTC.Controllers_Apî
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        [Route("GetSearchCategories")]
        public IHttpActionResult Get()
        {            
            var vm = new PTCViewModel();

            vm.LoadSearchCategories();
            if(vm.SearchCategories.Count > 0) {
                return Ok(vm.SearchCategories);
            } else {
                return NotFound();
            }
        }        
    }
}