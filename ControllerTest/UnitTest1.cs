using Moq;
using LineControl.Controllers;
using Kendo.Mvc.UI;
using LineControllerCore.Interface;
using Microsoft.AspNetCore.Mvc;
using LineControllerCore.Model;

namespace ControllerTest
{
  [TestClass]
  public class UnitTest1
  {
    private Mock<IActivityTypeService> serviceMock;
    private ActivityTypeController controller;

    [TestInitialize]
    public void Setup()
    {
      serviceMock = new Mock<IActivityTypeService>();
      controller = new ActivityTypeController(serviceMock.Object);
    }

    [TestMethod]
    public void TestMethod1()
    {
      var data = new List<ActivityTypeViewModel>
        {
            new ActivityTypeViewModel { Id = 1, Name = "Test" }
        };
      serviceMock
            .Setup(s => s.GetSelectViewModels())
            .Returns(data.AsQueryable());

      var result = controller.GetActivityType(new DataSourceRequest());
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(JsonResult));
    }
  }
}