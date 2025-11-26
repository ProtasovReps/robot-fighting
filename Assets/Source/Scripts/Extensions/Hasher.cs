using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Extensions
{
    public class Hasher<T> : IDisposable
    {
        private readonly SHA256 _encoder;
        private readonly Dictionary<T, string> _hashedData;
        
        public Hasher()
        {
            _encoder = SHA256.Create();
            _hashedData = new Dictionary<T, string>();
        }
        
        public void Dispose()
        {
            _encoder?.Dispose();
        }
        
        public string GetHash(T hashObject, string data)
        {
            if (_hashedData.ContainsKey(hashObject))
                return _hashedData[hashObject];
            
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hash = _encoder.ComputeHash(bytes);
            string formatedHash = Convert.ToBase64String(hash);
            
            _hashedData.Add(hashObject, formatedHash);
            return formatedHash;
        }
    }
}