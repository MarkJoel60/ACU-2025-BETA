// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportPM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[Serializable]
public class RMReportPM : PXCacheExtension<
#nullable disable
RMReport>
{
  protected bool? _RequestStartAccountGroup;
  protected bool? _RequestEndAccountGroup;
  protected bool? _RequestStartProject;
  protected bool? _RequestEndProject;
  protected bool? _RequestStartProjectTask;
  protected bool? _RequestEndProjectTask;
  protected bool? _RequestStartInventory;
  protected bool? _RequestEndInventory;
  protected bool? _RequestStartPeriod;
  protected short? _RequestEndPeriod;

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartAccountGroup
  {
    get => this._RequestStartAccountGroup;
    set => this._RequestStartAccountGroup = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndAccountGroup
  {
    get => this._RequestEndAccountGroup;
    set => this._RequestEndAccountGroup = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartProject
  {
    get => this._RequestStartProject;
    set => this._RequestStartProject = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndProject
  {
    get => this._RequestEndProject;
    set => this._RequestEndProject = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartProjectTask
  {
    get => this._RequestStartProjectTask;
    set => this._RequestStartProjectTask = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndProjectTask
  {
    get => this._RequestEndProjectTask;
    set => this._RequestEndProjectTask = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartInventory
  {
    get => this._RequestStartInventory;
    set => this._RequestStartInventory = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestEndInventory
  {
    get => this._RequestEndInventory;
    set => this._RequestEndInventory = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request")]
  public virtual bool? RequestStartPeriod
  {
    get => this._RequestStartPeriod;
    set => this._RequestStartPeriod = value;
  }

  [PXDBShort]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Not Set", "Request", "Use Start"})]
  [PXUIField(DisplayName = "Default")]
  public virtual short? RequestEndPeriod
  {
    get => this._RequestEndPeriod;
    set => this._RequestEndPeriod = value;
  }

  [PXDimension("SUBACCOUNT")]
  [PXSelector(typeof (PX.Objects.GL.Sub.subCD))]
  [PXString]
  public string SubCD
  {
    get => (string) null;
    set
    {
    }
  }

  public Decimal? Amount
  {
    get => new Decimal?();
    set
    {
    }
  }

  public Decimal? Drilldown
  {
    get => new Decimal?();
    set
    {
    }
  }

  public abstract class requestStartAccountGroup : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestStartAccountGroup>
  {
  }

  public abstract class requestEndAccountGroup : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestEndAccountGroup>
  {
  }

  public abstract class requestStartProject : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestStartProject>
  {
  }

  public abstract class requestEndProject : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestEndProject>
  {
  }

  public abstract class requestStartProjectTask : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestStartProjectTask>
  {
  }

  public abstract class requestEndProjectTask : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestEndProjectTask>
  {
  }

  public abstract class requestStartInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestStartInventory>
  {
  }

  public abstract class requestEndInventory : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestEndInventory>
  {
  }

  public abstract class requestStartPeriod : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RMReportPM.requestStartPeriod>
  {
  }

  public abstract class requestEndPeriod : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    RMReportPM.requestEndPeriod>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RMReportPM.subCD>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RMReportPM.amount>
  {
  }

  public abstract class drilldown : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RMReportPM.drilldown>
  {
  }
}
