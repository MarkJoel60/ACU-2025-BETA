// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerSuitableEmployee
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (Select<PX.Objects.AP.Vendor>))]
[PXHidden]
[Serializable]
public class SchedulerSuitableEmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (PX.Objects.AP.Vendor.bAccountID))]
  [PXUIField(DisplayName = "Employee ID")]
  public virtual int? BAccountID { get; set; }

  [PXBool]
  public virtual bool? IsDefault { get; set; }

  [PXBool]
  public virtual bool? IsUnsuitable { get; set; }

  [PXBool]
  public virtual bool? IsNonRecommended { get; set; }

  [PXString]
  public virtual 
  #nullable disable
  string Label { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.AP.Vendor.type))]
  public virtual string Type { get; set; }

  public abstract class bAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerSuitableEmployee.bAccountID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerSuitableEmployee.isDefault>
  {
  }

  public abstract class isUnsuitable : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerSuitableEmployee.isUnsuitable>
  {
  }

  public abstract class isNonRecommended : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulerSuitableEmployee.isNonRecommended>
  {
  }

  public abstract class label : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerSuitableEmployee.label>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerSuitableEmployee.type>
  {
  }
}
