// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.EAN128BarcodeParser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common.GS1;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.WMS;

public class EAN128BarcodeParser : ICompositeBarcodeParser
{
  private readonly Dictionary<string, string> _hriToRawBarcodes = new Dictionary<string, string>();
  private readonly Parser _parser = new Parser();

  public char GroupSeparator
  {
    get => this._parser.GroupSeparator;
    set => this._parser.GroupSeparator = value;
  }

  public bool IsCompositeBarcode(string barcode)
  {
    if (this._parser.IsRawCompositeBarcode(barcode) || this._hriToRawBarcodes.ContainsKey(barcode))
      return true;
    string str;
    if (!this._parser.TryConvertFromHRI(barcode, ref str, false))
      return false;
    this._hriToRawBarcodes[barcode] = str;
    return true;
  }

  public IReadOnlyDictionary<string, ParseResult> Parse(string compositeBarcode)
  {
    return (IReadOnlyDictionary<string, ParseResult>) (this._hriToRawBarcodes.ContainsKey(compositeBarcode) ? (IEnumerable<KeyValuePair<AI, StringData>>) this._parser.Parse(this._hriToRawBarcodes[compositeBarcode], false) : (IEnumerable<KeyValuePair<AI, StringData>>) this._parser.Parse(compositeBarcode, false)).ToDictionary<KeyValuePair<AI, StringData>, string, ParseResult>((Func<KeyValuePair<AI, StringData>, string>) (kvp => kvp.Key.Code), (Func<KeyValuePair<AI, StringData>, ParseResult>) (kvp => EAN128BarcodeParser.getResult(kvp.Value)));
  }

  private static ParseResult getResult(StringData data)
  {
    switch (data)
    {
      case DateTimeData dateTimeData:
        return ParseResult.From(dateTimeData.Value);
      case DecimalData decimalData:
        return ParseResult.From(decimalData.Value);
      default:
        return ParseResult.From(data.RawValue);
    }
  }
}
