using Microsoft.EntityFrameworkCore;

namespace CourierManagementSystem.Data
{
    public class CourierService
    {
        private readonly ApplicationDbContext _appDBContext;
        private readonly ILogger<CourierService> _logger;

        public CourierService(ApplicationDbContext appDBContext, ILogger<CourierService> logger)
        {
            _appDBContext = appDBContext;
            _logger = logger;
        }

        public async Task<List<Item>> GetAllCourierAsync()
        {
            try
            {
                return await _appDBContext.Item.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all Courier items.");
                throw;
            }
        }

        public async Task<bool> InsertItemAsync(Item item)
        {
            try
            {
                await _appDBContext.Item.AddAsync(item);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting an Courier item.");
                throw;
            }
        }

        public async Task<Item> GetItemAsync(int Id)
        {
            try
            {
                Item item = await _appDBContext.Item.FirstOrDefaultAsync(c => c.Id.Equals(Id));
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting an Courier item by Id.");
                throw;
            }
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            try
            {
                _appDBContext.Item.Update(item);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating an Courier item.");
                throw;
            }
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            try
            {
                _appDBContext.Remove(item);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting an Courier item.");
                throw;
            }
        }
    }
}
