using Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using webapi.Controllers;

namespace TestProject
{
    [TestClass]
    public class TarefasRepositoryUnitTest
    {
        #region CriarTarefa
        [TestMethod]
        public void CriarTarefa_ValidTarefa_ReturnsCreatedAtAction()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            var loggerMock = new Mock<ILogger<TarefaController>>();

            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);
            var newTarefa = new Tarefa { Data = DateTime.Today, Status = StatusTarefa.NaoIniciada, Descricao = "Lorem Ipsum" };

            tarefaRepositoryMock.Setup(repo => repo.Adicionar(newTarefa));

            // Act
            var result = controller.CriarTarefa(newTarefa);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdAtActionResult = (CreatedAtActionResult)result;
            Assert.AreEqual(nameof(TarefaController.ListarTarefas), createdAtActionResult.ActionName);
            Assert.AreEqual(newTarefa, createdAtActionResult.Value);

            tarefaRepositoryMock.Verify();
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [TestMethod]
        public void CriarTarefa_NullTarefa_ReturnsBadRequest()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            var loggerMock = new Mock<ILogger<TarefaController>>();

            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            tarefaRepositoryMock.Setup(repo => repo.Adicionar(null));

            // Act
            var result = controller.CriarTarefa(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));

        }
        #endregion

        [TestMethod]
        public void ListarTarefas_ReturnsOkWithTarefas()
        {
            // Setup
            var tarefas = new List<Tarefa> { new Tarefa(), new Tarefa() }; // Sample tasks
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterTodas()).Returns(tarefas);

            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.ListarTarefas();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(tarefas, okResult.Value);
        }

        [TestMethod]
        public void ObterTarefaPorId_ExistingTarefa_ReturnsOk()
        {
            // Setup
            var tarefaId = 1;
            var tarefa = new Tarefa { Id = tarefaId };
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterPorId(tarefaId)).Returns(tarefa);

            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.ObterTarefaPorId(tarefaId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(tarefa, okResult.Value);
        }

        [TestMethod]
        public void ObterTarefaPorId_NonexistentTarefa_ReturnsNotFound()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<int>())).Returns((Tarefa)null);

            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.ObterTarefaPorId(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void AtualizarTarefa_ValidTarefa_ReturnsOk()
        {
            // Arrange
            var tarefaId = 1;
            var existingTarefa = new Tarefa { Id = tarefaId }; // Existing task in the "database"
            var updatedTarefa = new Tarefa { Id = tarefaId, /* ... updated properties */ }; // Updated task
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterPorId(tarefaId)).Returns(existingTarefa); // Mock existing task
            var loggerMock = new Mock<ILogger<TarefaController>>();

            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.AtualizarTarefa(tarefaId, updatedTarefa);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            tarefaRepositoryMock.Verify(mock => mock.Atualizar(updatedTarefa), Times.Once());
        }

        [TestMethod]
        public void AtualizarTarefa_NullTarefa_ReturnsBadRequest()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.AtualizarTarefa(1, null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AtualizarTarefa_IdMismatch_ReturnsBadRequest()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);
            var tarefa = new Tarefa { Id = 2 }; // ID mismatch

            // Act
            var result = controller.AtualizarTarefa(1, tarefa);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AtualizarTarefa_NonexistentTarefa_ReturnsNotFound()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<int>())).Returns((Tarefa)null);

            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.AtualizarTarefa(9999, new Tarefa() { Id = 9999 });
            // If Tarefa is null, due to the method implementation, it returns a bad request first, so we need to actually test...
            // ...with a "valid" Tarefa here. Also, notice how we need to match the {id} with the created object.

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void ExcluirTarefa_ExistingTarefa_ReturnsOk()
        {
            // Setup
            var tarefaId = 1;
            var existingTarefa = new Tarefa { Id = tarefaId }; // Existing task
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterPorId(tarefaId)).Returns(existingTarefa); // Mock existing task

            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.ExcluirTarefa(tarefaId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            tarefaRepositoryMock.Verify(mock => mock.Excluir(tarefaId), Times.Once());
        }


        [TestMethod]
        public void ExcluirTarefa_NonexistentTarefa_ReturnsNotFound()
        {
            // Setup
            var tarefaRepositoryMock = new Mock<ITarefaRepository>();
            tarefaRepositoryMock.Setup(repo => repo.ObterPorId(It.IsAny<int>())).Returns((Tarefa)null); // No task found

            var loggerMock = new Mock<ILogger<TarefaController>>();
            var controller = new TarefaController(tarefaRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = controller.ExcluirTarefa(1); // Doesn't matter what ID we pass

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

    }
}