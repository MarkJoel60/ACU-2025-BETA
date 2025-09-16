// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.MatrixAttributeValueSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.DAC.Unbound;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.Matrix.Attributes;

public class MatrixAttributeValueSelectorAttribute : PXCustomSelectorAttribute
{
  protected int? _attributeNumber;
  protected bool _showDisabled;

  public MatrixAttributeValueSelectorAttribute(int attributeNumber, bool showDisabled)
    : base(typeof (CSAttributeDetail.valueID), new Type[2]
    {
      typeof (CSAttributeDetail.valueID),
      typeof (CSAttributeDetail.description)
    })
  {
    this._attributeNumber = new int?(attributeNumber);
    this._showDisabled = showDisabled;
    ((PXSelectorAttribute) this).CacheGlobal = false;
    ((PXSelectorAttribute) this).ValidateValue = true;
    ((PXSelectorAttribute) this).DescriptionField = typeof (CSAttributeDetail.description);
  }

  public MatrixAttributeValueSelectorAttribute()
    : base(typeof (CSAttributeDetail.valueID), new Type[2]
    {
      typeof (CSAttributeDetail.valueID),
      typeof (CSAttributeDetail.description)
    })
  {
    this._attributeNumber = new int?();
    ((PXSelectorAttribute) this).CacheGlobal = false;
    ((PXSelectorAttribute) this).ValidateValue = true;
    ((PXSelectorAttribute) this).DescriptionField = typeof (CSAttributeDetail.description);
  }

  protected virtual IEnumerable GetRecords()
  {
    string attributeId = this.GetAttributeID();
    int? controlType = this.GetControlType(attributeId);
    IEnumerable records;
    if (controlType.HasValue)
    {
      switch (controlType.GetValueOrDefault())
      {
        case 2:
          PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>> pxSelect = new PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>>(this._Graph);
          if (!this._showDisabled)
            ((PXSelectBase<CSAttributeDetail>) pxSelect).WhereAnd<Where<CSAttributeDetail.disabled, NotEqual<True>>>();
          records = (IEnumerable) ((PXSelectBase<CSAttributeDetail>) pxSelect).Select(new object[1]
          {
            (object) attributeId
          });
          goto label_7;
        case 4:
          records = (IEnumerable) new List<CSAttributeDetail>()
          {
            new CSAttributeDetail()
            {
              Description = "True",
              ValueID = "True"
            },
            new CSAttributeDetail()
            {
              Description = "False",
              ValueID = "False"
            }
          };
          goto label_7;
      }
    }
    records = (IEnumerable) new List<CSAttributeDetail>();
label_7:
    return records;
  }

  protected virtual int? GetControlType(string AttributeID)
  {
    int? controlType = new int?();
    if (AttributeID != null)
    {
      CSAttribute csAttribute = ((PXSelectBase<CSAttribute>) new PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>>(this._Graph)).SelectSingle(new object[1]
      {
        (object) AttributeID
      });
      if (csAttribute != null)
        controlType = csAttribute.ControlType;
    }
    return controlType;
  }

  protected virtual string GetAttributeID()
  {
    return !this._attributeNumber.HasValue ? (string) null : ((AdditionalAttributes) ((PXCache) GraphHelper.Caches<AdditionalAttributes>(this._Graph)).Current).AttributeIdentifiers[this._attributeNumber.Value];
  }

  protected virtual void EmitColumnForDescriptionField(PXCache sender)
  {
  }
}
