﻿namespace Demo_shopping.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public string UserName {  get; set; }
        public string OrderCode { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
