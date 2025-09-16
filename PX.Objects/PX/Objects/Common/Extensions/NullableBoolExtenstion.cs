// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Extensions.NullableBoolExtenstion
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.Common.Extensions;

public static class NullableBoolExtenstion
{
  /// <summary>
  /// Returns <c>true</c> if the value is not null and is equal true. Owervise returns <c>false</c>.
  /// </summary>
  public static bool IsTrue(this bool? value) => value.GetValueOrDefault();

  /// <summary>
  /// Returns <c>true</c> if the value is not null and is equal false. Owervise returns <c>false</c>.
  /// </summary>
  public static bool IsFalse(this bool? value)
  {
    bool? nullable = value;
    bool flag = false;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }
}
