using Application.DTO;
using Application.Features.Command.GenericCommands;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using static Application.DTO.Auth;

namespace Application.Features.Handlers.PaymentHandler
{
    internal class CreateHandler : IRequestHandler<GenericCreateCommand<PaymentLinkRequestDTO, GenericResponse<string>>, GenericResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly string KeyId;
        private readonly string KeySecret;

        public CreateHandler(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;

            // Initialize Razorpay credentials
            KeyId = _configuration["TestKeyId"] ?? ""; // Your Razorpay Key ID
            KeySecret = _configuration["TestKeySecret"] ?? ""; // Your Razorpay Key Secret
        }

        public async Task<GenericResponse<string>> Handle(GenericCreateCommand<PaymentLinkRequestDTO, GenericResponse<string>> request, CancellationToken cancellationToken)
        {
            try
            {
                RazorpayClient client = new RazorpayClient(KeyId, KeySecret);

                // Generate a Guid
                var guid = Guid.NewGuid();

                // Convert the Guid to a string, remove dashes, and take the first 10 characters
                string referenceId = new string(guid.ToString("N").Take(10).ToArray());

                // Calculate expiration timestamp (15 minutes from now)
                var expireByTimestamp = DateTimeOffset.UtcNow.AddMinutes(17).ToUnixTimeSeconds();

                // Create payment link parameters
                var options = new Dictionary<string, object>
                {
                    { "amount", request.Entity.Amount * 100 }, // Convert amount to paise
                    { "currency", "INR" },
                    { "accept_partial", true }, // Allow partial payments
                    { "first_min_partial_amount", 100 * 100 }, // Convert minimum partial amount to paise
                    { "expire_by", expireByTimestamp }, // Expiration timestamp
                    { "reference_id", referenceId }, // Reference ID for the payment link
                    { "description", "Payment for policy no #23456" }, // Description of the payment
                    { "customer", new Dictionary<string, object> // Customer details
                        {
                            { "name", request.Entity.CustomerName },
                            { "contact", request.Entity.CustomerContact },
                            { "email", request.Entity.CustomerEmail }
                        }
                    },
                    { "notify", new Dictionary<string, object> // Notification preferences
                        {
                            { "sms", true },
                            { "email", true }
                        }
                    },
                    { "reminder_enable", true }, // Enable payment reminders
                    { "notes", new Dictionary<string, object> // Additional notes
                        {
                            { "policy_name", "Jeevan Bima" }
                        }
                    },
                    { "callback_url", "http://localhost:3000/home" }, // Callback URL
                    { "callback_method", "get" } // Callback method
                };

                // Log the expireByTimestamp for debugging
                Console.WriteLine($"Calculated expire_by timestamp: {expireByTimestamp}");

                // Create Payment Link
                var paymentLink = client.PaymentLink.Create(options);

                return new GenericResponse<string>
                {
                    Content = $"{paymentLink["id"]}, {paymentLink["short_url"]}",
                    Error = null,
                    Message = "Payment link created successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // Return error response
                return new GenericResponse<string>
                {
                    Content = null,
                    Error = ex?.InnerException?.Message ?? ex.Message,
                    Message = "Error occurred while creating payment link",
                    Success = false
                };
            }
        }

    }
}
