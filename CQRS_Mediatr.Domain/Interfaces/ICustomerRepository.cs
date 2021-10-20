using CQRS_Mediatr.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetById(int customerId);
        Task<Customer> GetByEmail(string email);
        Task<IEnumerable<Customer>> GetAll();
        void Add(Customer customer);
        void Update(Customer customer);
        void Remove(Customer customer);

    }
}
