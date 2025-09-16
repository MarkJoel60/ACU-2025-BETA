// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Attributes.DocumentKeyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Attributes;

public class DocumentKeyAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldSelectingSubscriber,
  IPXFieldUpdatingSubscriber
{
  protected Type StringListAttributeType;
  protected Dictionary<string, string> ValuesLabels;
  protected Dictionary<string, string> LabelsValues;

  public DocumentKeyAttribute(Type listAttributeType)
  {
    this.StringListAttributeType = typeof (PXStringListAttribute).IsAssignableFrom(listAttributeType) ? listAttributeType : throw new PXArgumentException(nameof (listAttributeType));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.ValuesLabels = new Dictionary<string, string>();
    this.LabelsValues = new Dictionary<string, string>();
    foreach (KeyValuePair<string, string> keyValuePair in ((PXStringListAttribute) Activator.CreateInstance(this.StringListAttributeType)).ValueLabelDic.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (pair => pair.Key), (Func<KeyValuePair<string, string>, string>) (pair => PXMessages.LocalizeNoPrefix(pair.Value)?.Trim())))
    {
      this.ValuesLabels.Add(keyValuePair.Key, keyValuePair.Value);
      string str = (string) null;
      if (!this.LabelsValues.TryGetValue(keyValuePair.Value, out str))
        this.LabelsValues.Add(keyValuePair.Value, keyValuePair.Key);
      else
        throw new PXException("The {0} label is duplicated for the following values: {1}, {2}.", new object[3]
        {
          (object) keyValuePair.Value,
          (object) keyValuePair.Key,
          (object) str
        });
    }
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue == null)
      return;
    string str1 = ((string) e.ReturnValue).Trim();
    string[] strArray = str1.Trim().Split(new char[1]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray == null || strArray.Length < 2)
      throw new ArgumentOutOfRangeException();
    string str2 = str1.Substring(strArray[0].Length).Trim();
    e.ReturnValue = (object) $"{this.ValuesLabels[strArray[0]]} {str2}";
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    string outerValue = ((string) e.NewValue).Trim();
    string key = this.LabelsValues.Keys.Where<string>((Func<string, bool>) (docType => outerValue.StartsWith(docType))).FirstOrDefault<string>();
    if (key == null)
      throw new ArgumentOutOfRangeException();
    string str = outerValue.Substring(key.Length).Trim();
    e.NewValue = (object) $"{this.LabelsValues[key]} {str}";
  }
}
