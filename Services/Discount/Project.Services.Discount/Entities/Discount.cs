using System;

namespace Project.Services.Discount.Entities
{
    [Dapper.Contrib.Extensions.Table("discount")] // tablo ismi: discount
    public class Discount
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
