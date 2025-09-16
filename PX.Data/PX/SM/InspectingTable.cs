// Decompiled with JetBrains decompiler
// Type: PX.SM.InspectingTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity;
using PX.Data.ReferentialIntegrity.Inspecting;
using System;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.SM;

[PXHidden]
[PXPrimaryGraph(typeof (DatabaseSchemaInquiry))]
[Serializable]
public class InspectingTable : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "DAC Full Name")]
  public 
  #nullable disable
  string FullName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Class Name", Enabled = false)]
  public string ClassName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public string Description
  {
    get
    {
      if (string.IsNullOrEmpty(this.FullName))
        return (string) null;
      System.Type type = PXBuildManager.GetType(this.FullName, false);
      return (object) type == null ? (string) null : XmlDocsExtensions.GetXmlDocsSummary(type, true);
    }
  }

  [PXString]
  [PXUIField(DisplayName = "Namespace", Enabled = false)]
  public string NamespaceName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Short Name", Enabled = false)]
  public string ShortName { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Base Class", Enabled = false)]
  public string BaseClassName { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Has Outgoing", Enabled = false)]
  public bool? HasOutgoing { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Has Incoming", Enabled = false)]
  public bool? HasIncoming { get; set; }

  public InspectingTable()
  {
  }

  public InspectingTable(ReferencesInspectionResult inspectionResult)
  {
    this.InspectionResult = inspectionResult;
    this.ClassName = StringExtensions.LastSegment(inspectionResult.InspectingTable.FullName, '.');
    this.FullName = inspectionResult.InspectingTable.FullName.Replace("+", "..");
    this.ShortName = inspectionResult.InspectingTable.Name;
    this.NamespaceName = inspectionResult.InspectingTable.Namespace;
    this.BaseClassName = (inspectionResult.InspectingTable.BaseType != typeof (object) ? inspectionResult.InspectingTable.BaseType : inspectionResult.InspectingTable)?.FullName ?? "Unknown";
    this.HasIncoming = new bool?(inspectionResult.IncomingReferences.Any<Reference>());
    this.HasOutgoing = new bool?(inspectionResult.OutgoingReferences.Any<Reference>());
  }

  public virtual ReferencesInspectionResult InspectionResult { get; }

  public abstract class fullName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InspectingTable.fullName>
  {
  }

  public abstract class className : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InspectingTable.className>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InspectingTable.description>
  {
  }

  public abstract class namespaceName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InspectingTable.namespaceName>
  {
  }

  public abstract class shortName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InspectingTable.shortName>
  {
  }

  public abstract class baseClassName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InspectingTable.baseClassName>
  {
  }

  public abstract class hasOutgoing : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InspectingTable.hasOutgoing>
  {
  }

  public abstract class hasIncoming : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InspectingTable.hasIncoming>
  {
  }
}
