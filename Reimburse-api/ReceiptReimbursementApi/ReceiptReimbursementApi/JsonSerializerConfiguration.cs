using System.Text.Json.Serialization;
using ReceiptReimbursement.Models;

namespace ReceiptReimbursementApi
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Receipt))]
    [JsonSerializable(typeof(List<Receipt>))]
    [JsonSerializable(typeof(Employee))]
    [JsonSerializable(typeof(List<Employee>))]
    [JsonSerializable(typeof(ReceiptEmailSubmission))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}
