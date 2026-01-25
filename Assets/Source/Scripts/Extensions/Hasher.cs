using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public class Hasher : IDisposable
    {
        private readonly SHA256 _encoder;
        private readonly Dictionary<string, int> _hashedData;
        
        public Hasher()
        {
            _encoder = SHA256.Create();
            _hashedData = new Dictionary<string, int>();
        }
        
        public void Dispose()
        {
            _encoder?.Dispose();
        }
        
        public int GetHash(string data)
        {
            if (_hashedData.TryGetValue(data, out int memorizedHash))
            {
                return memorizedHash;
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = _encoder.ComputeHash(bytes);
            int formatedHash = BinaryPrimitives.ReadInt32LittleEndian(hash);
            
            _hashedData.Add(data, formatedHash);
            return formatedHash;
        }
    }
}