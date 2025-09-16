// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Descriptor.Attributes.SubcontractSearchableAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Common.Extensions;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Descriptor.Attributes;

public class SubcontractSearchableAttribute : PXSearchableAttribute
{
  private const string TitlePrefix = "{0} - {2}";
  private const string FirstLineFormat = "{0}{1:d}{2}{3}{4}";
  private const string SecondLineFormat = "{0}";
  private static readonly Type[] FieldsForTheFirstLine = new Type[5]
  {
    typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.orderType),
    typeof (PX.Objects.PO.POOrder.orderDate),
    typeof (PX.Objects.PO.POOrder.status),
    typeof (PX.Objects.PO.POOrder.vendorRefNbr),
    typeof (PX.Objects.PO.POOrder.expectedDate)
  };
  private static readonly Type[] Fields = new Type[2]
  {
    typeof (PX.Objects.PO.POOrder.vendorRefNbr),
    typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.orderDesc)
  };
  private static readonly Type[] TitleFields = new Type[3]
  {
    typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.orderNbr),
    typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.vendorID),
    typeof (PX.Objects.AP.Vendor.acctName)
  };

  public SubcontractSearchableAttribute()
    : base(128 /*0x80*/, "{0} - {2}", SubcontractSearchableAttribute.TitleFields, SubcontractSearchableAttribute.Fields)
  {
    this.NumberFields = typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.orderNbr).CreateArray<Type>();
    this.Line1Format = "{0}{1:d}{2}{3}{4}";
    this.Line1Fields = SubcontractSearchableAttribute.FieldsForTheFirstLine;
    this.Line2Format = "{0}";
    this.Line2Fields = typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.orderDesc).CreateArray<Type>();
    this.MatchWithJoin = typeof (InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.vendorID>>>);
    this.SelectForFastIndexing = typeof (Select2<PX.Objects.CN.Subcontracts.SC.DAC.Subcontract, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.CN.Subcontracts.SC.DAC.Subcontract.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>);
  }

  public virtual SearchIndex BuildSearchIndex(
    PXCache cache,
    object commitment,
    PXResult result,
    string noteText)
  {
    string str = this.BuildContent(cache, commitment, result);
    return new SearchIndex()
    {
      IndexID = new Guid?(Guid.NewGuid()),
      NoteID = (Guid?) cache.GetValue(commitment, typeof (Note.noteID).Name),
      Category = new int?(this.category),
      Content = $"{str} {noteText}",
      EntityType = typeof (PX.Objects.CN.Subcontracts.SC.DAC.Subcontract).FullName
    };
  }
}
