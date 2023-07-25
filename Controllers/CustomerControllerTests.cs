using Xunit;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace SimpleApi.Controllers.Tests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void GetAllCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(s => s.GetAllCustomers()).Returns(new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Aneesh", LastName = "Chattaraj", DateOfBirth = new DateTime(1990, 5, 15) },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Ch", DateOfBirth = new DateTime(1985, 9, 20) },
                new Customer { Id = 3, FirstName = "Mike", LastName = "Aneeshson", DateOfBirth = new DateTime(1982, 3, 10) }
            });
            var controller = new CustomerController(customerServiceMock.Object);

            // Act
            var result = controller.GetAllCustomers();

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetCustomerById_ExistingId_ShouldReturnCustomer()
        {
            // Arrange
            var customerId = 1;
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(s => s.GetCustomerById(customerId)).Returns(new Customer { Id = customerId, FirstName = "Aneesh", LastName = "Chattaraj", DateOfBirth = new DateTime(1990, 5, 15) });
            var controller = new CustomerController(customerServiceMock.Object);

            // Act
            var result = controller.GetCustomerById(customerId);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(customerId, result.Value.Id);
        }

        [Fact]
        public void GetCustomerById_NonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var customerId = 100;
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(s => s.GetCustomerById(customerId)).Returns((Customer)null);
            var controller = new CustomerController(customerServiceMock.Object);

            // Act
            var result = controller.GetCustomerById(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

[Fact]
public void UpdateCustomer_ExistingCustomer_ShouldReturnNoContent()
{
    // Arrange
    var existingCustomer = new Customer { Id = 1, FirstName = "Aneesh", LastName = "Chattaraj", DateOfBirth = new DateTime(1990, 5, 15) };
    var updatedCustomer = new Customer { Id = 1, FirstName = "Aneesh", LastName = "Ch", DateOfBirth = new DateTime(1990, 5, 15) };
    var customerServiceMock = new Mock<ICustomerService>();
    customerServiceMock.Setup(s => s.GetCustomerById(existingCustomer.Id)).Returns(existingCustomer);
    customerServiceMock.Setup(s => s.UpdateCustomer(existingCustomer,updatedCustomer));
    var controller = new CustomerController(customerServiceMock.Object);

    // Act
    var result = controller.UpdateCustomer(existingCustomer.Id, updatedCustomer);

    // Assert
    Assert.IsType<NoContentResult>(result);
}
[Fact]
public void UpdateCustomer_NonExistingCustomer_ShouldReturnNotFound()
{
    // Arrange
    var nonExistingCustomerId = 100; 
    var updatedCustomer = new Customer { Id = nonExistingCustomerId, FirstName = "Aneesh", LastName = "Ch", DateOfBirth = new DateTime(1990, 5, 15) };
    var customerServiceMock = new Mock<ICustomerService>();
    customerServiceMock.Setup(s => s.GetCustomerById(nonExistingCustomerId)).Returns((Customer)null);
    var controller = new CustomerController(customerServiceMock.Object);

    // Act
    var result = controller.UpdateCustomer(nonExistingCustomerId, updatedCustomer);

    // Assert
    Assert.IsType<NotFoundResult>(result);
}
[Fact]
public void DeleteCustomer_ExistingCustomer_ShouldReturnNoContent()
{
    // Arrange
    var existingCustomerId = 1;
    var existingCustomer = new Customer { Id = existingCustomerId, FirstName = "Aneesh", LastName = "Chattaraj", DateOfBirth = new DateTime(1990, 5, 15) };
    var customerServiceMock = new Mock<ICustomerService>();
    customerServiceMock.Setup(s => s.GetCustomerById(existingCustomerId)).Returns(existingCustomer);
    customerServiceMock.Setup(s => s.DeleteCustomer(existingCustomerId));
    var controller = new CustomerController(customerServiceMock.Object);

    // Act
    var result = controller.DeleteCustomer(existingCustomerId);

    // Assert
    Assert.IsType<NoContentResult>(result);
}
[Fact]
public void DeleteCustomer_NonExistingCustomer_ShouldReturnNotFound()
{
    // Arrange
    var nonExistingCustomerId = 100; 
    var customerServiceMock = new Mock<ICustomerService>();
    customerServiceMock.Setup(s => s.GetCustomerById(nonExistingCustomerId)).Returns((Customer)null);
    var controller = new CustomerController(customerServiceMock.Object);

    // Act
    var result = controller.DeleteCustomer(nonExistingCustomerId);

    // Assert
    Assert.IsType<NotFoundResult>(result);
}





    }
}
