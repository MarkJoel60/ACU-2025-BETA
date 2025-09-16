// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.RelatedTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.Data.BQL;
using System;
using System.Linq;

#nullable enable
namespace PX.Data.Maintenance.GI;

public class RelatedTable : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Parent Table")]
  [ParentTableFilter]
  public 
  #nullable disable
  string ParentTable { get; set; }

  [PXString(InputMask = "")]
  [PXUIField(DisplayName = "Child Table")]
  [ChildTableFilter]
  public string ChildTable { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Include Hidden Tables")]
  [PXDefault(false)]
  public bool? IncludeHidden { get; set; }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Alias", Enabled = false)]
  public string ParentAlias
  {
    get
    {
      return string.IsNullOrEmpty(this.ParentTable) ? (string) null : ServiceManager.Tables.First<System.Type>((Func<System.Type, bool>) (t => t.FullName == this.ParentTable)).Name;
    }
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Alias", Enabled = false)]
  public string ChildAlias
  {
    get
    {
      return string.IsNullOrEmpty(this.ChildTable) ? (string) null : ServiceManager.Tables.First<System.Type>((Func<System.Type, bool>) (t => t.FullName == this.ChildTable)).Name;
    }
  }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Relation", Enabled = false)]
  public string Relation { get; set; }

  [PXString(256 /*0x0100*/, InputMask = "")]
  public string LinkedToFields { get; set; }

  [PXString(256 /*0x0100*/, InputMask = "")]
  public string LinkedFrom { get; set; }

  [PXBool]
  [PXDefault(false)]
  public bool? IsNew { get; set; }

  public abstract class parentTable : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedTable.parentTable>
  {
  }

  public abstract class childTable : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedTable.childTable>
  {
  }

  public abstract class includeHidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedTable.includeHidden>
  {
  }

  public abstract class parentAlias : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedTable.parentAlias>
  {
  }

  public abstract class childAlias : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedTable.childAlias>
  {
  }

  public abstract class relation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedTable.relation>
  {
  }

  public abstract class linkedToFields : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelatedTable.linkedToFields>
  {
  }

  public abstract class linkedFrom : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelatedTable.linkedFrom>
  {
  }

  public abstract class isNew : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelatedTable.isNew>
  {
  }
}
