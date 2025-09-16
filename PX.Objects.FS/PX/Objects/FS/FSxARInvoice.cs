// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxARInvoice
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

[PXVirtual]
public sealed class FSxARInvoice : PXCacheExtension<
#nullable disable
PX.Objects.AR.ARInvoice>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXBool]
  [PXDefault]
  public bool? HasFSEquipmentInfo { get; set; }

  public abstract class hasFSEquipmentInfo : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxARInvoice.hasFSEquipmentInfo>
  {
  }
}
