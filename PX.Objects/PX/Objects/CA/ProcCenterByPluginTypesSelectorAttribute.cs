// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ProcCenterByPluginTypesSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class ProcCenterByPluginTypesSelectorAttribute : PXCustomSelectorAttribute
{
  private Type search;
  private string[] pluginTypeNames;

  public ProcCenterByPluginTypesSelectorAttribute(
    Type search,
    Type selectType,
    string[] pluginTypeNames)
    : base(selectType)
  {
    this.search = search;
    this.pluginTypeNames = pluginTypeNames;
  }

  public IEnumerable GetRecords()
  {
    ProcCenterByPluginTypesSelectorAttribute selectorAttribute = this;
    BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
    {
      selectorAttribute.search
    });
    foreach (object record in new PXView(selectorAttribute._Graph, false, instance).SelectMulti(Array.Empty<object>()))
    {
      CCProcessingCenter procCenter = PXResult.Unwrap<CCProcessingCenter>(record);
      if (selectorAttribute.CheckPluginType(procCenter))
        yield return record;
    }
  }

  private bool CheckPluginType(CCProcessingCenter procCenter)
  {
    string processingTypeName = procCenter.ProcessingTypeName;
    if (((IEnumerable<string>) this.pluginTypeNames).Contains<string>(processingTypeName))
      return true;
    try
    {
      Type pluginType = CCPluginTypeHelper.GetPluginType(processingTypeName);
      foreach (string pluginTypeName in this.pluginTypeNames)
      {
        if ((CCPluginTypeHelper.CheckParentClass(pluginType, pluginTypeName, 0, 3) ? 1 : (CCPluginTypeHelper.CheckImplementInterface(pluginType, pluginTypeName) ? 1 : 0)) != 0)
          return true;
      }
    }
    catch
    {
    }
    return false;
  }
}
