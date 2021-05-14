using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public interface IModuleSerializer
    {
        byte[] Serialize<T>(T value);
        T Deserialize<T>(byte[] values);
        object Deserialize(byte[] values, Type type);
    }

    internal sealed class JsonModuleSerializer : IModuleSerializer 
    {
        private static readonly JsonSerializerOptions serializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public T Deserialize<T>(byte[] values)
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(values), serializerOptions);
        }

        public object Deserialize(byte[] values, Type type)
        {
            return JsonSerializer.Deserialize(Encoding.UTF8.GetString(values), type, serializerOptions);
        }

        public byte[] Serialize<T>(T value)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value, serializerOptions));
        }
    }
}
