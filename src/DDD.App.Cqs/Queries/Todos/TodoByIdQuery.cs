using DDD.App.Cqs.QueryResult.Todos;
using DDD.Core.Cqs.Query;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDD.App.Cqs.Queries.Todos
{
    public class TodoByIdQuery: IQuery<TodoResult>, IValidatableObject
    {
        
        public string Id { get; set; }


        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var validator = new TodoByIdQueryValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));

        }
    }

    public class TodoByIdQueryValidator: AbstractValidator<TodoByIdQuery>
    {
        public TodoByIdQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}
