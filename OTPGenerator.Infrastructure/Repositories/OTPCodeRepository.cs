using Microsoft.EntityFrameworkCore;
using OTPGenerator.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTPGenerator.Infrastructure.Repositories
{
    public class OTPCodeRepository
    {
        protected readonly OTPGeneratorDbContext _OTPGeneratorDbContext;

        public OTPCodeRepository(OTPGeneratorDbContext OTPGeneratorDbContext)
        {
            _OTPGeneratorDbContext = OTPGeneratorDbContext;

        }

        public async Task<OTPCode> AddAsync(OTPCode oTPRequest)
        {
            await _OTPGeneratorDbContext.AddAsync(oTPRequest);
            await _OTPGeneratorDbContext.SaveChangesAsync();
            return (oTPRequest);
        }

        public async Task<OTPCode> GetByIdAsync(int id)
        {
            var result = await _OTPGeneratorDbContext.OTPCode.Include(x => x.Tenant).FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<OTPCode> GetByTransactionIdAsync(string transactionId)
        {
            var result = await _OTPGeneratorDbContext.OTPCode.Include(x => x.Tenant).FirstOrDefaultAsync(x => x.TransactionId == transactionId);
            return result;
        }

        public async Task<List<OTPCode>> GetAllAsync()
        {
            var result = await _OTPGeneratorDbContext.OTPCode.Include(x => x.Tenant).ToListAsync();
            return result;
        }

        public async Task<OTPCode> UpdateAsync(int id, OTPCode oTPRequest)
        {
            var result = await GetByIdAsync(id);

            result.Attempts = oTPRequest.Attempts;
            result.Dni = oTPRequest.Dni;
            result.CreateDate = oTPRequest.CreateDate;
            result.Email = oTPRequest.Email;
            result.Tenant = oTPRequest.Tenant;
            result.Code = oTPRequest.Code;
            result.TransactionId = oTPRequest.TransactionId;

            _OTPGeneratorDbContext.OTPCode.Update(result);
            await _OTPGeneratorDbContext.SaveChangesAsync();
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            _OTPGeneratorDbContext.OTPCode.Remove(result);
            await _OTPGeneratorDbContext.SaveChangesAsync();
        }
    }
}
