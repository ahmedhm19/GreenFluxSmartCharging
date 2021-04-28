using GreenFlux.DataAccess.Base;
using GreenFlux.DataAccess.Exceptions;
using GreenFlux.Model;
using GreenFlux.Service.Base;
using GreenFlux.Service.Base.Validators;
using GreenFlux.Service.Tools;
using System.Collections.Generic;

namespace GreenFlux.Service
{
    public class ChargeStationService : IChargeStationService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IConnectorKeyGenerator _ConnectorKeyGenerator;
        private readonly IChargeStationValidator _ChargeStationValidator;


        public ChargeStationService(IUnitOfWork unitOfWork, IConnectorKeyGenerator connectorKeyGenerator, IChargeStationValidator chargeStationValidator)
        {
            _UnitOfWork = unitOfWork;
            _ConnectorKeyGenerator = connectorKeyGenerator;
            _ChargeStationValidator = chargeStationValidator;
        }

        public ChargeStation GetChargeStation(long id)
        {
            return _UnitOfWork.ChargeStationRepository.GetById(id);
        }

        public ChargeStation CreateChargeStation(ChargeStation chargeStation , List<Connector> connectors)
        {

            ThrowIf.Argument.IsNull(chargeStation, nameof(chargeStation));
            ThrowIf.Argument.IsNull(connectors, nameof(connectors));

            _ChargeStationValidator.ValidateCreate(chargeStation, connectors);

            _UnitOfWork.ChargeStationRepository.Add(chargeStation);

            //Load connectors from given collection
            chargeStation.Connectors = connectors;
            
            //Generate keys
            foreach (var item in chargeStation.Connectors)
            {
                item.CK_Id = _ConnectorKeyGenerator.GenerateKey(chargeStation.Connectors);
            }          

            //Save
            _UnitOfWork.SaveChanges();

            return chargeStation;
        }

        public ChargeStation UpdateChargeStation(ChargeStation chargeStation)
        {
            ThrowIf.Argument.IsNull(chargeStation, nameof(chargeStation));

            if (!_UnitOfWork.ChargeStationRepository.Exists(chargeStation))
                throw new NotFoundException();

            //Always Ignore changes on connectors
            chargeStation.Connectors = null;

            //Update and ignore groupId change
            _UnitOfWork.ChargeStationRepository.Update(chargeStation, nameof(chargeStation.GroupId));

            _UnitOfWork.SaveChanges();

            return chargeStation;
        }

        public void DeleteChargeStation(long id)
        {
            var chargeStation = GetChargeStation(id);
            if (chargeStation == null)
                throw new NotFoundException();

            _UnitOfWork.ChargeStationRepository.Delete(chargeStation);
            _UnitOfWork.SaveChanges();
        }

    }
}
