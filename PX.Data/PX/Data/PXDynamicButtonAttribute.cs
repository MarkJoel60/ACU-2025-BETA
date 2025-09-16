// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDynamicButtonAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class)]
public class PXDynamicButtonAttribute : Attribute
{
  public string[] buttonNames;
  public string[] displayNames;

  public System.Type TranslationKeyType { get; set; }

  public PXDynamicButtonAttribute(string[] dynamicButtonNames, string[] dynamicButtonDisplayNames)
  {
    this.buttonNames = dynamicButtonNames;
    this.displayNames = dynamicButtonDisplayNames;
  }

  public List<PXActionInfo> DynamicActions
  {
    get
    {
      List<PXActionInfo> dynamicActions = new List<PXActionInfo>();
      if (this.buttonNames != null)
      {
        for (int buttonNameIndex = 0; buttonNameIndex < this.buttonNames.Length; ++buttonNameIndex)
        {
          string buttonName = this.buttonNames[buttonNameIndex];
          if (!string.IsNullOrEmpty(buttonName))
          {
            string actionDisplayName = this.GetActionDisplayName(buttonNameIndex);
            PXActionInfo pxActionInfo = new PXActionInfo(buttonName, actionDisplayName);
            dynamicActions.Add(pxActionInfo);
          }
        }
      }
      return dynamicActions;
    }
  }

  public virtual List<PXActionInfo> GetDynamicActions(System.Type graphType, System.Type viewType)
  {
    return this.DynamicActions;
  }

  private string GetActionDisplayName(int buttonNameIndex)
  {
    string actionDisplayName = this.buttonNames[buttonNameIndex];
    if (this.displayNames != null && buttonNameIndex <= this.displayNames.Length - 1 && !string.IsNullOrEmpty(this.displayNames[buttonNameIndex]))
      actionDisplayName = this.TranslationKeyType == (System.Type) null ? PXMessages.Localize(this.displayNames[buttonNameIndex]) : PXLocalizer.Localize(this.displayNames[buttonNameIndex], this.TranslationKeyType.FullName);
    return actionDisplayName;
  }
}
