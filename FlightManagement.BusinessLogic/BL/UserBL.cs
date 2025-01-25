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
    public class UserBL
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserBL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Delete(Guid id)
        {

            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            user.Active = false;
            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
        }

        public async Task<Guid> Create(UserDTOs.CreateUser dto)
        {
            User user = new User(dto.FullName);
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.Commit();

            return user.Id;
        }
        public async Task<Guid> Update(UserDTOs.UpdateUser dto)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(dto.Id);
            user.FullName = dto.FullName;

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.Commit();
            return user.Id;
        }

        public async Task<List<User>> Get() => await _unitOfWork.Repository<User>().GetAsync<User>(s => s.Active);
    }
}
