// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCarrierRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class SOCarrierRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineNbr;
  protected DateTime? _DeliveryDate;
  protected int? _DaysInTransit;
  protected Decimal? _Amount;
  protected 
  #nullable disable
  string _Method;
  protected string _Description;
  protected bool? _CanBeSelected;
  protected bool? _Selected = new bool?(false);

  [PXInt(IsKey = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDate]
  [PXUIField(DisplayName = "Delivery Date", Enabled = false)]
  public virtual DateTime? DeliveryDate
  {
    get => this._DeliveryDate;
    set => this._DeliveryDate = value;
  }

  [PXInt]
  [PXUIField(DisplayName = "Days in Transit", Enabled = false)]
  public virtual int? DaysInTransit
  {
    get => this._DaysInTransit;
    set => this._DaysInTransit = value;
  }

  [PXPriceCost]
  [PXUIField]
  public virtual Decimal? Amount
  {
    get => this._Amount;
    set => this._Amount = value;
  }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Method
  {
    get => this._Method;
    set => this._Method = value;
  }

  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? CanBeSelected
  {
    get => this._CanBeSelected;
    set => this._CanBeSelected = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOCarrierRate.lineNbr>
  {
  }

  public abstract class deliveryDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOCarrierRate.deliveryDate>
  {
  }

  public abstract class daysInTransit : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOCarrierRate.daysInTransit>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOCarrierRate.amount>
  {
  }

  public abstract class method : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOCarrierRate.method>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOCarrierRate.description>
  {
  }

  public abstract class canBeSelected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOCarrierRate.canBeSelected>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOCarrierRate.selected>
  {
  }
}
