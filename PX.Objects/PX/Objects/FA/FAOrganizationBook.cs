// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAOrganizationBook
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXProjection(typeof (Select2<FABook, LeftJoin<PX.Objects.GL.DAC.Organization, On<Where2<FeatureInstalled<FeaturesSet.multipleCalendarsSupport>, And<FABook.updateGL, Equal<True>>>>>>))]
[Serializable]
public class FAOrganizationBook : FABook
{
  /// <summary>
  /// A string identifier, which contains a key value. This field is also a selector for navigation.
  /// </summary>
  /// <value>The value can be entered only manually.</value>
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC", BqlTable = typeof (FABook))]
  [PXUIField(DisplayName = "Book ID")]
  public override 
  #nullable disable
  string BookCode { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.GL.DAC.Organization.organizationID))]
  public virtual int? RawOrganizationID { get; set; }

  [PXInt(IsKey = true)]
  [PXFormula(typeof (IsNull<FAOrganizationBook.rawOrganizationID, FinPeriod.organizationID.masterValue>))]
  public virtual int? OrganizationID { get; set; }

  [PXDBString(30, IsUnicode = true, InputMask = "", BqlTable = typeof (PX.Objects.GL.DAC.Organization))]
  [PXUIField(DisplayName = "Company ID")]
  [PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.multipleCalendarsSupport>>))]
  public virtual string OrganizationCD { get; set; }

  /// <summary>The first year of calendar for the book.</summary>
  /// <value>This is an unbound information field.</value>
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "First Calendar Year", Enabled = false)]
  [PXFormula(typeof (PX.Objects.FA.FirstCalendarYear<FAOrganizationBook.bookID, FAOrganizationBook.organizationID>))]
  public virtual string FirstCalendarYear { get; set; }

  /// <summary>The last year of calendar for the book.</summary>
  /// <value>This is an unbound information field.</value>
  [PXString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Last Calendar Year", Enabled = false)]
  [PXFormula(typeof (PX.Objects.FA.LastCalendarYear<FAOrganizationBook.bookID, FAOrganizationBook.organizationID>))]
  public virtual string LastCalendarYear { get; set; }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAOrganizationBook.selected>
  {
  }

  public new abstract class bookID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAOrganizationBook.bookID>
  {
  }

  public new abstract class updateGL : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAOrganizationBook.updateGL>
  {
  }

  public new abstract class bookCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAOrganizationBook.bookCode>
  {
  }

  public abstract class rawOrganizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FAOrganizationBook.rawOrganizationID>
  {
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FAOrganizationBook.organizationID>
  {
  }

  public abstract class organizationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAOrganizationBook.organizationCD>
  {
  }

  public abstract class firstCalendarYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAOrganizationBook.firstCalendarYear>
  {
  }

  public abstract class lastCalendarYear : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAOrganizationBook.lastCalendarYear>
  {
  }
}
