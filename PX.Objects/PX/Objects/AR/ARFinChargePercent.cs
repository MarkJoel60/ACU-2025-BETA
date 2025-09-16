// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARFinChargePercent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Financial Charge Percent")]
[Serializable]
public class ARFinChargePercent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const 
  #nullable disable
  string DefaultStartDate = "01/01/1900";
  protected string _FinChargeID;
  protected Decimal? _FinChargePercent;
  protected DateTime? _BeginDate;
  protected int? _PercentID;

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDBDefault(typeof (ARFinCharge.finChargeID))]
  [PXParent(typeof (Select<ARFinCharge, Where<ARFinCharge.finChargeID, Equal<Current<ARFinChargePercent.finChargeID>>>>))]
  public virtual string FinChargeID
  {
    get => this._FinChargeID;
    set => this._FinChargeID = value;
  }

  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Percent Rate")]
  public virtual Decimal? FinChargePercent
  {
    get => this._FinChargePercent;
    set => this._FinChargePercent = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField]
  [PXCheckUnique(new Type[] {typeof (ARFinChargePercent.finChargeID)})]
  public virtual DateTime? BeginDate
  {
    get => this._BeginDate;
    set => this._BeginDate = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? PercentID
  {
    get => this._PercentID;
    set => this._PercentID = value;
  }

  public class PK : 
    PrimaryKeyOf<ARFinChargePercent>.By<ARFinChargePercent.finChargeID, ARFinChargePercent.beginDate>
  {
    public static ARFinChargePercent Find(
      PXGraph graph,
      string finChargeID,
      DateTime? beginDate,
      PKFindOptions options = 0)
    {
      return (ARFinChargePercent) PrimaryKeyOf<ARFinChargePercent>.By<ARFinChargePercent.finChargeID, ARFinChargePercent.beginDate>.FindBy(graph, (object) finChargeID, (object) beginDate, options);
    }
  }

  public abstract class finChargeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARFinChargePercent.finChargeID>
  {
  }

  public abstract class finChargePercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARFinChargePercent.finChargePercent>
  {
  }

  public abstract class beginDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARFinChargePercent.beginDate>
  {
  }

  public abstract class percentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinChargePercent.percentID>
  {
  }
}
