// Decompiled with JetBrains decompiler
// Type: PX.SM.PXCheckDacFieldNameUniqueAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.SM;

/// <summary>
/// Attribute used to check that <see cref="T:PX.SM.AUScheduleFill" /> DAC <see cref="P:PX.SM.AUScheduleFill.FieldName" /> values are unique.
/// </summary>
public class PXCheckDacFieldNameUniqueAttribute : PXCheckUnique
{
  /// <summary>
  /// The error message for this attribute is always <see cref="F:PX.SM.Messages.ScheduleFilterUsedSeveralTimes" />.
  /// </summary>
  /// <remarks>
  /// This property should not be used. Setting it won't have any effect.
  /// </remarks>
  public override string ErrorMessage
  {
    get => "A record for the {0} field has already been added to this table.";
    set
    {
    }
  }

  public PXCheckDacFieldNameUniqueAttribute()
    : base()
  {
  }

  protected override string PrepareMessage(PXCache cache, object currentRow, object duplicateRow)
  {
    AUScheduleFill currentHeaderFilterValueRow = (AUScheduleFill) currentRow;
    return PXMessages.LocalizeFormatNoPrefixNLA("A record for the {0} field has already been added to this table.", (object) this.GetLocalizedValueOfFieldName(cache, currentHeaderFilterValueRow));
  }

  protected override bool CanClearError(string errorText)
  {
    return ((IEnumerable<string>) PXMessages.LocalizeFormatNoPrefix("A record for the {0} field has already been added to this table.").Split(new string[1]
    {
      "{0}"
    }, StringSplitOptions.None)).All<string>(new Func<string, bool>(errorText.Contains));
  }

  public override void RowPersisting(PXCache cache, PXRowPersistingEventArgs e)
  {
    base.RowPersisting(cache, e);
    if (e.Cancel && e.Row is AUScheduleFill row)
    {
      string valueOfFieldName = this.GetLocalizedValueOfFieldName(cache, row);
      throw new PXRowPersistingException("FieldName", (object) valueOfFieldName, "A record for the {0} field has already been added to this table.", new object[1]
      {
        (object) valueOfFieldName
      });
    }
  }

  private string GetLocalizedValueOfFieldName(
    PXCache cache,
    AUScheduleFill currentHeaderFilterValueRow)
  {
    return PXStringListAttribute.GetLocalizedLabel<AUScheduleFill.fieldName>(cache, (object) currentHeaderFilterValueRow, currentHeaderFilterValueRow.FieldName);
  }
}
