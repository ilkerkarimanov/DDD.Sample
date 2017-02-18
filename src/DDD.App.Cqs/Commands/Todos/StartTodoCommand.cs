using DDD.Core.Cqs.Command;
using DDD.Domain.Todos;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDD.App.Cqs.Commands.Todos
{
    public class StartTodoCommand : ICommand, IValidatableObject
    {
        public string Id { get; set; }
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var validator = new StartTodoCommandValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new System.ComponentModel.DataAnnotations.ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));

        }
    }

    public class StartTodoCommandValidator : AbstractValidator<StartTodoCommand>
    {
        public StartTodoCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
        }
    }

    public static class StartTodoFactory
    {
        public static TodoAction Create(StartTodoCommand command)
        {
            return new TodoAction(
                TodoState.InProgress
                );
        }
    }

}
