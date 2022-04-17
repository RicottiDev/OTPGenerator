using Microsoft.Extensions.Logging;
using OTPGenerator.Core.DTOs.SendOTPCode;
using OTPGenerator.Core.DTOs.ValidateOTPCode;
using OTPGenerator.Core.Interfaces.IHelpers;
using OTPGenerator.Core.Interfaces.IServices;
using OTPGenerator.Infrastructure.Entities;
using OTPGenerator.Infrastructure.IRepositories;
using System;
using System.Threading.Tasks;

namespace OTPGenerator.Core.Services
{
    public class OTPService : IOTPService
    {
        private const string subject = "Code OTP";
        private const string errorMaximumAttempts = "Error maximum attempts allowed";
        private const string errorMaximumDurationMinutes = "Error expired otp code";
        private const string errorDifferentCode = "Error different otp codes";
        private readonly IOTPCodeRepository _oTPCodeRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IOTPCodeGenerator _oTPCodeGenerator;
        private readonly ILogger<OTPService> _logger;
        public OTPService(IOTPCodeRepository oTPCodeRepository,
                          ITenantRepository tenantRepository,
                          IEmailSenderService emailSenderService,
                          IOTPCodeGenerator oTPCodeGenerator,
                          ILogger<OTPService> logger)
        {
            _oTPCodeRepository = oTPCodeRepository;
            _tenantRepository = tenantRepository;
            _emailSenderService = emailSenderService;
            _oTPCodeGenerator = oTPCodeGenerator;
            _logger = logger;
        }

        public async Task<string> SendCodeOTP(SendOTPCodeRequest sendOTPCodeRequest)
        {
            var code = _oTPCodeGenerator.CodeGenerate();
            var transactionId = Guid.NewGuid().ToString();
            var tenant = await _tenantRepository.GetByTenantNameAsync(sendOTPCodeRequest.Name);

            var oTPRequest = new OTPCode()
            {
                Email = sendOTPCodeRequest.Email,
                Dni = sendOTPCodeRequest.DNI,
                Code = code,
                TransactionId = transactionId,
                CreateDate = DateTime.Now,
                Attempts = 0,
                Tenant = tenant
            };

            await _oTPCodeRepository.AddAsync(oTPRequest);

            await _emailSenderService.SendEmail(sendOTPCodeRequest.Email, subject, code);

            return oTPRequest.TransactionId;
        }

        public async Task ValidateOTP(ValidateOTPCodeRequest validateOTPCodeRequest)
        {
            var otpConfigurations = await _oTPConfigurationRepository.GetLastestConfigurationAsync();
            var otpRequest = await _oTPCodeRepository.GetByTransactionIdAsync(validateOTPCodeRequest.TransactionId);

            if (otpConfigurations.MaximumAttempts < otpRequest.Attempts)
            {
                _logger.LogError($"{errorMaximumAttempts}: {otpRequest.Attempts} of {otpConfigurations.MaximumAttempts}");
                throw new Exception(errorMaximumAttempts);
            }

            if (otpConfigurations.MaximumDurationMinutes < (DateTime.Now - otpRequest.CreateDate).TotalMinutes)
            {
                _logger.LogError(errorMaximumDurationMinutes + " code created: " + otpRequest.CreateDate + " date time now: " + DateTime.Now);
                throw new Exception(errorMaximumDurationMinutes);
            }

            if (otpRequest.OTPCode != validateOTPCodeRequest.OTPCode)
            {
                otpRequest.Attempts++;
                await _oTPCodeRepository.UpdateAsync(otpRequest.Id, otpRequest);
                _logger.LogError(errorDifferentCode + " input code: " + validateOTPCodeRequest.OTPCode + " expected code: " + otpRequest.OTPCode);
                throw new Exception(errorDifferentCode);
            }

        }
    }
}
