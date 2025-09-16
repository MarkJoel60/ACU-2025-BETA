// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ConsolidationImport.ImportSubaccountMapper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Logging;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.ConsolidationImport;

public class ImportSubaccountMapper : IImportSubaccountMapper
{
  protected readonly GLSetup GLSetup;
  protected readonly GLConsolSetup GLConsolSetup;
  private readonly Func<string, int?> _findSubIdbyCD;
  protected readonly IAppLogger Logger;
  private readonly int _subaccountCDKeyLength;
  private int _segmentStartIndex;

  public ImportSubaccountMapper(
    IReadOnlyCollection<Segment> segments,
    GLSetup glSetup,
    GLConsolSetup glConsolSetup,
    Func<string, int?> findSubIDByCD,
    IAppLogger logger)
  {
    this.GLSetup = glSetup;
    this.GLConsolSetup = glConsolSetup;
    this._findSubIdbyCD = findSubIDByCD;
    this.Logger = logger;
    this.CalcSegmentStartIndex((IEnumerable<Segment>) segments);
    this._subaccountCDKeyLength = segments.Sum<Segment>((Func<Segment, int>) (segment => (int) segment.Length.Value));
  }

  private void CalcSegmentStartIndex(IEnumerable<Segment> segments)
  {
    if (this.GLConsolSetup.PasteFlag.GetValueOrDefault())
      this._segmentStartIndex = segments.OrderBy<Segment, short?>((Func<Segment, short?>) (segment => segment.SegmentID)).TakeWhile<Segment>((Func<Segment, bool>) (segment =>
      {
        short? segmentId = segment.SegmentID;
        int? nullable = segmentId.HasValue ? new int?((int) segmentId.GetValueOrDefault()) : new int?();
        int num = (int) this.GLSetup.ConsolSegmentId.Value;
        return !(nullable.GetValueOrDefault() == num & nullable.HasValue);
      })).Sum<Segment>((Func<Segment, int>) (segment => (int) segment.Length.Value));
    else
      this._segmentStartIndex = -1;
  }

  /// <summary>
  /// Validate subaccountCD length
  /// and insert "Consolidation Segment Value" if it is need
  /// </summary>
  public PX.Objects.GL.Sub.Keys GetMappedSubaccountKeys(string subaccountCD)
  {
    if (subaccountCD == null)
      throw new ArgumentNullException(nameof (subaccountCD));
    bool valueOrDefault = this.GLConsolSetup.PasteFlag.GetValueOrDefault();
    int length = valueOrDefault ? this.GLConsolSetup.SegmentValue.Length : 0;
    int num = subaccountCD.Length + length;
    if (num != this._subaccountCDKeyLength)
      this.Logger.WriteWarning(PXMessages.LocalizeFormatNoPrefix("Failed to assemble destination Subaccount for Mapped Value {0}. Please check Subaccounts configuration in the source company.", new object[1]
      {
        (object) subaccountCD
      }));
    if (num < this._subaccountCDKeyLength)
      subaccountCD = subaccountCD.PadRight(this._subaccountCDKeyLength - length);
    if (valueOrDefault)
      subaccountCD = subaccountCD.Insert(this._segmentStartIndex, this.GLConsolSetup.SegmentValue);
    if (subaccountCD.Length > this._subaccountCDKeyLength)
      subaccountCD = subaccountCD.Substring(0, this._subaccountCDKeyLength);
    return new PX.Objects.GL.Sub.Keys()
    {
      SubCD = subaccountCD,
      SubID = this._findSubIdbyCD(subaccountCD)
    };
  }
}
