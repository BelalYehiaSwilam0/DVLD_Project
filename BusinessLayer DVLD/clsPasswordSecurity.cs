using System;
using System.Security.Cryptography;
using System.Text;

public static class clsPasswordSecurity
{
    // طول الـ Salt (الملح) بـ 16 بايت (128 بت)
    private const int SaltSize = 16;

    // طول الـ Hash بـ 20 بايت (160 بت)
    private const int HashSize = 20;

    // عدد التكرارات لـ PBKDF2 (مناسب للتوازن بين الأمان والاداء)
    private const int Iterations = 10000;

    // تشفير الباسورد — يعيد string مدمج (Salt + Hash) بالـ Base64
    public static string HashPassword(string password)
    {
        // توليد ملح عشوائي
        byte[] salt = new byte[SaltSize];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        // توليد الـ hash باستخدام PBKDF2
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
        byte[] hash = pbkdf2.GetBytes(HashSize);

        // دمج الملح والهاش في مصفوفة واحدة
        byte[] hashBytes = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        // تحويل البايتات إلى نص Base64
        string base64Hash = Convert.ToBase64String(hashBytes);

        return base64Hash;
    }

    // التحقق من الباسورد المدخل مقابل الهاش المخزن
    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // تحويل النص Base64 مرة أخرى لبايتات
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // استخراج الملح من أول 16 بايت
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // استخراج الهاش المخزن
        byte[] storedPasswordHash = new byte[HashSize];
        Array.Copy(hashBytes, SaltSize, storedPasswordHash, 0, HashSize);

        // توليد هاش جديد من الباسورد المدخل والملح نفسه
        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations);
        byte[] enteredPasswordHash = pbkdf2.GetBytes(HashSize);

        // مقارنة الهش الجديد مع المخزن
        for (int i = 0; i < HashSize; i++)
        {
            if (enteredPasswordHash[i] != storedPasswordHash[i])
                return false;
        }
        return true;
    }
}
