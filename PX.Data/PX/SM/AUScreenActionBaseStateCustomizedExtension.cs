// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScreenActionBaseStateCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AUScreenActionBaseStateCustomizedExtension : PXCacheExtension<
#nullable disable
AUScreenActionBaseState>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisplayNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ActionFolderTypeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? MenuFolderTypeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? MenuFolderCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsTopLevelCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? BeforeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AfterCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PlacementInCategoryCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? AfterInMenuCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisableConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HideConditionCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? FormCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? MassProcessingScreenCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisablePersistCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? BatchModeCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? MapEnableRightsCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? MapViewRightsCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ExposedToMobileCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? CategoryCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsLockedOnToolbarCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IgnoresArchiveDisablingCustomized { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ConnotationCustomized { get; set; } = new bool?(false);

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.displayNameCustomized>
  {
  }

  public abstract class actionFolderTypeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.actionFolderTypeCustomized>
  {
  }

  public abstract class menuFolderTypeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.menuFolderTypeCustomized>
  {
  }

  public abstract class menuFolderCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.menuFolderCustomized>
  {
  }

  public abstract class isTopLevelCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.isTopLevelCustomized>
  {
  }

  public abstract class beforeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.beforeCustomized>
  {
  }

  public abstract class afterCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.afterCustomized>
  {
  }

  public abstract class placementInCategoryCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.placementInCategoryCustomized>
  {
  }

  public abstract class afterInMenuCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.afterInMenuCustomized>
  {
  }

  public abstract class disableConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.disableConditionCustomized>
  {
  }

  public abstract class hideConditionCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.hideConditionCustomized>
  {
  }

  public abstract class formCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.formCustomized>
  {
  }

  public abstract class massProcessingScreenCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.massProcessingScreenCustomized>
  {
  }

  public abstract class disablePersistCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.disablePersistCustomized>
  {
  }

  public abstract class batchModeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.batchModeCustomized>
  {
  }

  public abstract class mapEnableRightsCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.mapEnableRightsCustomized>
  {
  }

  public abstract class mapViewRightsCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.mapViewRightsCustomized>
  {
  }

  public abstract class exposedToMobileCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.exposedToMobileCustomized>
  {
  }

  public abstract class categoryCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.categoryCustomized>
  {
  }

  public abstract class isLockedOnToolbarCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.isLockedOnToolbarCustomized>
  {
  }

  public abstract class ignoresArchiveDisablingCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.ignoresArchiveDisablingCustomized>
  {
  }

  public abstract class connotationCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AUScreenActionBaseStateCustomizedExtension.connotationCustomized>
  {
  }
}
