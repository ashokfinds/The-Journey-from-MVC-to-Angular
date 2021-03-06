﻿using PTC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTC.Controllers_Apî
{
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {        
        [HttpGet]        
        public IHttpActionResult Get() {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            //throw new ApplicationException("Error in the Get() method");
            vm.Get();
            if(vm.Products.Count > 0) {
                ret = Ok(vm.Products);
            } else {
                ret = NotFound();
            }

            return ret;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();
            
            var product = vm.Get(id);
            if (product != null) {
                ret = Ok(product);
            } else {
                ret = NotFound();
            }

            return ret;
        }

        [HttpPost]        
        public IHttpActionResult Post(Product product)
        {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            vm.Entity = product;
            vm.PageMode = PageConstants.ADD;
            vm.Save();

            if (vm.IsValid) {
                ret = Created(Request.RequestUri + product.ProductId.ToString(), product);
            } else {
                ret = NotFound();
            }

            return ret;
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, Product product) {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            vm.Entity = product;
            vm.PageMode = PageConstants.EDIT;
            vm.Save();

            if (vm.IsValid) {
                ret = Ok(product);
            } else {
                ret = NotFound();
            }

            return ret;
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            vm.Entity = vm.Get(id);            

            if (vm.Entity.ProductId > 0) {
                vm.Delete(id);
                ret = Ok();
            } else {
                ret = NotFound();
            }

            return ret;
        }

        [HttpPost]
        [Route("Search")]
        public IHttpActionResult Search(ProductSearch searchEntity) {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            vm.SearchEntity = searchEntity;
            vm.Search();
            if (vm.Products.Count > 0) {
                ret = Ok(vm.Products);
            } else {
                ret = NotFound();
            }

            return ret;
        }
    }
}