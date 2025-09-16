// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.AP1099History
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

/// <summary>
/// Represents the contents of a 1099 form box for a specific financial year and 1099 vendor.
/// </summary>
[PXCacheName("AP 1099 History")]
[Serializable]
public class AP1099History : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected int? _VendorID;
  protected 
  #nullable disable
  string _FinYear;
  protected short? _BoxNbr;
  protected Decimal? _HistAmt;
  protected byte[] _tstamp;

  /// <summary>
  /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, for which the record provides 1099 information.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Branch(null, null, true, true, false, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// Identifier of the 1099 <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Vendor.BAccountID" /> field.
  /// </value>
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>Financial year, to which 1099 information belongs.</summary>
  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string FinYear
  {
    get => this._FinYear;
    set => this._FinYear = value;
  }

  /// <summary>
  /// The number (ordinal) of the box on 1099 form. (See <see cref="T:PX.Objects.AP.AP1099Box" />)
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AP.AP1099Box.BoxNbr" /> field.
  /// </value>
  [PXDBShort(IsKey = true)]
  [PXDefault]
  public virtual short? BoxNbr
  {
    get => this._BoxNbr;
    set => this._BoxNbr = value;
  }

  /// <summary>
  /// The amount (YTD) of the corresponding type of payments paid to the vendor specified in the <see cref="P:PX.Objects.AP.AP1099History.VendorID" /> field.
  /// </summary>
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
  public virtual Decimal? HistAmt
  {
    get => this._HistAmt;
    set => this._HistAmt = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<AP1099History>.By<AP1099History.branchID, AP1099History.vendorID, AP1099History.finYear, AP1099History.boxNbr>
  {
    public static AP1099History Find(
      PXGraph graph,
      int? branchID,
      int? vendorID,
      string finYear,
      short? boxNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<AP1099History>.By<AP1099History.branchID, AP1099History.vendorID, AP1099History.finYear, AP1099History.boxNbr>.FindBy(graph, (object) branchID, (object) vendorID, (object) finYear, (object) boxNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<AP1099History>.By<AP1099History.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<AP1099History>.By<AP1099History.vendorID>
    {
    }

    public class Box : 
      PrimaryKeyOf<AP1099Box>.By<AP1099Box.boxNbr>.ForeignKeyOf<AP1099History>.By<AP1099History.boxNbr>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099History.branchID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AP1099History.vendorID>
  {
  }

  public abstract class finYear : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AP1099History.finYear>
  {
  }

  public abstract class boxNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  AP1099History.boxNbr>
  {
  }

  public abstract class histAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AP1099History.histAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AP1099History.Tstamp>
  {
  }
}
