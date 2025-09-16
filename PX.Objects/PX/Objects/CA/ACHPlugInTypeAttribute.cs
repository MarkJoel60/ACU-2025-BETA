// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ACHPlugInTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.ACHPlugInBase;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CA;

public class ACHPlugInTypeAttribute : PXProviderTypeSelectorAttribute
{
  public static string USACHPlugInType = "PX.ACHPlugIn.ACHPlugIn";
  private static Type[] _interfaces = new Type[1]
  {
    typeof (IACHPlugIn)
  };

  public ACHPlugInTypeAttribute()
    : base(ACHPlugInTypeAttribute._interfaces)
  {
  }

  public static IEnumerable<PXProviderTypeSelectorAttribute.ProviderRec> GetPlugIns()
  {
    return PXProviderTypeSelectorAttribute.GetProviderRecs(ACHPlugInTypeAttribute._interfaces);
  }

  protected override IEnumerable GetRecords() => (IEnumerable) ACHPlugInTypeAttribute.GetPlugIns();
}
