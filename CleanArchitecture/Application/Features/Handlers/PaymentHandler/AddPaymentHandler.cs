using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Org.BouncyCastle.Asn1.Pkcs;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.PaymentHandler
{
    internal class AddPaymentHandler(IUnitOfWork _unitOfWork, IMapper mapper) : IRequestHandler<GenericCreateCommand<PaymentVerificationRequestDTO, GenericResponse<PaymentVerificationRequestDTO>>, GenericResponse<PaymentVerificationRequestDTO>>
    {
        private static string KeyId = Environment.GetEnvironmentVariable("TestKeyId") ?? ""; // Your Razorpay Key ID
        private static string KeySecret = Environment.GetEnvironmentVariable("TestKeySecret") ?? ""; // Your Razorpay Key Secret

        public async Task<GenericResponse<PaymentVerificationRequestDTO>> Handle(GenericCreateCommand<PaymentVerificationRequestDTO, GenericResponse<PaymentVerificationRequestDTO>> request, CancellationToken cancellationToken)
        {
            try
            {
                if (request?.Entity != null)
                {
                    var generatedSignature = GenerateSignature(request.Entity.OrderId, request.Entity.PaymentId);

                    if (generatedSignature == request.Entity.Signature)
                    {
                        // Save payment details to the database
                        var payment = new Payment
                        {
                            OrderId = request.Entity.OrderId,
                            PaymentId = request.Entity.PaymentId,
                            PaymentSignature = request.Entity.Signature,
                            CustomerName = "John Doe", // You can modify this to get customer data from the frontend
                            Amount = 500, // Replace with the actual amount
                            PaymentStatus = "Success", // Set based on the Razorpay response (Success/Failed)
                            PaymentDate = DateTime.UtcNow,
                            CustomerEmail = "john@example.com", // Get this from frontend
                            CustomerContact = "9999999999" // Get this from frontend
                        };

                        if (payment != null)
                        {
                            await _unitOfWork.paymentRepository.InsertAsync(payment);
                            await _unitOfWork.paymentRepository.SaveAsync(cancellationToken);
                            return new GenericResponse<PaymentVerificationRequestDTO> { Content = request.Entity ?? null, Error = "null", Message = "Payment detail Successfully", Success = true };
                        }
                        return new GenericResponse<PaymentVerificationRequestDTO> { Content = null, Error = "Error occured", Message = "Error Occured while saving the payment data", Success = false };
                    }
                    return new GenericResponse<PaymentVerificationRequestDTO> { Content = null, Error = "Error occured", Message = "Signature not match", Success = false };
                }
                return new GenericResponse<PaymentVerificationRequestDTO> { Content = null, Error = "Enter valid data", Message = "Please enter the valid data", Success = false };
            }
            catch (Exception ex)
            {
                return new GenericResponse<PaymentVerificationRequestDTO>
                {
                    Content = null,
                    Error = ex?.InnerException?.Message,
                    Message = ex?.Message,
                    Success = false
                };
            }
        }
        private string GenerateSignature(string orderId, string paymentId)
        {
            string body = $"{orderId}|{paymentId}";
            var hmac = new System.Security.Cryptography.HMACSHA256();
            hmac.Key = System.Text.Encoding.UTF8.GetBytes(KeySecret);
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(body));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
