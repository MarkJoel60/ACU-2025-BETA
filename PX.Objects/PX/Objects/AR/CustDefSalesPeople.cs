// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustDefSalesPeople
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXProjection(typeof (Select5<CustSalesPeople, InnerJoin<CustDefSalesPeople.BAccount, On<CustDefSalesPeople.BAccount.bAccountID, Equal<CustSalesPeople.bAccountID>>, InnerJoin<CustDefSalesPeople.Location, On<CustDefSalesPeople.Location.bAccountID, Equal<CustSalesPeople.bAccountID>>, LeftJoin<CustDefSalesPeople.CustSalesPeople2, On<CustDefSalesPeople.CustSalesPeople2.bAccountID, Equal<CustDefSalesPeople.Location.bAccountID>, And<CustDefSalesPeople.CustSalesPeople2.locationID, Equal<CustDefSalesPeople.Location.locationID>, And<CustDefSalesPeople.CustSalesPeople2.isDefault, Equal<True>>>>>>>, Where<CustSalesPeople.locationID, Equal<CustDefSalesPeople.BAccount.defLocationID>, And<CustDefSalesPeople.CustSalesPeople2.salesPersonID, IsNull, Or<CustDefSalesPeople.CustSalesPeople2.salesPersonID, Equal<CustSalesPeople.salesPersonID>>>>, Aggregate<GroupBy<CustSalesPeople.salesPersonID, GroupBy<CustSalesPeople.bAccountID, GroupBy<CustSalesPeople.isDefault, GroupBy<CustDefSalesPeople.Location.locationID>>>>>>))]
[PXHidden]
[Serializable]
public class CustDefSalesPeople : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SalesPersonID;
  protected int? _BAccountID;
  protected int? _LocationID;
  protected bool? _IsDefault;

  [PXDBInt(BqlField = typeof (CustSalesPeople.salesPersonID), IsKey = true)]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDBInt(BqlField = typeof (CustSalesPeople.bAccountID), IsKey = true)]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [PXDBInt(BqlField = typeof (CustDefSalesPeople.Location.locationID), IsKey = true)]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBBool(BqlField = typeof (CustSalesPeople.isDefault))]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  public abstract class salesPersonID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  CustDefSalesPeople.salesPersonID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustDefSalesPeople.bAccountID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustDefSalesPeople.locationID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustDefSalesPeople.isDefault>
  {
  }

  [PXHidden]
  [Serializable]
  public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BAccountID;
    protected int? _DefLocationID;

    [PXDBInt(IsKey = true)]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBInt]
    public virtual int? DefLocationID
    {
      get => this._DefLocationID;
      set => this._DefLocationID = value;
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.BAccount.bAccountID>
    {
    }

    public abstract class defLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.BAccount.defLocationID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class Location : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BAccountID;
    protected int? _LocationID;

    [PXDBInt(IsKey = true)]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBInt(IsKey = true)]
    public virtual int? LocationID
    {
      get => this._LocationID;
      set => this._LocationID = value;
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.Location.bAccountID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.Location.locationID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class CustSalesPeople2 : CustSalesPeople
  {
    public new abstract class salesPersonID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.CustSalesPeople2.salesPersonID>
    {
    }

    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.CustSalesPeople2.bAccountID>
    {
    }

    public new abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustDefSalesPeople.CustSalesPeople2.locationID>
    {
    }

    public new abstract class isDefault : IBqlField, IBqlOperand
    {
    }
  }
}
