using AutoMapper;
using GreenFlux.API.Models;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service;
using GreenFlux.Service.Base;
using GreenFlux.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.ComponentModel.DataAnnotations;

namespace GreenFlux.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private readonly IGroupService _GroupService;
        private readonly IMapper _Mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _GroupService = groupService;
            _Mapper = mapper;
        }

        [HttpGet(nameof(Get) + "/{id:long}")]
        public ActionResult<GroupModel> Get(long id)
        {
            try
            {
                var group = _GroupService.GetGroup(id);
               
                if (group == null) return NotFound();

                return _Mapper.Map<GroupModel>(group);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(nameof(Create))]
        public ActionResult<GroupModel> Create(GroupModel groupModel)
        {
            try
            {
                if (groupModel.Id > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Providing custom Id is not allowed, Group.Id is Auto-Generated !");


                var _group = _GroupService.CreateGroup(_Mapper.Map<Group>(groupModel));
                return Ok(_Mapper.Map<GroupModel>(_group));

            }
            catch (DataException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                //Log exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut(nameof(Update))]
        public ActionResult<GroupModel> Update(GroupModel groupModel)
        {
            try
            {
                var _group = _GroupService.UpdateGroup(_Mapper.Map<Group>(groupModel));
                return Ok(_Mapper.Map<GroupModel>(_group));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DataException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest , ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                //Log exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete(nameof(Delete) + "/{id:long}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _GroupService.DeleteGroup(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DataException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception)
            {
                //Log exception
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
