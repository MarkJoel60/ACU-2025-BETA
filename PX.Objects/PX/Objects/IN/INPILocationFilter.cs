// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPILocationFilter
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
public class INPILocationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _StartLocationID;
  protected int? _EndLocationID;

  [Location(typeof (INPIClass.siteID), DisplayName = "Start Location ID")]
  public virtual int? StartLocationID
  {
    get => this._StartLocationID;
    set => this._StartLocationID = value;
  }

  [Location(typeof (INPIClass.siteID), DisplayName = "End Location ID")]
  public virtual int? EndLocationID
  {
    get => this._EndLocationID;
    set => this._EndLocationID = value;
  }

  public abstract class startLocationID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INPILocationFilter.startLocationID>
  {
  }

  public abstract class endLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPILocationFilter.endLocationID>
  {
  }
}
