// Decompiled with JetBrains decompiler
// Type: PX.Api.SYExportMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.DependencyInjection;
using PX.Data.Maintenance.SM.SiteMapHelpers;
using System;

#nullable disable
namespace PX.Api;

public class SYExportMaint : 
  SYMappingMaint<SYMapping.mappingType.typeExport>,
  IGraphWithInitialization
{
  public const string ExportByScenarioScreenUrl = "~/Pages/SM/SM207036.aspx";
  public const string ExportByScenarioScreenId = "SM207036";

  protected override string ScreenId => "SM207025";

  public SYExportMaint()
  {
    PXSiteMapNodeSelectorAttribute.SetRestriction<SYMapping.screenID>(this.Mappings.Cache, (object) null, (Func<PX.SM.SiteMap, bool>) (s => false));
    PXUIFieldAttribute.SetDisplayName<SYMappingField.objectName>(this.FieldMappings.Cache, "Source Object");
    PXUIFieldAttribute.SetDisplayName<SYMappingField.value>(this.FieldMappings.Cache, "Target Field or Value");
    PXUIFieldAttribute.SetDisplayName<SYMappingCondition.objectName>(this.MatchingConditions.Cache, "Source Object");
  }

  public void Initialize()
  {
    this.ScreenToSiteMapAddHelper = (PXFieldScreenToSiteMapAddHelper<SYMapping>) new PXScenarioScreenToSiteMapAddHelper((PXGraph) this, this.ScreenInfoCacheControl, "EB", "~/Pages/SM/SM207036.aspx");
  }

  protected override bool CallbacksAutoCommit => false;

  protected override bool AllowLineNumberNew => false;

  [PXMergeAttributes]
  [PXFormulaEditor(DisplayName = "Field or Action Name", IsDBField = false)]
  [PXFormulaEditor_AddInternalFields]
  [PXFormulaEditor_AddExternalFields]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor_AddSubstitutions]
  [PXFormulaEditor_AddMultiselectSubstitute]
  protected virtual void _(Events.CacheAttached<SYMappingField.fieldName> e)
  {
  }
}
