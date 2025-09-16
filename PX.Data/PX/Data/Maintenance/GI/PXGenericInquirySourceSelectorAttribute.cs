// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.PXGenericInquirySourceSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.Data.GenericInquiry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Maintenance.GI;

internal class PXGenericInquirySourceSelectorAttribute : PXTablesSelectorAttribute
{
  [InjectDependencyOnTypeLevel]
  private 
  #nullable disable
  IGenericInquiryReferenceInfoProvider GenericInquiryReferenceInfoProvider { get; set; }

  public PXGenericInquirySourceSelectorAttribute()
    : base(typeof (PXGenericInquirySourceSelectorAttribute.GenericInquirySource.identifier))
  {
    this.DescriptionField = typeof (PXGenericInquirySourceSelectorAttribute.GenericInquirySource.title);
  }

  public override void DescriptionFieldCommandPreparing(
    PXCache sender,
    PXCommandPreparingEventArgs e)
  {
  }

  internal override IEnumerable GetRecords()
  {
    PXGenericInquirySourceSelectorAttribute selectorAttribute = this;
    // ISSUE: reference to a compiler-generated method
    foreach (PXTablesSelectorAttribute.SingleTable singleTable in selectorAttribute.\u003C\u003En__0())
    {
      PXGenericInquirySourceSelectorAttribute.GenericInquirySource record = new PXGenericInquirySourceSelectorAttribute.GenericInquirySource();
      record.FullName = singleTable.FullName;
      record.Name = singleTable.Name;
      record.DisplayName = singleTable.DisplayName;
      record.Type = new int?(0);
      record.Identifier = singleTable.FullName;
      record.Title = singleTable.FullName;
      yield return (object) record;
    }
    Guid? currentDesignID = (Guid?) (selectorAttribute._Graph as GenericInquiryDesigner)?.CurrentDesign?.Current?.DesignID;
    PXGenericInquirySourceSelectorAttribute.GIDisplayNames definition = PXDatabase.GetSlot<PXGenericInquirySourceSelectorAttribute.GIDisplayNames>("GIDisplayNames", typeof (PX.SM.SiteMap), typeof (PX.SM.PortalMap), typeof (GIDesign), typeof (GITable));
    foreach (GIDescription giDescription in PXGenericInqGrph.Def.Where<GIDescription>((Func<GIDescription, bool>) (x => !currentDesignID.HasValue || !this.GenericInquiryReferenceInfoProvider.HasReferenceTo(x.DesignID, currentDesignID.Value))))
    {
      PXGenericInquirySourceSelectorAttribute.GenericInquirySource record = new PXGenericInquirySourceSelectorAttribute.GenericInquirySource();
      record.FullName = string.Empty;
      record.Name = giDescription.Design.Name;
      string str;
      record.DisplayName = definition.DisplayNames.TryGetValue(giDescription.Design.DesignID.Value, out str) ? str : (string) null;
      record.Type = new int?(1);
      record.Identifier = giDescription.Design.DesignID.ToInvariantString();
      record.Title = giDescription.Design.Name;
      yield return (object) record;
    }
  }

  public override void DescriptionFieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    string alias)
  {
    base.DescriptionFieldSelecting(sender, e, alias);
    if (e.ReturnValue != null || e.Row == null)
      return;
    e.ReturnValue = sender.GetValue(e.Row, this.FieldName);
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    try
    {
      base.FieldVerifying(sender, e);
    }
    catch (PXSetPropertyException ex)
    {
      sender.RaiseExceptionHandling(this.FieldName, e.Row, e.NewValue, (Exception) ex);
    }
  }

  [Serializable]
  public class GenericInquirySource : PXTablesSelectorAttribute.SingleTable
  {
    [PXString(512 /*0x0200*/, InputMask = "")]
    [PXUIField(DisplayName = "Full Name", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
    public new string FullName { get; set; }

    [PXInt]
    [PXIntList(new int[] {0, 1}, new string[] {"Table", "Generic Inquiry"})]
    [PXUIField(DisplayName = "Type", Visibility = PXUIVisibility.SelectorVisible, IsReadOnly = true)]
    public int? Type { get; set; }

    [PXString(512 /*0x0200*/, IsKey = true, InputMask = "")]
    [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, IsReadOnly = true)]
    public string Identifier { get; set; }

    [PXString(512 /*0x0200*/, InputMask = "")]
    [PXUIField(Visible = false, Visibility = PXUIVisibility.Invisible, IsReadOnly = true)]
    public string Title { get; set; }

    public new abstract class fullName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInquirySourceSelectorAttribute.GenericInquirySource.fullName>
    {
    }

    public abstract class type : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXGenericInquirySourceSelectorAttribute.GenericInquirySource.type>
    {
    }

    public abstract class identifier : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInquirySourceSelectorAttribute.GenericInquirySource.identifier>
    {
    }

    public abstract class title : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXGenericInquirySourceSelectorAttribute.GenericInquirySource.title>
    {
    }
  }

  public class GIDisplayNames : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<Guid, string> DisplayNames { get; private set; }

    public void Prefetch()
    {
      this.DisplayNames = PXGenericInqGrph.Def.ToDictionary<GIDescription, Guid, string>((Func<GIDescription, Guid>) (gi => gi.Design.DesignID.Value), (Func<GIDescription, string>) (gi => GIScreenHelper.TryGetSiteMapNode(gi.Design)?.Title));
    }
  }
}
