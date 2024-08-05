﻿using BLL.Exceptions;
using BLL;
using ENTITIES.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SLC;
using System;
using DAL;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase , IProductService
    {
        private readonly Products _bll; // Dependency injection for better testability

        public ProductController(Products bll)
        {
            _bll = bll;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            try
            {
                var result = await _bll.RetrieveAllAsync();
                return Ok(result); // Use IActionResult for more flexibility (200 OK)
            }
            catch (ProductExceptions ex) // Catch specific business logic exceptions
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with error message
            }
            catch (Exception ex) // Catch unhandled exceptions for logging and generic error response
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}", Name = "RetrieveProductAsync")]
        public async Task<ActionResult<Product>>RetrieveAsync(int id)
        {
            try
            {
                var product = await _bll.RetrieveByIDAsync(id);

                if (product == null)
                {
                    return NotFound("Product not found."); // Use NotFound result for missing resources
                }
                return Ok(product);
            }
            catch (ProductExceptions ce)
            {
                return BadRequest(ce.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        // POST: api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateAsync([FromBody] Product toCreate)
        {
            try
            {
                var product = await _bll.CreateAsync(toCreate);
                return CreatedAtRoute("RetrieveProductAsync", new { id = product.Id }, product); // Use CreatedAtRoute for 201 Created
            }
            catch (ProductExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<Product>> CreateAsync([FromBody] Product toCreate)
        //{
        //    try
        //    {
        //        using (var repository = RepositoryFactory.CreateRepository())
        //        {
        //            // Buscar el Supplier existente por el supplierId
        //            var existingSupplier = await repository.RetrieveAsync<Supplier>(s => s.Id == toCreate.SupplierId);
        //            if (existingSupplier == null)
        //            {
        //                return BadRequest("Supplier not found.");
        //            }
        //            // Asignar el Supplier encontrado al Product
        //            toCreate.Supplier = existingSupplier;
        //        }

        //        // Crear el Product utilizando la lógica de negocio
        //        var product = await _bll.CreateAsync(toCreate);
        //        return CreatedAtRoute("RetrieveProductAsync", new { id = product.Id }, product);
        //    }
        //    catch (ProductExceptions ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        //    }
        //}



        // PUT: api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product toUpdate)
        {
            toUpdate.Id = id;
            try
            {
                var result = await _bll.UpdateAsync(toUpdate);
                if (!result)
                {
                    return NotFound("Product not found or update failed."); // Informative message for unsuccessful update
                }
                return NoContent(); // Use NoContent for successful updates with no content to return
            }
            catch (ProductExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        // DELETE: api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _bll.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Product not found or deletion failed."); // Informative message for unsuccessful deletion
                }
                return NoContent(); // Use NoContent for successful deletions with no content to return
            }
            catch (ProductExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
