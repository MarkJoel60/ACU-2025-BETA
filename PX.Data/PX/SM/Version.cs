// Decompiled with JetBrains decompiler
// Type: PX.SM.Version
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Application Version")]
[Serializable]
public class Version : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15)]
  public virtual 
  #nullable disable
  string ComponentName { get; set; }

  [PXDBString(1)]
  public virtual string ComponentType { get; set; }

  [PXUIField(DisplayName = "Current Version", Enabled = false)]
  [PXDBString(10, DatabaseFieldName = "Version")]
  public virtual string CurrentVersion { get; set; }

  [PXUIField(DisplayName = "Last Update Date", Enabled = false)]
  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? Date { get; set; }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? Altered { get; set; }

  public abstract class componentName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Version.componentName>
  {
  }

  public abstract class componentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Version.componentType>
  {
  }

  public abstract class currentVersion : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Version.currentVersion>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Version.date>
  {
  }

  public abstract class altered : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Version.altered>
  {
  }
}
