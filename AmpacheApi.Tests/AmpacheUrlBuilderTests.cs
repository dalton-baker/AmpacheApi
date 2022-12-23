namespace AmpacheApi.Tests;

[ExcludeFromCodeCoverage]
[TestClass]
public class AmpacheUrlBuilderTests
{
    public TestContext TestContext { get; set; } = null!;
    private readonly Faker _faker = new();


    [TestMethod]
    public void CreateSutTest()
    {
        //Arrange

        //Act
        var urlBuilder = new AmpacheUrlBuilder();

        //Assert
        Assert.IsNotNull(urlBuilder);
    }

    [TestMethod]
    public void CreateSutTest_WithAmpacheAuthInfo()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var auth = _faker.Internet.Password();
        var ampacheAuthInfo = new AmpacheAuthInfo
        {
            AmpacheUrl = baseUrl,
            AuthToken = auth
        };

        //Act
        var builtUrl = new AmpacheUrlBuilder(ampacheAuthInfo).Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual($"{baseUrl}/server/json.server.php?auth={auth}", builtUrl);
    }

    [TestMethod]
    public void BuildTest_WithBaseUrl()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var urlBuilder = new AmpacheUrlBuilder().WithBaseUrl(baseUrl);

        //Act
        var builtUrl = urlBuilder.Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual(baseUrl + "/server/json.server.php", builtUrl);
    }

    [TestMethod]
    public void BuildTest_WithBaseUrlAndTrailingSlash()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var urlBuilder = new AmpacheUrlBuilder().WithBaseUrl(baseUrl + "/");

        //Act
        var builtUrl = urlBuilder.Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual(baseUrl + "/server/json.server.php", builtUrl);
    }

    [TestMethod]
    public void BuildTest_NoBaseUrl()
    {
        //Arrange
        var urlBuilder = new AmpacheUrlBuilder();

        //Act
        var builtUrl = urlBuilder.Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual(string.Empty, builtUrl);
    }

    [TestMethod]
    public void WithBaseUrlTest()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var urlBuilder = new AmpacheUrlBuilder().WithBaseUrl(baseUrl + "/");

        //Act
        var builtUrl = urlBuilder.Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual(baseUrl + "/server/json.server.php", builtUrl);
    }

    [TestMethod]
    public void WithAuthenticationTest()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var auth = _faker.Internet.Password();

        //Act
        var builtUrl = new AmpacheUrlBuilder().WithBaseUrl(baseUrl).WithAuthentication(auth).Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual($"{baseUrl}/server/json.server.php?auth={auth}", builtUrl);
    }


    [TestMethod]
    public void WithActionTest()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var action = _faker.Database.Column();

        //Act
        var builtUrl = new AmpacheUrlBuilder().WithBaseUrl(baseUrl).WithAction(action).Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual($"{baseUrl}/server/json.server.php?action={action}", builtUrl);
    }

    [TestMethod]
    public void WithActionAndAmpacheAuthTest()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var action = _faker.Database.Column();
        var auth = _faker.Internet.Password();
        var ampacheAuthInfo = new AmpacheAuthInfo
        {
            AmpacheUrl = baseUrl,
            AuthToken = auth
        };

        //Act
        var builtUrl = new AmpacheUrlBuilder(ampacheAuthInfo).WithAction(action).Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.AreEqual($"{baseUrl}/server/json.server.php?auth={auth}&action={action}", builtUrl);
    }

    [TestMethod]
    public void WithLoginInfoTest()
    {
        //Arrange
        var baseUrl = _faker.Internet.DomainName();
        var userName = _faker.Internet.UserName();
        var password = _faker.Internet.Password();
        var ampacheAuthInfo = new AmpacheAuthInfo
        {
            AmpacheUrl = baseUrl,
            UserName = userName,
            Password = password,
        };

        //Act
        string builtUrl = new AmpacheUrlBuilder().WithLoginInfo(ampacheAuthInfo).Build();
        TestContext.WriteLine(builtUrl);

        //Assert
        Assert.IsFalse(string.IsNullOrEmpty(builtUrl));
    }
}
