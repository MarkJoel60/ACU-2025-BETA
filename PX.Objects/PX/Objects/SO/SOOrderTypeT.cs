// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderTypeT
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select<SOOrderType>))]
[PXHidden]
[Serializable]
public class SOOrderTypeT : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _Descr;
  protected string _Behavior;
  protected bool? _RequireAllocation;
  protected bool? _RequireShipping;

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa", BqlField = typeof (SOOrderType.orderType))]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<SOOrderType.orderType>))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true, BqlField = typeof (SOOrderType.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof (SOOrderType.behavior))]
  [PXUIField]
  [PXDefault]
  [SOBehavior.List]
  public virtual string Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  [PXDBBool(BqlField = typeof (SOOrderType.requireAllocation))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Stock Allocation")]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  [PXDBBool(BqlField = typeof (SOOrderType.requireShipping))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Process Shipments")]
  public virtual bool? RequireShipping
  {
    get => this._RequireShipping;
    set => this._RequireShipping = value;
  }

  public class PK : PrimaryKeyOf<SOOrderTypeT>.By<SOOrderTypeT.orderType>
  {
    public static SOOrderTypeT Find(PXGraph graph, string orderType, PKFindOptions options = 0)
    {
      return (SOOrderTypeT) PrimaryKeyOf<SOOrderTypeT>.By<SOOrderTypeT.orderType>.FindBy(graph, (object) orderType, options);
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderTypeT.orderType>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderTypeT.descr>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderTypeT.behavior>
  {
  }

  public abstract class requireAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderTypeT.requireAllocation>
  {
  }

  public abstract class requireShipping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderTypeT.requireShipping>
  {
  }
}
