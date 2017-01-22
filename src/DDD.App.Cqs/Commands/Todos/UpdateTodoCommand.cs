using DDD.Common.Cqs.Command;
using DDD.Domain.Todos;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DDD.App.Cqs.Commands.Todos
{
    public class UpdateTodoCommand : ICommand, IValidatableObject
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var validator = new UpdateTodoCommandValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new System.ComponentModel.DataAnnotations.ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));

        }
    }

    public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
    {
        public UpdateTodoCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.Description).NotEmpty();
        }
    }

    public static class UpdateTodoFactory
    {
        public static Todo CreateFrom(UpdateTodoCommand command, Todo todo)
        {
            todo.ChangeDescrition(command.Description);
            return todo;
        }
    }

}
