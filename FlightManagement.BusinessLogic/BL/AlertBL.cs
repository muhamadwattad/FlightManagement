using FlightManagement.BusinessLogic.BL.DTOs;
using FlightManagement.DataAccessLayer.Entities;
using FlightManagement.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.BusinessLogic.BL
{
    public class AlertBL
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlertBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> CreateAlert(AlertDTOs.Create dto)
        {
            Alert alert = new Alert(dto.UserId, dto.AirportOrigin, dto.AirportDestination, dto.Price);
            await _unitOfWork.Repository<Alert>().AddAsync(alert);
            await _unitOfWork.Commit();
            return alert.Id;
        }
        public async Task<Guid> Update(AlertDTOs.Update dto)
        {
            var alert = await _unitOfWork.Repository<Alert>().GetByIdAsync(dto.Id);
            alert.AirportOrigin = dto.AirportOrigin;
            alert.AirportDestination = dto.AirportDestination;
            alert.Price = dto.Price;
            await _unitOfWork.Repository<Alert>().UpdateAsync(alert);
            await _unitOfWork.Commit();

            return alert.Id;
        }

        public async Task Delete(Guid id)
        {
            var alert = await _unitOfWork.Repository<Alert>().GetByIdAsync(id);
            alert.Active = false;
            await _unitOfWork.Repository<Alert>().UpdateAsync(alert);
            await _unitOfWork.Commit();
        }

        public async Task<List<Alert>> Get(Guid userId) =>
            await _unitOfWork.Repository<Alert>().GetAsync<Alert>(s => s.UserId == userId && s.Active);

    }
}
