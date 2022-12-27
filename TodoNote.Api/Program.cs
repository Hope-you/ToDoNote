using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyToDo.Api;
using TodoNote.Api;
using TodoNote.Api.Context;
using TodoNote.Api.Extensions;
using TodoNote.Api.Repository;
using TodoNote.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ToDoNoteContext>(options =>
{
    var configruation = builder.Configuration.GetConnectionString("ToDoConnection");
    options.UseMySql(configruation, new MySqlServerVersion(new Version()));


}).AddUnitOfWork<ToDoNoteContext>()
.AddCustomRepository<ToDo, ToDoRepository>()
.AddCustomRepository<Memo, MemoRepository>()
.AddCustomRepository<User, UserRepository>();

builder.Services.AddTransient<IToDoService, ToDoService>();

builder.Services.AddTransient<IMemoService, MemoService>();

//Ìí¼Óautomapper
var autoMapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProFile());
});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
