// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RouteDocumentFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class RouteDocumentFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Route ID")]
  [FSSelectorRouteID]
  public virtual int? RouteID { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Open")]
  public virtual bool? StatusOpen { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "In Process")]
  public virtual bool? StatusInProcess { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Canceled")]
  public virtual bool? StatusCanceled { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? StatusCompleted { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Closed")]
  public virtual bool? StatusClosed { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? FromDate { get; set; }

  [PXDate]
  [PXUIField]
  public virtual DateTime? ToDate { get; set; }

  public abstract class routeID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  RouteDocumentFilter.routeID>
  {
  }

  public abstract class statusOpen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RouteDocumentFilter.statusOpen>
  {
  }

  public abstract class statusInProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteDocumentFilter.statusInProcess>
  {
  }

  public abstract class statusCanceled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteDocumentFilter.statusCanceled>
  {
  }

  public abstract class statusCompleted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteDocumentFilter.statusCompleted>
  {
  }

  public abstract class statusClosed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RouteDocumentFilter.statusClosed>
  {
  }

  public abstract class fromDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RouteDocumentFilter.fromDate>
  {
  }

  public abstract class toDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RouteDocumentFilter.toDate>
  {
  }
}
