using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTC.Controllers_Apî
{
    public class ProductController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            vm.Get();
            if(vm.Products.Count > 0) {
                ret = Ok(vm.Products);
            } else {
                ret = NotFound();
            }

            return ret;
        }        
    }
}