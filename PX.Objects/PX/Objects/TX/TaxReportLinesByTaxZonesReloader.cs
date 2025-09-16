// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReportLinesByTaxZonesReloader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.TX;

/// <summary>
/// A tax report lines by tax zones reloader - helper which implements "Reload Tax Zones" action of <see cref="T:PX.Objects.TX.TaxReportMaint" /> graph.
/// </summary>
internal class TaxReportLinesByTaxZonesReloader
{
  public const int DefaultMaxCountOfDisplayedLineNumbers = 10;
  private readonly TaxReportMaint taxReportGraph;
  private readonly int maxCountOfDisplayedLineNumbers;
  private string messageDisplayed;

  /// <summary>
  /// Initializes a new instance of the <see cref="T:PX.Objects.TX.TaxReportLinesByTaxZonesReloader" /> class.
  /// </summary>
  /// <param name="aTaxReportGraph">The tax report graph.</param>
  /// <param name="aMaxCountOfDisplayedLineNumbers">(Optional) The maximum count of displayed line numbers.
  /// Default is <see cref="F:PX.Objects.TX.TaxReportLinesByTaxZonesReloader.DefaultMaxCountOfDisplayedLineNumbers" /></param>
  public TaxReportLinesByTaxZonesReloader(
    TaxReportMaint aTaxReportGraph,
    int? aMaxCountOfDisplayedLineNumbers = null)
  {
    if (aTaxReportGraph == null)
      throw new ArgumentNullException(nameof (aTaxReportGraph));
    int? nullable = aMaxCountOfDisplayedLineNumbers;
    int num = 0;
    if (nullable.GetValueOrDefault() <= num & nullable.HasValue)
      throw new ArgumentOutOfRangeException(nameof (aMaxCountOfDisplayedLineNumbers), "The value must be greater than zero");
    this.taxReportGraph = aTaxReportGraph;
    this.maxCountOfDisplayedLineNumbers = aMaxCountOfDisplayedLineNumbers ?? 10;
  }

  public void ReloadTaxReportLinesForTaxZones()
  {
    this.messageDisplayed = this.messageDisplayed ?? this.ReloadTaxReportLinesForTaxZonesInternalAndPrepareChangesDescription();
    if (this.messageDisplayed == null || !WebDialogResultExtension.IsPositive(((PXSelectBase<TaxReport>) this.taxReportGraph.Report).Ask("Reload Tax Zones", this.messageDisplayed, (MessageButtons) 0, false)))
      return;
    this.messageDisplayed = (string) null;
  }

  private string ReloadTaxReportLinesForTaxZonesInternalAndPrepareChangesDescription()
  {
    TaxReport current = ((PXSelectBase<TaxReport>) this.taxReportGraph?.Report).Current;
    if (current == null)
      return (string) null;
    Dictionary<int, TaxReportLine> dictionary1 = GraphHelper.RowCast<TaxReportLine>((IEnumerable) PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReport.revisionID>>, And<TaxReportLine.tempLineNbr, IsNull, And<TaxReportLine.tempLine, Equal<True>>>>>>.Config>.Select((PXGraph) this.taxReportGraph, new object[2]
    {
      (object) current.VendorID,
      (object) current.RevisionID
    })).ToDictionary<TaxReportLine, int>((Func<TaxReportLine, int>) (line => line.LineNbr.Value));
    if (dictionary1.Count == 0)
      return PXLocalizer.Localize("Tax zones cannot be updated. The report version does not have report lines with the Detail by Tax Zones check box selected.", typeof (Messages).FullName);
    Dictionary<string, TaxZone> dictionary2 = GraphHelper.RowCast<TaxZone>((IEnumerable) PXSelectBase<TaxZone, PXSelect<TaxZone>.Config>.Select((PXGraph) this.taxReportGraph, Array.Empty<object>())).ToDictionary<TaxZone, string>((Func<TaxZone, string>) (zone => zone.TaxZoneID));
    ILookup<int, TaxReportLine> lookup = GraphHelper.RowCast<TaxReportLine>((IEnumerable) PXSelectBase<TaxReportLine, PXSelect<TaxReportLine, Where<TaxReportLine.vendorID, Equal<Required<TaxReport.vendorID>>, And<TaxReportLine.taxReportRevisionID, Equal<Required<TaxReport.revisionID>>, And<TaxReportLine.tempLineNbr, IsNotNull>>>>.Config>.Select((PXGraph) this.taxReportGraph, new object[2]
    {
      (object) current.VendorID,
      (object) current.RevisionID
    })).ToLookup<TaxReportLine, int>((Func<TaxReportLine, int>) (line => line.TempLineNbr.Value));
    List<TaxReportLine> deletedLines = this.DeleteChildTaxLinesForOldZones(dictionary2, lookup);
    return this.GetTextFromReloadTaxZonesResult(this.GenerateChildTaxLinesForMissingZones(dictionary1, dictionary2, lookup), deletedLines);
  }

  /// <summary>Generates a child tax lines for missing zones.</summary>
  /// <param name="templateLinesByLineNumber">The template lines by line number.</param>
  /// <param name="zonesInSystem">The zones in system.</param>
  /// <param name="detailLinesByTemplateLineNumber">The detail lines by template line number.</param>
  /// <returns>The generated child tax lines for missing zones.</returns>
  private List<TaxReportLine> GenerateChildTaxLinesForMissingZones(
    Dictionary<int, TaxReportLine> templateLinesByLineNumber,
    Dictionary<string, TaxZone> zonesInSystem,
    ILookup<int, TaxReportLine> detailLinesByTemplateLineNumber)
  {
    Dictionary<int, List<string>> dictionary = templateLinesByLineNumber.ToDictionary<KeyValuePair<int, TaxReportLine>, int, List<string>>((Func<KeyValuePair<int, TaxReportLine>, int>) (templatePair => templatePair.Key), (Func<KeyValuePair<int, TaxReportLine>, List<string>>) (templatePair => zonesInSystem.Select<KeyValuePair<string, TaxZone>, string>((Func<KeyValuePair<string, TaxZone>, string>) (zone => zone.Value.TaxZoneID)).Except<string>(detailLinesByTemplateLineNumber[templatePair.Key].Select<TaxReportLine, string>((Func<TaxReportLine, string>) (line => line.TaxZoneID))).ToList<string>()));
    List<TaxReportLine> linesForMissingZones = new List<TaxReportLine>(8);
    foreach (KeyValuePair<int, List<string>> keyValuePair in dictionary)
    {
      TaxReportLine template = templateLinesByLineNumber[keyValuePair.Key];
      foreach (string key in keyValuePair.Value)
      {
        TaxReportLine taxReportLine = ((PXSelectBase) this.taxReportGraph.ReportLine).Cache.Insert((object) this.taxReportGraph.CreateChildLine(template, zonesInSystem[key])) as TaxReportLine;
        linesForMissingZones.Add(taxReportLine);
      }
    }
    return linesForMissingZones;
  }

  /// <summary>Deletes the child tax lines for old zones.</summary>
  /// <param name="zonesInSystem">The zones in system.</param>
  /// <param name="detailLinesByTemplateLineNumber">The detail lines by template line number.</param>
  /// <returns>The deleted child lines.</returns>
  private List<TaxReportLine> DeleteChildTaxLinesForOldZones(
    Dictionary<string, TaxZone> zonesInSystem,
    ILookup<int, TaxReportLine> detailLinesByTemplateLineNumber)
  {
    return detailLinesByTemplateLineNumber.SelectMany<IGrouping<int, TaxReportLine>, TaxReportLine>((Func<IGrouping<int, TaxReportLine>, IEnumerable<TaxReportLine>>) (group => (IEnumerable<TaxReportLine>) group)).Where<TaxReportLine>((Func<TaxReportLine, bool>) (line => !zonesInSystem.ContainsKey(line.TaxZoneID))).Select<TaxReportLine, object>((Func<TaxReportLine, object>) (line => ((PXSelectBase) this.taxReportGraph.ReportLine).Cache.Delete((object) line))).OfType<TaxReportLine>().ToList<TaxReportLine>();
  }

  private string GetTextFromReloadTaxZonesResult(
    List<TaxReportLine> addedLines,
    List<TaxReportLine> deletedLines)
  {
    if (addedLines.Count == 0 && deletedLines.Count == 0)
      return PXLocalizer.Localize("No changes have been made to the reporting lines; the reporting lines reflect current configuration.", typeof (Messages).FullName);
    string reloadTaxZonesResult = PXLocalizer.Localize("The following changes have been made when tax zones were reloaded:", typeof (Messages).FullName) + Environment.NewLine;
    if (deletedLines.Count > 0)
    {
      string str = PXMessages.LocalizeFormatNoPrefixNLA("The tax zone details for the following lines have been deleted: {0}.", new object[1]
      {
        (object) this.CreateTextToDisplayFromLineNumbers(deletedLines)
      });
      reloadTaxZonesResult = reloadTaxZonesResult + Environment.NewLine + str;
    }
    if (addedLines.Count > 0)
    {
      string str = PXMessages.LocalizeFormatNoPrefixNLA("The tax zone details for the following lines have been added: {0}.", new object[1]
      {
        (object) this.CreateTextToDisplayFromLineNumbers(addedLines)
      });
      reloadTaxZonesResult = reloadTaxZonesResult + Environment.NewLine + str;
    }
    return reloadTaxZonesResult;
  }

  private string CreateTextToDisplayFromLineNumbers(List<TaxReportLine> lines)
  {
    List<int> list = lines.Select<TaxReportLine, int>((Func<TaxReportLine, int>) (line => line.SortOrder.Value)).Distinct<int>().OrderBy<int, int>((Func<int, int>) (orderNumber => orderNumber)).ToList<int>();
    string displayFromLineNumbers = string.Join<int>(", ", list.Take<int>(this.maxCountOfDisplayedLineNumbers));
    if (list.Count > this.maxCountOfDisplayedLineNumbers)
    {
      string str = PXMessages.LocalizeFormatNoPrefixNLA("and {0} more lines", new object[1]
      {
        (object) (list.Count - this.maxCountOfDisplayedLineNumbers)
      });
      displayFromLineNumbers = $"{displayFromLineNumbers} {str}";
    }
    return displayFromLineNumbers;
  }
}
