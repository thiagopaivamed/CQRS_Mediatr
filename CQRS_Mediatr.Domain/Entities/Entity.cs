using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Mediatr.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }

        [NotMapped]
        public bool Valid { get; private set; }
        public bool Invalid => !this.Valid;

        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            this.ValidationResult = validator.Validate(model);
            return this.Valid = this.ValidationResult.IsValid;
        }
    }
}
