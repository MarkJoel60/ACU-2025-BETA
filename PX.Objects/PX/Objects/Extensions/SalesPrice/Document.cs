// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.SalesPrice.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.Extensions.SalesPrice;

/// <summary>A mapped cache extension that represents a document that supports multiple price lists.</summary>
public class Document : PXMappedCacheExtension
{
  /// <exclude />
  protected int? _BranchID;
  /// <exclude />
  protected int? _BAccountID;
  /// <exclude />
  protected 
  #nullable disable
  string _CuryID;
  /// <exclude />
  protected long? _CuryInfoID;
  /// <exclude />
  protected System.DateTime? _DocumentDate;

  /// <summary>The identifier of the branch associated with the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>The identifier of the business account of this document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CR.BAccount.BAccountID" /> field.
  /// </value>
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  /// <summary>The identifier of the currency in the system.</summary>
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <summary>The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">CurrencyInfo</see> object associated with the document.</summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The date of the document.</summary>
  public virtual System.DateTime? DocumentDate
  {
    get => this._DocumentDate;
    set => this._DocumentDate = value;
  }

  /// <summary>Tax Calculation Mode</summary>
  public virtual string TaxCalcMode { get; set; }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.branchID>
  {
  }

  /// <exclude />
  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.bAccountID>
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

  /// <exclude />
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
  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Document.taxCalcMode>
  {
  }
}
