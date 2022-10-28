using Todo.Domain.Commands;

namespace Todo.Domain.Tests.CommandTests
{

    [TestClass]
    public class CreateTodoCommandTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titulo da tarefa", "Luan", DateTime.Now);
        [TestMethod]
        public void Dado_um_comando_invalido()
        {
            bool valid = _invalidCommand.Validate();
            Assert.AreEqual(valid, false);
        }

        [TestMethod]
        public void Dado_um_comando_invalido1()
        {
            bool valid = _invalidCommand.Validate();
            Assert.AreNotEqual(valid, true);
        }

        [TestMethod]
        public void Dado_um_comando_valido()
        {
            bool valid = _validCommand.Validate();
            Assert.AreEqual(valid, true);
        }
    }
}