using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDelete("api/people-no-withopenapi/{id:int}", Results<NoContent, NotFound> (int id) =>
{
    return id > 0 ? TypedResults.NoContent() : TypedResults.NotFound();
});

app.MapDelete("api/people-withopenapi/{id:int}", Results<NoContent, NotFound> (int id) =>
{
    return id > 0 ? TypedResults.NoContent() : TypedResults.NotFound();
})
.WithOpenApi(operation =>
{
    operation.Summary = "Deletes a person given the ID. The 404 response has the description 'Client error', while it should be 'Not Found'";
    return operation;
});

app.Run();
