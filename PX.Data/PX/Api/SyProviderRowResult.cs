// Decompiled with JetBrains decompiler
// Type: PX.Api.SyProviderRowResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api;

public struct SyProviderRowResult
{
  public int RowIndex;
  public bool HasError;
  public string ErrorMessage;

  public string ExtRefNbr { get; set; }

  /// <summary>Construct positive row result</summary>
  /// <param name="rowIndex"></param>
  public SyProviderRowResult(int rowIndex)
    : this()
  {
    this.RowIndex = rowIndex;
    this.HasError = false;
    this.ErrorMessage = (string) null;
  }

  /// <summary>Construct negative row result with error message</summary>
  /// <param name="rowIndex"></param>
  /// <param name="errorMessage"></param>
  public SyProviderRowResult(int rowIndex, string errorMessage)
    : this()
  {
    this.RowIndex = rowIndex;
    this.HasError = true;
    this.ErrorMessage = errorMessage;
  }
}
