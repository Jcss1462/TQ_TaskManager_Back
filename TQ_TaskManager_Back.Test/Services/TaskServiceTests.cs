using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using TQ_TaskManager_Back.Contexts;
using TQ_TaskManager_Back.Dtos;
using TQ_TaskManager_Back.Models;
using TQ_TaskManager_Back.Services;

namespace TQ_TaskManager_Back.Test.Services;

public class TaskServiceTests
{
    [Fact]
    public async Task CreateTask_Should_AddNewTask_When_DataIsValid()
    {
        // Arrange
        var mockContext = new Mock<TQContext>();

        // Mocking Estados
        var mockEstados = new List<Estado>
        {
            new Estado { Id = 1, Nombre = "Pendiente" }
        }.AsQueryable();

        var mockEstadosDbSet = new Mock<DbSet<Estado>>();
        mockEstadosDbSet.As<IQueryable<Estado>>().Setup(m => m.Provider).Returns(mockEstados.Provider);
        mockEstadosDbSet.As<IQueryable<Estado>>().Setup(m => m.Expression).Returns(mockEstados.Expression);
        mockEstadosDbSet.As<IQueryable<Estado>>().Setup(m => m.ElementType).Returns(mockEstados.ElementType);
        mockEstadosDbSet.As<IQueryable<Estado>>().Setup(m => m.GetEnumerator()).Returns(mockEstados.GetEnumerator());

        mockContext.Setup(c => c.Estados).Returns(mockEstadosDbSet.Object);

        // Mocking Usuarios
        var mockUsuarios = new List<Usuario>
        {
            new Usuario { Id = 1, Correo = "user@example.com" }
        }.AsQueryable();

        var mockUsuariosDbSet = new Mock<DbSet<Usuario>>();
        mockUsuariosDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(mockUsuarios.Provider);
        mockUsuariosDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(mockUsuarios.Expression);
        mockUsuariosDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(mockUsuarios.ElementType);
        mockUsuariosDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(mockUsuarios.GetEnumerator());

        mockContext.Setup(c => c.Usuarios).Returns(mockUsuariosDbSet.Object);

        // Mocking Tareas
        var mockTareasDbSet = new Mock<DbSet<Tarea>>();
        mockContext.Setup(c => c.Tareas).Returns(mockTareasDbSet.Object);

        var taskService = new TaskService(mockContext.Object);

        var newTaskDto = new CreateTareaDto
        {
            Titulo = "Test Task",
            Descripcion = "This is a test task",
            Usuarioasignadoid = 1
        };

        // Act
        await taskService.CreateTask(newTaskDto);

        // Assert
        mockTareasDbSet.Verify(m => m.AddAsync(It.IsAny<Tarea>(), default), Times.Once);
        mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }

}
