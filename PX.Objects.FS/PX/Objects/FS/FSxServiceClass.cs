// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxServiceClass
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable enable
namespace PX.Objects.FS;

public class FSxServiceClass : PXCacheExtension<
#nullable disable
INItemClass>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXUIVisible(typeof (Where<INItemClass.itemType, Equal<INItemTypes.serviceItem>>))]
  [PXUIField(DisplayName = "Default Billing Rule")]
  [PXDefault]
  public virtual string DefaultBillingRule { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Route Service Class", FieldClass = "ROUTEMANAGEMENT")]
  public virtual bool? RequireRoute { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXDefault]
  public virtual bool? chkServiceManagement => new bool?(true);

  [PXBool]
  [PXDefault]
  [PXUIField(DisplayName = "Route Service", FieldClass = "ROUTEMANAGEMENT")]
  public virtual bool? Mem_RouteService => this.RequireRoute;

  public abstract class defaultBillingRule : ListField_BillingRule
  {
  }

  public abstract class requireRoute : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxServiceClass.requireRoute>
  {
  }

  public abstract class ChkServiceManagement : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxServiceClass.ChkServiceManagement>
  {
  }

  public abstract class mem_RouteService : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxServiceClass.mem_RouteService>
  {
  }
}
