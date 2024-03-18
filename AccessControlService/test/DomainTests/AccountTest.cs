using Domain.Models;

namespace AccountTest;

public class AccountTest
{
    [Fact]
    public void Create_account_success()
    {
        // Arrange
        var username = "Jhon Wick";
        var email = "jhon@wick.com";
        
        // Act
        var sut = new Account(username, email);
        
        // Assert
        Assert.True(sut.Valid);
        Assert.True(sut.Id != Guid.Empty);
        Assert.Equal(username, sut.Username);
        Assert.Equal(email, sut.Email);
        Assert.True(sut.Active);
        Assert.Equal(DateTime.Today, sut.CreatedOn.Date);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("abc")]
    [InlineData("abc@")]
    [InlineData("abc@def")]
    [InlineData("abc@def.")]
    public void Create_account_fail_with_invalid_email(string invalidEmail)
    {
        var sut = CreateAccountForTest(email: invalidEmail);
        
        Assert.False(sut.Valid);
        Assert.Single(sut.ValidationResult.Errors);
    }

    [Fact]
    public void Deactivate_account_success()
    {
        var sut = CreateAccountForTest();
        
        sut.Deactivate();
        
        Assert.True(sut.Valid);
        Assert.False(sut.Active);
    }

    [Fact]
    public void Deactivate_account_fail_when_account_is_deactivated()
    {
        var sut = CreateAccountForTest();
        sut.Deactivate();
        
        sut.Deactivate();
        
        Assert.False(sut.Valid);
        Assert.Single(sut.ValidationResult.Errors);
    }

    [Fact]
    public void Change_username_success()
    {
        var newUsername = "Mary";
        var sut = CreateAccountForTest(username: "Jhon");
        
        sut.ChangeUsername(newUsername);
        
        Assert.True(sut.Valid);
        Assert.Equal(newUsername, sut.Username);
    }

    [Fact]
    public void Change_username_fail_when_account_is_deactivated()
    {
        var sut = CreateAccountForTest(username: "Jhon");
        sut.Deactivate();
        
        sut.ChangeUsername("Mary");
        
        Assert.False(sut.Valid);
        Assert.Single(sut.ValidationResult.Errors);
    }

    private Account CreateAccountForTest(string? username = null, string? email = null)
    {
        return new Account
        (
            username ?? "Jhon Wick",
            email ?? "jhon@wick.com"
        );
    }
}