using AutoMapper;
using GreenFlux.API.Models;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base;
using GreenFlux.Service.Exceptions;
using GreenFlux.Service.Tools;
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
    public class ConnectorController : ControllerBase
    {


        private readonly IConnectorService _ConnectorService;
        private readonly IChargeStationService _ChargeStationService;
        private readonly IMapper _Mapper;
        private readonly ISuggester _Suggester;

        public ConnectorController(IConnectorService connectorService, IChargeStationService chargeStationService, IMapper mapper, ISuggester suggester)
        {
            _ConnectorService = connectorService;
            _ChargeStationService = chargeStationService;
            _Mapper = mapper;
            _Suggester = suggester;
        }



        [HttpGet(nameof(Get) + "/{ck_Id}/{chargeStationId:long}")]
        public ActionResult<ConnectorModel> Get(byte ck_Id , long chargeStationId)
        {
            try
            {
                var connector = _ConnectorService.GetConnector(ck_Id,chargeStationId);
               
                if (connector == null) return NotFound();

                return _Mapper.Map<ConnectorModel>(connector);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(nameof(Create))]
        public ActionResult Create(ConnectorModel connectorModel)
        {
            try
            {
                if (connectorModel.CK_Id > 0)
                    return StatusCode(StatusCodes.Status400BadRequest, "Providing custom CK_Id is not allowed, Connector.CK_Id is Auto-Generated !");



                var connector = _Mapper.Map<Connector>(connectorModel);
                connector = _ConnectorService.CreateConnector(connector);   
                return Ok(_Mapper.Map<ConnectorModel>(connector));
            }
            catch (ConnectorMaxCurrentExceedGroupCapacityException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (ConnectorsMaxCurrentExceedGroupCapacityException ex)
            {
                //get chargeStation
                var chargeStation = _ChargeStationService.GetChargeStation(connectorModel.ChargeStationId);
                //get connectors of group
                var connectorsOFGroup = _ConnectorService.GetConnectorsByGroup(chargeStation.GroupId);
                //get suggestions
                var suggestions = _Suggester.GetSuggestions(connectorsOFGroup, connectorModel.MaxCurrent);
                //Create suggestionResponse
                var response = new SuggestionResponse { ErrorMessage = ex.Message };
                response.Suggestions = _Mapper.Map<List<SuggestionModel>>(suggestions);

                return BadRequest(response);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut(nameof(Update))]
        public ActionResult<ConnectorModel> Update(ConnectorModel connectorModel)
        {
            try
            {
                //Map to model
                var connector = _Mapper.Map<Connector>(connectorModel);
                //update 
                connector = _ConnectorService.UpdateConnector(connector);
                //map back to DTO
                connectorModel = _Mapper.Map<ConnectorModel>(connector);

                return Ok(connectorModel);
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete(nameof(Delete) + "/{ck_Id}/{chargeStationId:long}")]
        public IActionResult Delete(byte ck_Id, long chargeStationId)
        {
            try
            {
                _ConnectorService.DeleteConnector(ck_Id , chargeStationId);
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
