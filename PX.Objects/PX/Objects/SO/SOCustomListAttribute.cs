// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOCustomListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public abstract class SOCustomListAttribute(Tuple<string, string>[] valuesToLabels) : 
  PXStringListAttribute(valuesToLabels)
{
  public string[] AllowedValues => this._AllowedValues;

  public string[] AllowedLabels => this._AllowedLabels;

  protected abstract Tuple<string, string>[] GetPairs();

  public virtual void CacheAttached(PXCache sender)
  {
    Tuple<string, string>[] pairs = this.GetPairs();
    this._AllowedValues = ((IEnumerable<Tuple<string, string>>) pairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>();
    this._AllowedLabels = ((IEnumerable<Tuple<string, string>>) pairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>();
    this._NeutralAllowedLabels = this._AllowedLabels;
    base.CacheAttached(sender);
  }

  protected static string MaskLocationLabel
  {
    get
    {
      return PXAccess.FeatureInstalled<FeaturesSet.accountLocations>() ? "Customer Location" : "Customer";
    }
  }
}
