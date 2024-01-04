﻿using MediatR;
using Project.Services.Order.BLL.Commands;
using Project.Services.Order.BLL.DTOs;
using Project.Services.Order.Domain.OrderAggregate;
using Project.Services.Order.Infrastructure;
using Project.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.BLL.Handlers
{
    //  Response<CreatedOrderDto> dönecek..
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // requestin (CreateOrderCommand) Address parametresi
            var newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.Customer_ID, newAddress); // ctor çalışır

            // request.OrderItems --> public List<OrderItemDto> OrderItems { get; set; }

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            // CreatedOrderDto.CS 
            return Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 200);
            // sonunda, CreatedOrderDto.cs'nin field'i olan OrderId geriye dönülecek...
        }

    }
}