using FormazioneNET6;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FormazioneNET6")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "Formazione .NET 6 Web api - Minimal Api in Asp.Net Core",
    Title = "Formazione .NET 6 Api",
    Version = "v1",
}));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

//GET LIST
app.MapGet("/api/employees", async (EmployeeContext db) => await db.Employees.ToListAsync());

//GET BY ID
app.MapGet("/api/employees/{id}", async (EmployeeContext db, int id) => await db.Employees.FindAsync(id));

// ADD
app.MapPost("/api/employees", async (EmployeeContext db, Employee employee) =>
{
    await db.Employees.AddAsync(employee);
    await db.SaveChangesAsync();
    Results.Accepted();
});

//UPDATE
app.MapPut("/api/employees", async (EmployeeContext db, Employee employee) =>
{
    var existingEmployee = await db.Employees.FindAsync(employee.Id);
    if (existingEmployee == null) return Results.BadRequest();

    existingEmployee.Name = employee.Name;
    existingEmployee.Surname = employee.Surname;
    existingEmployee.Code = employee.Code;
    existingEmployee.Ufficio = employee.Ufficio;

    db.Update(existingEmployee);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

//DELETE
app.MapDelete("/api/employees/{id}", async (EmployeeContext db, int id) =>
{
    var employee = await db.Employees.FindAsync(id);
    if (employee == null) return Results.NotFound();

    db.Employees.Remove(employee);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Formazione Api v1");
    c.RoutePrefix = string.Empty;
});


app.Run();
