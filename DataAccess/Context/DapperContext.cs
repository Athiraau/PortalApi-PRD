using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Context
{
	public class DapperContext : DbContext
	{
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _password;
        private readonly string _securityKey;

        public DapperContext(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _password = _configuration.GetConnectionString("Password");
            _securityKey = _configuration.GetConnectionString("Key");
            if (environment.IsProduction())
            {
                _password = Decrypt(_password);
            }
            _connectionString = _configuration.GetConnectionString("OracleConnection") + _password;
        }

        public IDbConnection CreateConnection()
            => new OracleConnection(_connectionString);

        public string Decrypt(string EncryptedText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(EncryptedText);
            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);        
            objTripleDESCryptoService.Clear();

            //Convert and return the decrypted data/byte into string format.
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
