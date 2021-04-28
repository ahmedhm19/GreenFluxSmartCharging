using AutoMapper;
using GreenFlux.API.Models;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base;
using GreenFlux.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GreenFlux.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ChargeStationController : ControllerBase
    {

        private readonly IChargeStationService _ChargeStationService;
        private readonly IConnectorService _ConnectorService;
        private readonly IMapper _Mapper;

        public ChargeStationController(IChargeStationService chargeStationService, IConnectorService connectorService, IMapper mapper)
        {
            _ChargeStationService = chargeStationService;
            _ConnectorService = connectorService;
            _Mapper = mapper;
        }



        [HttpGet(nameof(Get)+ "/{id:long}/{includeConnectors?}")]
        public ActionResult<ChargeStationModel> Get(long id , bool includeConnectors = false)
        {
            try
            {
                var chargeStation = _ChargeStationService.GetChargeStation(id);
               
                if (chargeStation == null) return NotFound();

                var chargeStationModel = _Mapper.Map<ChargeStationModel>(chargeStation);

                if (includeConnectors)
                    chargeStationModel.Connectors = _Mapper.Map<List<ConnectorModel>>( _ConnectorService.GetConnectorsByChargeStation(id));

                return chargeStationModel;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(nameof(Create))]
        public ActionResult<ChargeStationModel> Create(ChargeStationModel chargeStationModel)
        {
            try
            {
                if (chargeStationModel.Id > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Providing custom Id is not allowed, ChargeStation.Id is Auto-Generated !");


                var chargeStation = _Mapper.Map<ChargeStation>(chargeStationModel);
                var connectors = _Mapper.Map<List<Connector>>(chargeStationModel.Connectors);

                chargeStation = _ChargeStationService.CreateChargeStation(chargeStation , connectors);
            
                return Ok(_Mapper.Map<ChargeStationModel>(chargeStation));
            }
            catch(DataException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(DomainValidationException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            //catch (Exception)
            //{
            //    //Log exception
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
        }

        [HttpPut(nameof(Update))]
        public ActionResult<ChargeStationModel> Update(ChargeStationModel chargeStationModel)
        {
            try
            {
                //Map to model
                var chargeStation = _Mapper.Map<ChargeStation>(chargeStationModel);
                //update 
                chargeStation = _ChargeStationService.UpdateChargeStation(chargeStation);
                //map back to DTO
                chargeStationModel = _Mapper.Map<ChargeStationModel>(chargeStation);

                return Ok(chargeStationModel);
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
                _ChargeStationService.DeleteChargeStation(id);
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
