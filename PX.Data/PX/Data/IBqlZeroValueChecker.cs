// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlZeroValueChecker
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Interface which allows to check if the value is zero.</summary>
public interface IBqlZeroValueChecker
{
  /// <summary>
  /// Get zero value of the same type as the passed parameter
  /// </summary>
  object GetZeroValue(object val);

  /// <summary>Check if the passed parameter is zero</summary>
  bool IsZeroValue(object val);
}
