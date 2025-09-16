// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAROrd
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// An auxiliary DAC that is used in Accounts Payable and Accounts Receivable balance reports
/// to properly join documents with their adjustments.
/// </summary>
[PXCacheName("APAROrd")]
[Serializable]
public class APAROrd : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected short? _Ord;

  /// <summary>
  /// The field is used in reports for joining and filtering purposes.
  /// </summary>
  [PXDBShort(IsKey = true)]
  public virtual short? Ord
  {
    get => this._Ord;
    set => this._Ord = value;
  }

  public abstract class ord : BqlType<IBqlShort, short>.Field<
  #nullable disable
  APAROrd.ord>
  {
  }
}
