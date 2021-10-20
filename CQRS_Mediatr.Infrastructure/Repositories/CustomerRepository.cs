using CQRS_Mediatr.Domain.Entities;
using CQRS_Mediatr.Domain.Interfaces;
using CQRS_Mediatr.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        protected readonly AppDbContext appDbContext;

        public CustomerRepository(AppDbContext context)
        {
            appDbContext = context;
        }

        public void Add(Customer customer)
        {
            appDbContext.Customers.Add(customer);
            appDbContext.SaveChanges();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await appDbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetByEmail(string email)
        {
            return await appDbContext.Customers.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Customer> GetById(int customerId)
        {
            return await appDbContext.Customers.FindAsync(customerId);
        }

        public void Remove(Customer customer)
        {
            appDbContext.Customers.Remove(customer);
            appDbContext.SaveChanges();
        }

        public void Update(Customer customer)
        {
            appDbContext.Customers.Update(customer);
            appDbContext.SaveChanges();
        }

        public void Dispose()
        {
            appDbContext.Dispose();
        }
    }
}
