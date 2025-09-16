// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxEPPosition
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.EP;

#nullable enable
namespace PX.Objects.FS;

public class FSxEPPosition : PXCacheExtension<
#nullable disable
EPPosition>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Staff for Service Management")]
  public virtual bool? SDEnabled { get; set; }

  [PXBool]
  [PXDefault(false)]
  public virtual bool SDEnabledModified { get; set; }

  public abstract class sDEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxEPPosition.sDEnabled>
  {
  }

  public abstract class sDEnabledModified : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEPPosition.sDEnabledModified>
  {
  }
}
