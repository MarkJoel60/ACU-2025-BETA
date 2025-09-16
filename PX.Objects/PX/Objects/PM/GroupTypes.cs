// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GroupTypes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class GroupTypes
{
  public const 
  #nullable disable
  string Project = "PROJECT";
  public const string Task = "TASK";
  public const string AccountGroup = "ACCGROUP";
  public const string Transaction = "PROTRAN";
  public const string Equipment = "EQUIPMENT";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("PROJECT", "Project"),
        PXStringListAttribute.Pair("TASK", "Project Task"),
        PXStringListAttribute.Pair("ACCGROUP", "Account Group"),
        PXStringListAttribute.Pair("EQUIPMENT", "Equipment")
      })
    {
    }
  }

  public class ProjectType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GroupTypes.ProjectType>
  {
    public ProjectType()
      : base("PROJECT")
    {
    }
  }

  public class TaskType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GroupTypes.TaskType>
  {
    public TaskType()
      : base("TASK")
    {
    }
  }

  public class AccountGroupType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GroupTypes.AccountGroupType>
  {
    public AccountGroupType()
      : base("ACCGROUP")
    {
    }
  }

  public class TransactionType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GroupTypes.TransactionType>
  {
    public TransactionType()
      : base("PROTRAN")
    {
    }
  }

  public class EquipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  GroupTypes.EquipmentType>
  {
    public EquipmentType()
      : base("EQUIPMENT")
    {
    }
  }
}
