// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDateListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDateListAttribute(string[] allowedValues, string[] allowedLabels) : 
  PXStringListAttribute(allowedValues, allowedLabels)
{
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string[] allowedValues = Array.ConvertAll<string, string>(this._AllowedValues, (Converter<string, string>) (a => System.DateTime.Parse(a, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("d", (IFormatProvider) CultureInfo.InvariantCulture)));
    CultureInfo labelsCulture = object.Equals((object) sender.Graph.Culture, (object) CultureInfo.InvariantCulture) ? LocaleInfo.GetUICulture() : sender.Graph.Culture;
    string[] allowedLabels = Array.ConvertAll<string, string>(this._AllowedLabels, (Converter<string, string>) (a => System.DateTime.Parse(a, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("d", (IFormatProvider) labelsCulture)));
    e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), this._FieldName, new bool?(), new int?(-1), (string) null, allowedValues, allowedLabels, new bool?(this._ExclusiveValues), (string) null);
  }
}
