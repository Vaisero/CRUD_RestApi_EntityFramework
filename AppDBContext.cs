﻿using EF_CRUD_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_CRUD_REST_API
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Product_OrderModel> product_order { get; set; }
        public DbSet<CustomerModel> customer { get; set; }
        public DbSet<StatusModel> status { get; set; }
        public DbSet<AvgSummPerHour_procedureModel> avgSummPerHour { get; set; }
        public DbSet<OrderSummFromBirthday_procedureModel> orderSummFromBirthday { get; set; }
    }
}
