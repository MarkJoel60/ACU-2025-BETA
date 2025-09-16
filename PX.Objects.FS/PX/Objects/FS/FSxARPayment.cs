// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxARPayment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.FS;

public class FSxARPayment : PXCacheExtension<
#nullable disable
ARPayment>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxARPayment.ChkServiceManagement>
  {
  }
}
