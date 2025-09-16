// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PXCCPluginTypeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Objects.AR.CCPaymentProcessing.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CA;

public class PXCCPluginTypeSelectorAttribute : PXProviderTypeSelectorAttribute
{
  private static Type[] _interfaces = new Type[1]
  {
    typeof (ICCProcessingPlugin)
  };

  public PXCCPluginTypeSelectorAttribute()
    : base(PXCCPluginTypeSelectorAttribute._interfaces)
  {
  }

  public static IEnumerable<PXProviderTypeSelectorAttribute.ProviderRec> GetPluginRecs()
  {
    return PXProviderTypeSelectorAttribute.GetProviderRecs(PXCCPluginTypeSelectorAttribute._interfaces).Where<PXProviderTypeSelectorAttribute.ProviderRec>((Func<PXProviderTypeSelectorAttribute.ProviderRec, bool>) (record => !PXCCPluginTypeSelectorAttribute.SkipRecord(record)));
  }

  protected override IEnumerable GetRecords()
  {
    return (IEnumerable) PXCCPluginTypeSelectorAttribute.GetPluginRecs();
  }

  private static bool SkipRecord(PXProviderTypeSelectorAttribute.ProviderRec record)
  {
    return CCPluginTypeHelper.IsProcCenterFeatureDisabled(record.TypeName);
  }
}
