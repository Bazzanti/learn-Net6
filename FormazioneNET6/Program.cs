using FormazioneNET6;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EmployeeContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("FormazioneNET6")));
//builder.Services.AddSwaggerGen();
var app = builder.Build();

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

    db.Update(employee);
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

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.Run();
