using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class PaymentLinkRequestDTO
    {
        public decimal Amount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
    }

    public class PaymentVerificationRequestDTO
    {
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string Signature { get; set; }
    }

    // Payment Entity (model)
    public class PaymentDTO
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string PaymentSignature { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
    }
}
