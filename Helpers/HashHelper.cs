using System.Security.Cryptography;
using System.Text;
using TodoApi.Models;

namespace TodoApi.Helpers
{
    public static class HashHelper
    {
        public static string ComputeHash(Todo todo, string previousHash)
        {
            var rawData =
                $"{todo.Id}|{todo.Title}|{todo.Completed}|{todo.CreatedAt:O}|{previousHash}";

            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            return Convert.ToHexString(bytes);
        }
    }
}