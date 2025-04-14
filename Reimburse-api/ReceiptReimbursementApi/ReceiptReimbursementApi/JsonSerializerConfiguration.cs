using System.Text.Json.Serialization;
using ReceiptReimbursement.Models;
using ReceiptReimbursementApi.Models;

namespace ReceiptReimbursementApi
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Receipt))]
    [JsonSerializable(typeof(List<Receipt>))]
    [JsonSerializable(typeof(Employee))]
    [JsonSerializable(typeof(List<Employee>))]
    [JsonSerializable(typeof(ReceiptEmailSubmission))]
    [JsonSerializable(typeof(ErrorResponse))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}
