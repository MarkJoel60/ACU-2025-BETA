// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ServiceSelectionFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class ServiceSelectionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Service Class ID")]
  [PXSelector(typeof (Search<INItemClass.itemClassID, Where<INItemClass.itemType, Equal<INItemTypes.serviceItem>>>), SubstituteKey = typeof (INItemClass.itemClassCD))]
  public virtual int? ServiceClassID { get; set; }

  [PXDateAndTime(UseTimeZone = true)]
  [PXUIField(DisplayName = "Scheduled Date")]
  public virtual DateTime? ScheduledDateTimeBegin { get; set; }

  public abstract class serviceClassID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ServiceSelectionFilter.serviceClassID>
  {
  }

  public abstract class scheduledDateTimeBegin : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ServiceSelectionFilter.scheduledDateTimeBegin>
  {
  }
}
