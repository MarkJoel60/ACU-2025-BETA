// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.Project.Overview.GenericInquiryAliases
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PM.Project.Overview;

public static class GenericInquiryAliases
{
  [PXHidden]
  public abstract class Document : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  [PXHidden]
  public abstract class Project : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  [PXHidden]
  public abstract class AccountGroup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
  }

  [PXHidden]
  public abstract class DFR : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXHidden]
    public abstract class ProjectId : 
      BqlType<IBqlInt, int>.Field<
      #nullable disable
      GenericInquiryAliases.DFR.ProjectId>
    {
    }
  }

  [PXHidden]
  public abstract class RFI : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXHidden]
    public abstract class ProjectId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      GenericInquiryAliases.RFI.ProjectId>
    {
    }

    [PXHidden]
    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GenericInquiryAliases.RFI.status>
    {
    }
  }

  [PXHidden]
  public abstract class ProjectIssue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXHidden]
    public abstract class ProjectId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      GenericInquiryAliases.ProjectIssue.ProjectId>
    {
    }

    [PXHidden]
    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GenericInquiryAliases.ProjectIssue.status>
    {
    }
  }

  [PXHidden]
  public abstract class Submittal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXHidden]
    public abstract class ProjectId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      GenericInquiryAliases.Submittal.ProjectId>
    {
    }
  }
}
