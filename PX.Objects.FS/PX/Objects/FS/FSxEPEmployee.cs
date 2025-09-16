// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxEPEmployee
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

public class FSxEPEmployee : PXCacheExtension<
#nullable disable
EPEmployee>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Staff Member in Service Management")]
  public virtual bool? SDEnabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Driver", Enabled = false, FieldClass = "ROUTEMANAGEMENT")]
  public virtual bool? IsDriver { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  [PXBool]
  [PXDefault]
  [PXUIField(DisplayName = "Already Assigned", Enabled = false, FieldClass = "ROUTEMANAGEMENT")]
  public virtual bool? Mem_UnassignedDriver { get; set; }

  public abstract class sDEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxEPEmployee.sDEnabled>
  {
  }

  public abstract class isDriver : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxEPEmployee.isDriver>
  {
  }

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEPEmployee.ChkServiceManagement>
  {
  }

  public abstract class mem_UnassignedDriver : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxEPEmployee.mem_UnassignedDriver>
  {
  }
}
