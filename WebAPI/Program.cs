using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using RedLockNet.SERedis;
using RedLockNet;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using WebAPI.BackgroundServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using StackExchange.Redis;
using RedLockNet.SERedis.Configuration;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserTestService, UserTestManager>();
builder.Services.AddSingleton<IUserTestDal, EfUserTestDal>();
builder.Services.AddSingleton<IVoteLimitDal, EfVoteLimitDal>();
builder.Services.AddSingleton<IUserDal, EfUserDal>();
builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserInfoDal, EfUserInfoDal>();
builder.Services.AddSingleton<IUserInfoService, UserInfoManager>();
builder.Services.AddSingleton<IVoteService, VoteManager>();
builder.Services.AddSingleton<IVoteDal, EfVoteDal>();
builder.Services.AddSignalR();
builder.Services.AddDbContext<UserContext>();
builder.Services.AddDbContext<UserDataContext>();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    //    options.FallbackPolicy = options.DefaultPolicy;
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("*");
app.UseRouting();
//app.UseDeveloperExceptionPage();
//app.UseCors(x => x
//                .AllowAnyMethod()
//                .AllowAnyHeader()
//                .SetIsOriginAllowed(origin => true) // allow any origin

//                .AllowAnyOrigin()
//                ); // allow credentials
app.MapControllers();

app.Run();


