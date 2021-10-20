using CQRS_Mediatr.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS_Mediatr.Domain.Entities
{
    public class Customer : Entity
    {        

        public string Name { get; set; }

        public string Email { get; set; }

        public Customer()
        {

        }

        public Customer(string name, string email)
        {
            Name = name;
            Email = email;

            this.Validate(this, new CustomerValidation());
        }
    }
}
