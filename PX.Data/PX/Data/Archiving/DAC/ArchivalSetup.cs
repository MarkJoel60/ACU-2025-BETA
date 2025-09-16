// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.DAC.ArchivalSetup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;

#nullable enable
namespace PX.Data.Archiving.DAC;

[PXCacheName("Archival Setup")]
public class ArchivalSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(MinValue = 1, MaxValue = 24)]
  [PXDefault(6)]
  [PXUIField(DisplayName = "Archiving Process Duration (Hours)")]
  [PXUIVerify(typeof (BqlOperand<ArchivalSetup.archivingProcessDurationLimitInHours, IBqlInt>.IsLessEqual<ArchivalSetup.archivingProcessDurationLimitInHours.maxValue>), PXErrorLevel.Error, "The duration of the archiving process temporarily cannot exceed 8 hours. This value will be increased in future versions.", new System.Type[] {})]
  public virtual int? ArchivingProcessDurationLimitInHours { get; set; }

  public abstract class archivingProcessDurationLimitInHours : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ArchivalSetup.archivingProcessDurationLimitInHours>
  {
    public const int MinValue = 1;
    public const int MaxValue = 8;

    public class minValue : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ArchivalSetup.archivingProcessDurationLimitInHours.minValue>
    {
      public minValue()
        : base(1)
      {
      }
    }

    public class maxValue : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Constant<
      #nullable disable
      ArchivalSetup.archivingProcessDurationLimitInHours.maxValue>
    {
      public maxValue()
        : base(8)
      {
      }
    }

    [PXLocalizable]
    public class Msg
    {
      public const string CannotExceedMax = "The duration of the archiving process temporarily cannot exceed 8 hours. This value will be increased in future versions.";
    }
  }
}
