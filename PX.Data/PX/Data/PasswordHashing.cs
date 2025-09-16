// Decompiled with JetBrains decompiler
// Type: PX.Data.PasswordHashing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Security;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

#nullable disable
namespace PX.Data;

internal static class PasswordHashing
{
  private static Lazy<ISqlDialect> _lazySqlDialect = new Lazy<ISqlDialect>((Func<ISqlDialect>) (() => PXDatabase.Provider.SqlDialect), LazyThreadSafetyMode.PublicationOnly);

  private static ISqlDialect SqlDialect => PasswordHashing._lazySqlDialect.Value;

  internal static string GetOrSetCachedHash(
    string username,
    string password,
    bool dbpassword,
    Func<string> get)
  {
    Dictionary<PasswordHashing.PasswordHashCacheKey, string> dictionary = PXContext.GetSlot<Dictionary<PasswordHashing.PasswordHashCacheKey, string>>("PasswordHashes") ?? PXContext.SetSlot<Dictionary<PasswordHashing.PasswordHashCacheKey, string>>("PasswordHashes", new Dictionary<PasswordHashing.PasswordHashCacheKey, string>());
    PasswordHashing.PasswordHashCacheKey key = new PasswordHashing.PasswordHashCacheKey(username, password, dbpassword);
    string orSetCachedHash;
    if (dictionary.TryGetValue(key, out orSetCachedHash))
      return orSetCachedHash;
    orSetCachedHash = get();
    dictionary[key] = orSetCachedHash;
    return orSetCachedHash;
  }

  internal static string HashPasswordWithSalt(string password, string strSalt, bool dbpassword = true)
  {
    byte[] numArray = Encoding.Unicode.GetBytes(strSalt);
    if (numArray.Length < 8)
    {
      byte[] destinationArray = new byte[8];
      Array.Copy((Array) numArray, (Array) destinationArray, numArray.Length);
      numArray = destinationArray;
    }
    using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, numArray, dbpassword ? 10000 : 1000))
      return PasswordHashing.SqlDialect.WildcardFieldSeparatorChar.ToString() + Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(128 /*0x80*/));
  }

  /// <summary>
  /// Checks if password is already migrated to hashed-only password type.
  /// </summary>
  internal static bool IsPasswordMigrated(string password)
  {
    return Str.IsNullOrEmpty(password) || password.StartsWith(PasswordHashing.SqlDialect.WildcardFieldSeparator, StringComparison.InvariantCultureIgnoreCase);
  }

  private readonly struct PasswordHashCacheKey(string userName, string password, bool isDbPassword) : 
    IEquatable<PasswordHashing.PasswordHashCacheKey>
  {
    public string UserName { get; } = userName;

    public string Password { get; } = password;

    public bool IsDbPassword { get; } = isDbPassword;

    public override bool Equals(object obj)
    {
      return obj is PasswordHashing.PasswordHashCacheKey other && this.Equals(other);
    }

    public bool Equals(PasswordHashing.PasswordHashCacheKey other)
    {
      return this.IsDbPassword == other.IsDbPassword && ConstantTimeStringComparer.AreEqual(this.UserName, other.UserName) && ConstantTimeStringComparer.AreEqual(this.Password, other.Password);
    }

    public override int GetHashCode()
    {
      return 23 * (23 * (23 * 17 + ConstantTimeStringComparer.GetConstantTimeHashCode(this.UserName, 256 /*0x0100*/)) + ConstantTimeStringComparer.GetConstantTimeHashCode(this.Password, 512 /*0x0200*/)) + this.IsDbPassword.GetHashCode();
    }
  }
}
