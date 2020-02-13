using MockQueryable.FakeItEasy;
using Moq;
using Npv_Exercise.Core.Constants;
using Npv_Exercise.Core.Models.NpvVariables;
using Npv_Exercise.Data.Contexts;
using Npv_Exercise.Data.Entities.NpvVariables;
using Npv_Exercise.Service.Services.NpvVariables;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Npv_Exercise.Service.Tests.Services.NpvVariables
{
    public class NpvVariableServiceTests
    {       
        [Fact]
        public async void AddNpvVariableTest()
        {
            // Arrange            
            var appDbContextMock = new Mock<IAppDbContext>();
            appDbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1);
            appDbContextMock.Setup(x => x.NpvVariables).Returns(new List<NpvVariableEntity>().AsQueryable().BuildMockDbSet());

            var npvVariableService = new NpvVariableService(appDbContextMock.Object);

            // Act
            var sut = await npvVariableService.AddNpvVariable(new NpvVariable());

            // Assert
            Assert.Empty(sut.Errors);
        }

        [Fact]
        public async void GetNpvVariableByIdTest()
        {
            // Arrange            
            var npvVariableEntities = new List<NpvVariableEntity>
            {
                new NpvVariableEntity
                {
                    Id = 1,
                    InitialValue = -100,
                    LowerBoundRate = 2,
                    UpperBoundRate = 3,
                    Increment = 0.25m,
                    CashflowEntities = new List<NpvVariableCashflowEntity>
                    {
                        new NpvVariableCashflowEntity
                        {
                            Id = 1,
                            NpvVariableId = 1,
                            Cashflow = 100,
                            Order = 1
                        },
                        new NpvVariableCashflowEntity
                        {
                            Id = 2,
                            NpvVariableId = 1,
                            Cashflow = 200,
                            Order = 2
                        }
                    }
                },
                new NpvVariableEntity
                {
                    Id = 2
                }
            };

            var appDbContextMock = new Mock<IAppDbContext>();
            appDbContextMock.Setup(x => x.NpvVariables).Returns(npvVariableEntities.AsQueryable().BuildMockDbSet());

            var npvVariableService = new NpvVariableService(appDbContextMock.Object);

            // Act
            var sut = await npvVariableService.GetNpvVariableById(1);

            // Assert
            // NOTE: since we want to keep Query folder in services project internal to avoid being used in the wrong places, mapping should also be tested here
            Assert.Empty(sut.Errors);
            Assert.NotNull(sut.Data);
            Assert.Equal(1, sut.Data.Id);
            Assert.Equal(-100, sut.Data.InitialValue);
            Assert.Equal(2, sut.Data.LowerBoundRate);
            Assert.Equal(3, sut.Data.UpperBoundRate);
            Assert.Equal(0.25m, sut.Data.Increment);
            Assert.Equal(2, sut.Data.Cashflows.Count);
            Assert.Equal(1, sut.Data.Cashflows[0].Id);
            Assert.Equal(1, sut.Data.Cashflows[0].NpvVariableId);
            Assert.Equal(100, sut.Data.Cashflows[0].Cashflow);
            Assert.Equal(1, sut.Data.Cashflows[0].Order);
            Assert.Equal(2, sut.Data.Cashflows[1].Id);
            Assert.Equal(1, sut.Data.Cashflows[1].NpvVariableId);
            Assert.Equal(200, sut.Data.Cashflows[1].Cashflow);
            Assert.Equal(2, sut.Data.Cashflows[1].Order);
        }

        [Fact]
        public async void GetNpvVariableById_NotFoundError_Test()
        {
            // Arrange            
            var npvVariableEntities = new List<NpvVariableEntity>
            {
                new NpvVariableEntity
                {
                    Id = 1
                }
            };
            var appDbContextMock = new Mock<IAppDbContext>();
            appDbContextMock.Setup(x => x.NpvVariables).Returns(npvVariableEntities.AsQueryable().BuildMockDbSet());
            appDbContextMock.Setup(x => x.NpvVariableCashflows).Returns(new List<NpvVariableCashflowEntity>().AsQueryable().BuildMockDbSet());

            var npvVariableService = new NpvVariableService(appDbContextMock.Object);

            // Act
            var sut = await npvVariableService.GetNpvVariableById(2);

            // Assert
            Assert.NotEmpty(sut.Errors);
            Assert.Equal(ErrorCodes.NpvVariableServiceError005, sut.Errors[0].Code);
        }

        [Fact]
        public async void GetNpvVariablesList_Test()
        {
            // Arrange            
            var npvVariableEntities = new List<NpvVariableEntity>
            {
                new NpvVariableEntity
                {
                    Id = 1
                },
                new NpvVariableEntity
                {
                    Id = 2
                }
            };
            var appDbContextMock = new Mock<IAppDbContext>();
            appDbContextMock.Setup(x => x.NpvVariables).Returns(npvVariableEntities.AsQueryable().BuildMockDbSet());
            appDbContextMock.Setup(x => x.NpvVariableCashflows).Returns(new List<NpvVariableCashflowEntity>().AsQueryable().BuildMockDbSet());

            var npvVariableService = new NpvVariableService(appDbContextMock.Object);

            // Act
            var sut = await npvVariableService.GetNpvVariablesList();

            // Assert
            Assert.Empty(sut.Errors);
            Assert.Equal(2, sut.Data.Count);
        }
    }
}
