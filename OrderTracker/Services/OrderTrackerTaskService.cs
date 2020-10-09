using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OrderTracker.Extensions;
using OrderTracker.Models;
using OrderTracker.Models.DataModels;
using OrderTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace OrderTracker.Services
{
    public class OrderTrackerTaskService
    {
        private readonly OrderTrackerDbContext _dbContext;

        public OrderTrackerTaskService(OrderTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUpdateDeliveryInfo(DeliveryInfoViewModel item)
        {
            var entity = _dbContext.DeliveryInfo.Find(item.Id) ?? new DeliveryInfoEntity();
            entity.ProductId = item.Product;
            entity.SellerId = item.Seller;
            entity.StartDeliveryDate = item.StartDeliveryDate.Value;
            entity.EndDeliveryDate = item.EndDeliveryDate.Value;

            if (entity.Id != 0)
                _dbContext.DeliveryInfo.Update(entity);
            else
                _dbContext.DeliveryInfo.Add(entity);

            _dbContext.SaveChanges();
        }

        public void AddUpdateProduct(ProductViewModel item)
        {
            var entity = _dbContext.Products.Find(item.Id) ?? new ProductEntity();
            entity.Name = item.Name;

            if (entity.Id != 0)
                _dbContext.Products.Update(entity);
            else
                _dbContext.Products.Add(entity);

            _dbContext.SaveChanges();
        }

        public void AddUpdateSeller(SellerViewModel item)
        {
            var entity = _dbContext.Sellers.Find(item.Id) ?? new SellerEntity();
            entity.Name = item.Name;
            entity.Country = item.Country;

            if (entity.Id != 0)
                _dbContext.Sellers.Update(entity);
            else
                _dbContext.Sellers.Add(entity);

            _dbContext.SaveChanges();
        }

        public IEnumerable<DisplayDeliveryInfoViewModelItem> GetDeliveryInfoItems(DateTime? dateFilter)
        {         
            
            var result = from deliveryInfo in _dbContext.DeliveryInfo
                         join sellers in _dbContext.Sellers on deliveryInfo.SellerId equals sellers.Id
                         join products in _dbContext.Products on deliveryInfo.ProductId equals products.Id
                         where dateFilter == null || (deliveryInfo.EndDeliveryDate > dateFilter.Value.AddMonths(-6) && deliveryInfo.EndDeliveryDate<=dateFilter.Value)
                         select new { deliveryInfo, products, sellers };

            
            return result.ToList().Select(a => new DisplayDeliveryInfoViewModelItem
            {
                Id = a.deliveryInfo.Id,
                Seller = a.sellers.Name,
                Product = a.products.Name,
                StartWeek = a.deliveryInfo.StartDeliveryDate.GetWeek(),
                EndWeek = a.deliveryInfo.EndDeliveryDate.GetWeek(),
                DayDiff = GetDayDiff(a.deliveryInfo.StartDeliveryDate, a.deliveryInfo.EndDeliveryDate),
                DayColor = GetDayColor(a.deliveryInfo.StartDeliveryDate, a.deliveryInfo.EndDeliveryDate),
                WeekDiff = GetWeekDiff(a.deliveryInfo.StartDeliveryDate, a.deliveryInfo.EndDeliveryDate),
                WeekColor = GetWeekColor(a.deliveryInfo.StartDeliveryDate, a.deliveryInfo.EndDeliveryDate)
            }); 
        }
        
        public DeliveryInfoViewModel GetDeliveryInfoItem(int id)
        {
            var result = _dbContext.DeliveryInfo.FirstOrDefault(a => a.Id.Equals(id));
            return new DeliveryInfoViewModel
            {
                Id = result.Id,
                Product = result.ProductId,
                Seller = result.SellerId,
                StartDeliveryDate = result.StartDeliveryDate,
                EndDeliveryDate = result.EndDeliveryDate
            };
        }

        public IEnumerable<ProductViewModel> GetProductItems()
        {
            return _dbContext.Products.Select(a => new ProductViewModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }

        public ProductViewModel GetProductItem(int id)
        {
            var result = _dbContext.Products.Find(id);
            var item = new ProductViewModel()
            {
                Id = result.Id,
                Name = result.Name
            };
            return item;
        }

        public IEnumerable<SellerViewModel> GetSellerItems()
        {
            return _dbContext.Sellers.Select(a => new SellerViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Country = a.Country
            }).ToList();
        }

        public SellerViewModel GetSellerItem(int id)
        {
            var result = _dbContext.Sellers.FirstOrDefault(a => a.Id.Equals(id));
            return new SellerViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Country = result.Country
            };
        }

        public void DeleteProductItem(int id)
        {
            var result = _dbContext.Products.Find(id);
            _dbContext.Products.Remove(result);
            _dbContext.SaveChanges();    
        }

        public void DeleteSellerItem(int id)
        {
            var result = _dbContext.Sellers.Find(id);
            _dbContext.Sellers.Remove(result);
            _dbContext.SaveChanges();
        }

        public void DeleteDeliveryInfoItem(int id)
        {
            var result = _dbContext.DeliveryInfo.Find(id);
            _dbContext.DeliveryInfo.Remove(result);
            _dbContext.SaveChanges();
        }

        private int GetDayDiff(DateTime start, DateTime end)
        {
            return (end - start).Days;
        }
            

        private string GetDayColor(DateTime start, DateTime end)
        {
            var day = GetDayDiff(start, end);
            if (day.IsBetween(0, 100, true))
                return "blue";

            if (day.IsBetween(101, 200, true))
                return "yellow";

            return "red";
        }

        private int GetWeekDiff(DateTime start, DateTime end)
        {
            return (int)Math.Ceiling((end - start).Days / 7.0);
        }
            
        private string GetWeekColor(DateTime start, DateTime end)
        {
            var week = GetWeekDiff(start, end);

            if (week.IsBetween(0, 2, true))
                return "blue";

            if (week.IsBetween(3, 4, true))
                return "yellow";

            return "red";
        }
    }
}
