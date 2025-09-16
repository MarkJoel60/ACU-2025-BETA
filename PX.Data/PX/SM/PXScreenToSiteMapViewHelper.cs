// Decompiled with JetBrains decompiler
// Type: PX.SM.PXScreenToSiteMapViewHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Reflection;

#nullable disable
namespace PX.SM;

internal class PXScreenToSiteMapViewHelper : PXScreenToSiteMapBaseHelper
{
  public PXScreenToSiteMapViewHelper(
    string screenIDPrefix,
    PXCache entityCache,
    PXAction[] navigationActions,
    System.Type[] navigationFields)
    : base(entityCache)
  {
    int num = 0;
    if (navigationActions != null)
      num += navigationActions.Length;
    if (navigationFields != null)
    {
      foreach (System.Type navigationField in navigationFields)
      {
        if (!navigationField.IsNested || !typeof (IBqlField).IsAssignableFrom(navigationField))
          throw new PXArgumentException(nameof (navigationFields), "The property {0} is not found or it is not a BQL field in a DAC.", new object[1]
          {
            (object) navigationField.Name
          });
      }
      num += navigationFields.Length;
    }
    if (num == 0)
      throw new PXException("The navigation actions and fields count is 0. You must specify at least one navigation action or field.");
    if (screenIDPrefix.Length != 2)
      throw new PXArgumentException(nameof (screenIDPrefix), "The length of a screen ID prefix must be two symbols.");
    bool isEnabled = !PXSiteMap.CurrentNode.ScreenID.StartsWith(screenIDPrefix, StringComparison.InvariantCultureIgnoreCase);
    this.EnableNavigationControls(navigationActions, navigationFields, isEnabled);
  }

  private void EnableNavigationControls(
    PXAction[] navigationActions,
    System.Type[] customNavigationFields,
    bool isEnabled)
  {
    if (navigationActions != null)
    {
      foreach (PXAction navigationAction in navigationActions)
        navigationAction.SetEnabled(isEnabled);
    }
    if (customNavigationFields == null)
      return;
    foreach (MemberInfo customNavigationField in customNavigationFields)
      PXUIFieldAttribute.SetEnabled(this.EntityCache, customNavigationField.Name, isEnabled);
  }
}
