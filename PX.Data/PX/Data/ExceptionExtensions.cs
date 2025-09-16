// Decompiled with JetBrains decompiler
// Type: PX.Data.ExceptionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public static class ExceptionExtensions
{
  internal static string GetFullMessageNoPrefix(this PXOuterException exception, string separator)
  {
    return ExceptionExtensions.GetFullMessage(separator, exception.InnerMessages, exception.MessageNoPrefix);
  }

  internal static string GetFullMessage(this PXOuterException exception, string separator)
  {
    return ExceptionExtensions.GetFullMessage(separator, exception.InnerMessages, exception.Message);
  }

  private static string GetFullMessage(string separator, string[] innerMessages, string message)
  {
    return message + separator + string.Join(separator, innerMessages);
  }
}
