
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ConnectedVehicle_API.Service.Secrets;
using ConnectVehicle.Data.Interfaces;
using ConnectVehicle.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
var builderInfo = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

// Add services to the container.
var services = builder.Services;

string kvURL = builder.Configuration["KeyVaultConfig:KVURL"];
string tenantId = builder.Configuration["KeyVaultConfig:TenantId"];
string clientId = builder.Configuration["KeyVaultConfig:ClientId"];
var clientSecret = builder.Configuration["KeyVaultConfig:ClientSecretId"];

// Add services to the container.
builder.Host.ConfigureAppConfiguration((context, config) =>
{

    // Set Azure KeyVault
    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
    var client = new SecretClient(new Uri(kvURL), credential);
    //string connectionString = client.GetSecret("ConnectionString--ConnectedVehicle").Value.Value.ToString();
    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
});

ConfigureService(services);

var app = builder.Build();

//// Set Azure KeyVault
//var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
//var client = new SecretClient(new Uri(kvURL), credential);
//string connectionString = client.GetSecret("ConnectionString--ConnectedVehicle").Value.Value.ToString();
//builder.Configuration.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.Use(async (context, next) =>
{
    if (!context.User.Identity?.IsAuthenticated ?? false)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Not Authenticated");
    }
    else await next();
});
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureService(IServiceCollection serviceCollection)
{
    services.AddScoped<IVehicleRepository, VehicleRepository>();
    services.AddControllers();
    services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");
    services.AddHttpContextAccessor();
    services.Configure<MyConfig>(builderInfo.GetSection("MyConfig"));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}




//string GetToken()
//{
//    AuthenticationContext context = new AuthenticationContext(authority);
//    ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
//    return context.AcquireTokenAsync(resource, clientCredential).Result.AccessToken;
//}

//string GetName(string token)
//{
//    HttpClient httpClient = new HttpClient();
//    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
//    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "");
//    request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
//    var response = httpClient.SendAsync(request).Result;
//    if (response.IsSuccessStatusCode)
//        return response.Content.ReadAsStringAsync().Result;
//    else
//        return response.StatusCode.ToString();
//}

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey
//            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true
//    };
//});
//builder.Services.AddAuthorization();


//app.MapPost("/security/createToken",
//[AllowAnonymous] (UserModel user) =>
//{
//    if (user.UserName == "volvo" && user.UserPwd == "connect12345")
//    {
//        var issuer = builder.Configuration["Jwt:Issuer"];
//        var audience = builder.Configuration["Jwt:Audience"];
//        var key = Encoding.ASCII.GetBytes
//        (builder.Configuration["Jwt:Key"]);
//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new[]
//            {
//                new Claim("Id", Guid.NewGuid().ToString()),
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
//                new Claim(JwtRegisteredClaimNames.Jti,
//                Guid.NewGuid().ToString())
//             }),
//            Expires = DateTime.UtcNow.AddMinutes(5),
//            Issuer = issuer,
//            Audience = audience,
//            SigningCredentials = new SigningCredentials
//            (new SymmetricSecurityKey(key),
//            SecurityAlgorithms.HmacSha512Signature)
//        };
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        var jwtToken = tokenHandler.WriteToken(token);
//        var stringToken = tokenHandler.WriteToken(token);
//        return Results.Ok(stringToken);
//    }
//    return Results.Unauthorized();
//});