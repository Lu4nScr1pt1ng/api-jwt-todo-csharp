using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler : 
        IHandler<CreateTodoCommand>, 
        IHandler<UpdateTodoCommand>,
        IHandler<MarkTodoAsDoneCommand>,
        IHandler<MarkTodoAsUndoneCommand>
    {
        private readonly ITodoRepository _repository;
        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateTodoCommand command)
        {
            bool valid = command.Validate();
            if(!valid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", new { message = "error" });

            var todo = new TodoItem(command.Title!, command.User!, command.Date!);

            _repository.Create(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(UpdateTodoCommand command)
        {
            bool valid = command.Validate();
            if(!valid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", new { message = "error" });


            // Recuperar dado do banco (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User!);

            todo.UpdateTitle(command.Title!);

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            bool valid = command.Validate();
            if(!valid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", new { message = "error" });


            // Recuperar dado do banco (Rehidratação)
            try {
                var todo = _repository.GetById(command.Id, command.User!);

                todo.MarkAsDone();

                _repository.Update(todo);

                return new GenericCommandResult(true, "Tarefa salva", todo);
            }
            catch (Exception)
            {
                return new GenericCommandResult(false, "Ocorreu um erro ao salvar essa tarefa", new { message = "error" });
            }

        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            bool valid = command.Validate();
            if(!valid)
                return new GenericCommandResult(false, "Ops, parece que sua tarefa está errada!", new { message = "error" });


            // Recuperar dado do banco (Rehidratação)
            var todo = _repository.GetById(command.Id, command.User!);

            todo.MarkAsUndone();

            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}