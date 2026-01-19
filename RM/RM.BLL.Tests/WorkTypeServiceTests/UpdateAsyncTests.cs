// using FluentAssertions;
// using Moq;
// using RM.BLL.Abstractions.Models;
// using RM.BLL.Abstractions.Services;
// using RM.BLL.Tests.TestData;

// namespace RM.BLL.Tests.WorkTypeServiceTests;

// /// <summary>
// /// Тесты для метода <see cref="IWorkTypeService.UpdateAsync"/>.
// /// </summary>
// /// <param name="fixture">Настройка контекста для тестирования сервиса видов работ.</param>
// public class UpdateAsyncTests(WorkTypeServiceFixture fixture) : IClassFixture<WorkTypeServiceFixture>
// {
//     #region Поля

//     private readonly WorkTypeServiceFixture _fixture = fixture;

//     #endregion

//     #region Методы

//     /// <summary>
//     /// Тест обновления вида работ для корректных данных.
//     /// </summary>
//     [Theory]
//     [MemberData(nameof(WorkTypeServiceTestData.UpdateAsyncForCorrectDataTestData), 
//                 MemberType = typeof(WorkTypeServiceTestData))]
//     public async Task ForCorrectDataTest(WorkTypeUpdationModel workTypeUpdationModel)
//     {
//         _fixture.WorkTypeRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<DAL.Abstractions.Models.WorkTypeShortModel>()))
//                                        .Returns(Task.CompletedTask);
        
//         _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
//                                        .Returns(Task.FromResult<DAL.Abstractions.Models.WorkTypeModel?>(null));

//         var workUnitModel = workTypeUpdationModel.WorkUnitId.HasValue? 
//         new DAL.Abstractions.Models.WorkUnitModel
//         {
//             Id = workTypeUpdationModel.WorkUnitId.Value
//         }: null;

//         _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
//                                        .Returns(Task.FromResult(workUnitModel));

//         var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object,
//                                                   _fixture.WorkUnitRepositoryMock.Object, 
//                                                   _fixture.WorkTypeNameValidator, 
//                                                   _fixture.WorkTypeUpdationModelValidator,
//                                                   _fixture.PageOptionsValidator);

//         var expected = async () => await workTypeService.UpdateAsync(workTypeUpdationModel);

//         await expected.Should().NotThrowAsync<Exception>();
//     }

//     /// <summary>
//     /// Тест обновления вида работ для некорректных данных.
//     /// </summary>
//     [Theory]
//     [MemberData(nameof(WorkTypeServiceTestData.UpdateAsyncForIncorrectDataTestData), 
//                 MemberType = typeof(WorkTypeServiceTestData))]
//     public async Task ForIncorrectDataTest(WorkTypeUpdationModel workTypeUpdationModel)
//     {
//         _fixture.WorkTypeRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<DAL.Abstractions.Models.WorkTypeShortModel>()))
//                                        .Throws(() => new Exception("Repository Exception"));

//         _fixture.WorkTypeRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
//                                        .Returns(Task.FromResult<DAL.Abstractions.Models.WorkTypeModel?>( new DAL.Abstractions.Models.WorkTypeModel
//                                        {
//                                           Id = workTypeUpdationModel.Id,
//                                           Name = workTypeUpdationModel.Name
//                                        }));

//         _fixture.WorkUnitRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<byte>()))
//                                        .Returns(Task.FromResult<DAL.Abstractions.Models.WorkUnitModel?>(null));   

//         var workTypeService = new WorkTypeService(_fixture.WorkTypeRepositoryMock.Object,
//                                                   _fixture.WorkUnitRepositoryMock.Object, 
//                                                   _fixture.WorkTypeNameValidator,
//                                                   _fixture.WorkTypeUpdationModelValidator,
//                                                   _fixture.PageOptionsValidator);

//         var expected = async() => await workTypeService.UpdateAsync(workTypeUpdationModel);

//         await expected.Should().ThrowAsync<Exception>();
//     }

//     #endregion
// }
