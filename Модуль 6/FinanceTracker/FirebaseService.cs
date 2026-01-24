using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinanceTracker
{
    public class FirebaseService
    {
        private readonly FirebaseClient? _firebase;
        private const string BasePath = "financialRecords";

        public FirebaseService(string firebaseUrl)
        {
            if (!string.IsNullOrEmpty(firebaseUrl))
            {
                _firebase = new FirebaseClient(firebaseUrl);
            }
        }

        public async Task<string> AddRecordAsync(FinancialRecord record)
        {
            if (_firebase == null)
                throw new InvalidOperationException("Firebase client not initialized");

            try
            {
                record.Id = Guid.NewGuid().ToString();
                var recordJson = JsonConvert.SerializeObject(record);

                await _firebase
                    .Child(BasePath)
                    .Child(record.Id)
                    .PutAsync(recordJson);

                return record.Id;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при добавлении записи: {ex.Message}");
            }
        }

        public async Task UpdateRecordAsync(FinancialRecord record)
        {
            if (_firebase == null)
                throw new InvalidOperationException("Firebase client not initialized");

            try
            {
                var recordJson = JsonConvert.SerializeObject(record);

                await _firebase
                    .Child(BasePath)
                    .Child(record.Id)
                    .PutAsync(recordJson);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обновлении записи: {ex.Message}");
            }
        }

        public async Task DeleteRecordAsync(string recordId)
        {
            if (_firebase == null)
                throw new InvalidOperationException("Firebase client not initialized");

            try
            {
                await _firebase
                    .Child(BasePath)
                    .Child(recordId)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при удалении записи: {ex.Message}");
            }
        }

        public async Task<List<FinancialRecord>> GetAllRecordsAsync()
        {
            if (_firebase == null)
                throw new InvalidOperationException("Firebase client not initialized");

            try
            {
                var records = await _firebase
                    .Child(BasePath)
                    .OnceAsync<FinancialRecord>();

                return records
                    .Select(item => new FinancialRecord
                    {
                        Id = item.Key,
                        Description = item.Object.Description ?? string.Empty,
                        Amount = item.Object.Amount,
                        Type = item.Object.Type,
                        Date = item.Object.Date,
                        Category = item.Object.Category ?? string.Empty
                    })
                    .OrderByDescending(r => r.Date)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении записей: {ex.Message}");
            }
        }

        public async Task<FinancialRecord?> GetRecordByIdAsync(string recordId)
        {
            if (_firebase == null)
                throw new InvalidOperationException("Firebase client not initialized");

            try
            {
                var record = await _firebase
                    .Child(BasePath)
                    .Child(recordId)
                    .OnceSingleAsync<FinancialRecord>();

                if (record != null)
                {
                    record.Id = recordId;
                }

                return record;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении записи: {ex.Message}");
            }
        }
    }
}
