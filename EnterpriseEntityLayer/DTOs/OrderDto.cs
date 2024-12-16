using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseEntityLayer.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int ProductId  { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
