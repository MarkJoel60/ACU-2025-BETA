// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerDevicesFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXHidden]
[Serializable]
public class SchedulerDevicesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  public virtual 
  #nullable disable
  string SelectedEmployeeIDs { get; set; }

  public abstract class selectedEmployeeIDs : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SchedulerDevicesFilter.selectedEmployeeIDs>
  {
  }
}
