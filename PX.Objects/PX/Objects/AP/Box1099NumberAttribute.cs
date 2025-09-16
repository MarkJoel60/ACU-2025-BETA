// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Box1099NumberAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

[Obsolete("This is an absolete attribute. It will be removed in 2019R2")]
public class Box1099NumberAttribute : PXIntListAttribute
{
  protected Box1099NumberAttribute.AP1099BoxDefinition Definition;

  public Box1099NumberAttribute()
    : base(new int[1], new string[1]{ "undefined" })
  {
  }

  public override void CacheAttached(PXCache sender)
  {
    this.BuildLists();
    base.CacheAttached(sender);
  }

  private void BuildLists()
  {
    this.Definition = PXDatabase.GetSlot<Box1099NumberAttribute.AP1099BoxDefinition>(typeof (Box1099NumberAttribute.AP1099BoxDefinition).FullName, typeof (AP1099Box));
    if (!this.Definition.AP1099Boxes.Any<KeyValuePair<short, string>>())
      return;
    this._AllowedValues = this.Definition.AP1099Boxes.Select<KeyValuePair<short, string>, int>((Func<KeyValuePair<short, string>, int>) (kvp => (int) kvp.Key)).ToArray<int>();
    this._AllowedLabels = this.Definition.AP1099Boxes.Select<KeyValuePair<short, string>, string>((Func<KeyValuePair<short, string>, string>) (kvp => $"{(object) kvp.Key}-{kvp.Value}")).ToArray<string>();
    this._NeutralAllowedLabels = this._AllowedLabels;
  }

  protected class AP1099BoxDefinition : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<short, string> AP1099Boxes = new Dictionary<short, string>();

    public void Prefetch()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(typeof (AP1099Box), new PXDataField(typeof (AP1099Box.boxNbr).Name), new PXDataField(typeof (AP1099Box.descr).Name)))
        this.AP1099Boxes[pxDataRecord.GetInt16(0).Value] = pxDataRecord.GetString(1);
    }
  }
}
