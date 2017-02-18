using DDD.Core.Cqs.Command;
using DDD.Domain.Todos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDD.App.Cqs.Commands.Todos
{
    public class CreateTodoCommand : ICommand, IValidatableObject
    {
        public string Description { get; set; }
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var validator = new CreateTodoCommandValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new System.ComponentModel.DataAnnotations.ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));

        }
    }

    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(command => command.Description).NotEmpty();
        }
    }

    public static class CreateTodoFactory
    {
        public static Todo Create(CreateTodoCommand command)
        {
            var todo = new Todo(
                new TodoId(Guid.NewGuid().ToString()),
                command.Description);
            return todo;
        }
    }

}
