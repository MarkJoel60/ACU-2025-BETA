// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.ScheduleTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
public class ScheduleTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [PXUIField(DisplayName = "")]
  [PXIntList]
  public int? Schedule { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Merge Reports")]
  public bool? MergeReports { get; set; }

  [PXInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Merging Order")]
  public int? MergingOrder { get; set; }

  public abstract class schedule : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ScheduleTemplate.schedule>
  {
  }

  public abstract class mergeReports : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ScheduleTemplate.mergeReports>
  {
  }

  public abstract class mergingOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ScheduleTemplate.mergingOrder>
  {
  }
}
