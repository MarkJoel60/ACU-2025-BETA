// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.MultiCurrency.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;

#nullable enable
namespace PX.Objects.Extensions.MultiCurrency;

/// <summary>A mapped cache extension that represents a document that supports multiple currencies.</summary>
public class Document : PXMappedCacheExtension
{
  /// <summary>The identifier of the business account of the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  public virtual int? BAccountID { get; set; }

  /// <summary>The identifier of the branch of the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  public virtual int? BranchID { get; set; }

  /// <summary>
  /// The code of the <see cref="T:PX.Objects.CM.Extensions.Currency" /> of the document.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// Corresponds to the <see cref="P:PX.Objects.CM.Currency.CuryID" /> field.
  /// </value>
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [PXDecimal]
  public virtual Decimal? CuryRate { get; set; }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [CurrencyInfo(typeof (CurrencyInfo.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The date of the document.</summary>
  public virtual System.DateTime? DocumentDate { get; set; }

  /// <summary>Required for PXCurrencyRate to be operational</summary>
  public virtual bool? CuryViewState { get; set; }

  /// <exclude />
  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.bAccountID>
  {
  }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.branchID>
  {
  }

  /// <exclude />
  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.curyID>
  {
  }

  public abstract class curyRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  Document.curyRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  Document.curyInfoID>
  {
  }

  /// <exclude />
  public abstract class documentDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Document.documentDate>
  {
  }

  /// <exclude />
  public abstract class curyViewState : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Document.curyViewState>
  {
  }
}
