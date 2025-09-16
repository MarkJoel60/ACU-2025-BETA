// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.UpdateABCAssignmentResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class UpdateABCAssignmentResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _Descr;
  protected string _OldABCCode;
  protected bool? _ABCCodeFixed = new bool?(false);
  protected string _NewABCCode;
  protected Decimal? _YtdCost;
  protected Decimal? _Ratio;
  protected Decimal? _CumulativeRatio;

  [PXGuid(IsKey = true)]
  public virtual Guid ID { get; set; } = Guid.NewGuid();

  [Inventory]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXString(1)]
  [PXUIField]
  public virtual string OldABCCode
  {
    get => this._OldABCCode;
    set => this._OldABCCode = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? ABCCodeFixed
  {
    get => this._ABCCodeFixed;
    set => this._ABCCodeFixed = value;
  }

  [PXString(1)]
  [PXUIField]
  public virtual string NewABCCode
  {
    get => this._NewABCCode;
    set => this._NewABCCode = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Criteria Value")]
  public virtual Decimal? YtdCost
  {
    get => this._YtdCost;
    set => this._YtdCost = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Ratio, %")]
  public virtual Decimal? Ratio
  {
    get => this._Ratio;
    set => this._Ratio = value;
  }

  [PXDBDecimal(4)]
  [PXUIField(DisplayName = "Cumulative Ratio, %")]
  public virtual Decimal? CumulativeRatio
  {
    get => this._CumulativeRatio;
    set => this._CumulativeRatio = value;
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UpdateABCAssignmentResult.id>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    UpdateABCAssignmentResult.inventoryID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UpdateABCAssignmentResult.descr>
  {
  }

  public abstract class oldABCCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UpdateABCAssignmentResult.oldABCCode>
  {
  }

  public abstract class aBCCodeFixed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    UpdateABCAssignmentResult.aBCCodeFixed>
  {
  }

  public abstract class newABCCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UpdateABCAssignmentResult.newABCCode>
  {
  }

  public abstract class ytdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    UpdateABCAssignmentResult.ytdCost>
  {
  }

  public abstract class ratio : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  UpdateABCAssignmentResult.ratio>
  {
  }

  public abstract class cumulativeRatio : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    UpdateABCAssignmentResult.cumulativeRatio>
  {
  }
}
