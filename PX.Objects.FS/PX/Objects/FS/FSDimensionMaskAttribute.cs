// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSDimensionMaskAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public class FSDimensionMaskAttribute(
  string dimension,
  string mask,
  string defaultValue,
  string[] allowedValues,
  string[] allowedLabels) : PXDimensionMaskAttribute(dimension, mask, defaultValue, allowedValues, allowedLabels)
{
  public virtual bool IsStringListValueDisabled(string item)
  {
    return PXAccess.FeatureInstalled<FeaturesSet.warehouse>() || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>() || !(item == "W");
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    IEnumerable<Tuple<string, string>> source = ((IEnumerable<string>) this._allowedValues).Zip<string, string, Tuple<string, string>>((IEnumerable<string>) this._allowedLabels, (Func<string, string, Tuple<string, string>>) ((v, l) => new Tuple<string, string>(v, l))).Where<Tuple<string, string>>((Func<Tuple<string, string>, bool>) (t => this.IsStringListValueDisabled(t.Item1)));
    this._allowedValues = source.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (p => p.Item1)).ToArray<string>();
    this._allowedLabels = source.Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (p => p.Item2)).ToArray<string>();
  }
}
