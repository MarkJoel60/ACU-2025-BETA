// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.DAC.ArchivalPolicy
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Archiving.DAC;

[PXCacheName("Archival Policy")]
public class ArchivalPolicy : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1024 /*0x0400*/, IsUnicode = true, IsKey = true, InputMask = "")]
  [ArchivableTables(TypeNameField = typeof (ArchivalPolicy.typeName))]
  [PXUIField(DisplayName = "Entity Name")]
  public virtual 
  #nullable disable
  string TableName { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string TypeName { get; set; }

  [PXDBInt(MinValue = 1, MaxValue = 1200)]
  [PXDefault(12)]
  [PXUIField(DisplayName = "Retention Period (Months)")]
  [PXUIVerify(typeof (BqlOperand<ArchivalPolicy.retentionPeriodInMonths, IBqlInt>.IsGreaterEqual<ArchivalPolicy.months12>), TriggerPoints.RowSelected, PXErrorLevel.Warning, "It is not recommended to set a retention period which is less than 12 months. The documents may still be operable within a year.", new System.Type[] {})]
  public virtual int? RetentionPeriodInMonths { get; set; }

  public int DelayInDays
  {
    get => (int) System.Math.Ceiling(30.5M * (Decimal) this.RetentionPeriodInMonths.GetValueOrDefault());
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ArchivalPolicy.tableName>
  {
  }

  public abstract class typeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ArchivalPolicy.typeName>
  {
  }

  public abstract class retentionPeriodInMonths : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchivalPolicy.retentionPeriodInMonths>
  {
  }

  public class months12 : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  ArchivalPolicy.months12>
  {
    public months12()
      : base(12)
    {
    }
  }

  [PXLocalizable]
  public class Msg
  {
    public const string TooShortRetentionPeriod = "It is not recommended to set a retention period which is less than 12 months. The documents may still be operable within a year.";
  }
}
