// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.TrialBalanceImportStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// List Attrubute with the following values : New, Valid, Duplicate, Error.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class TrialBalanceImportStatusAttribute : PXIntListAttribute
{
  public const int NEW = 0;
  public const int VALID = 1;
  public const int DUPLICATE = 2;
  public const int ERROR = 3;

  public TrialBalanceImportStatusAttribute()
    : base(new int[4]{ 0, 1, 2, 3 }, new string[4]
    {
      "New",
      "Validated",
      "Duplicate",
      "Error"
    })
  {
  }
}
