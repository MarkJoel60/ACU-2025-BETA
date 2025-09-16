// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.WorkgroupMemberStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.TM;

#nullable enable
namespace PX.Objects.EP;

public class WorkgroupMemberStatusAttribute : PXStringListAttribute, IPXFieldDefaultingSubscriber
{
  public const 
  #nullable disable
  string PermanentActive = "PERMA";
  public const string PermanentInactive = "PERMI";
  public const string TemporaryActive = "TEMPA";
  public const string TemporaryInactive = "TEMPI";
  public const string AdHoc = "ADHOC";

  public WorkgroupMemberStatusAttribute()
    : base(new (string, string)[5]
    {
      ("PERMA", "Permanent"),
      ("PERMI", "Permanent - Inactive"),
      ("TEMPA", "Temporary"),
      ("TEMPI", "Temporary - Inactive"),
      ("ADHOC", "Ad Hoc")
    })
  {
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    EPTimeActivitiesSummary row = (EPTimeActivitiesSummary) e.Row;
    if (row == null)
      return;
    if (row.IsMemberActive.HasValue && row.Status != null)
      e.ReturnValue = (object) WorkgroupMemberStatusAttribute.GetStatus(row.IsMemberActive, row.Status);
    if (e.ReturnValue != null)
      return;
    e.ReturnValue = (object) "ADHOC";
  }

  public void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    EPTimeActivitiesSummary row = (EPTimeActivitiesSummary) e.Row;
    if (row == null)
      return;
    EPCompanyTreeMember topFirst = PXSelectBase<EPCompanyTreeMember, PXViewOf<EPCompanyTreeMember>.BasedOn<SelectFromBase<EPCompanyTreeMember, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPCompanyTreeMember.workGroupID, Equal<P.AsInt>>>>>.And<BqlOperand<EPCompanyTreeMember.contactID, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) row.WorkgroupID,
      (object) row.ContactID
    }).TopFirst;
    e.NewValue = (object) WorkgroupMemberStatusAttribute.GetStatus((bool?) topFirst?.Active, topFirst?.MembershipType);
  }

  private static string GetStatus(bool? isActive, string membershipType)
  {
    string status = "ADHOC";
    if (membershipType == "PERMA" || isActive.GetValueOrDefault() && membershipType == "PERM")
    {
      status = "PERMA";
    }
    else
    {
      if (!(membershipType == "PERMI"))
      {
        bool? nullable = isActive;
        bool flag1 = false;
        if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue) || !(membershipType == "PERM"))
        {
          if (membershipType == "TEMPA" || isActive.GetValueOrDefault() && membershipType == "TEMP")
          {
            status = "TEMPA";
            goto label_10;
          }
          if (!(membershipType == "TEMPI"))
          {
            nullable = isActive;
            bool flag2 = false;
            if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue) || !(membershipType == "TEMP"))
              goto label_10;
          }
          status = "TEMPI";
          goto label_10;
        }
      }
      status = "PERMI";
    }
label_10:
    return status;
  }

  public class permanentActive : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WorkgroupMemberStatusAttribute.permanentActive>
  {
    public permanentActive()
      : base("PERMA")
    {
    }
  }

  public class permanentInactive : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WorkgroupMemberStatusAttribute.permanentInactive>
  {
    public permanentInactive()
      : base("PERMI")
    {
    }
  }

  public class temporaryActive : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WorkgroupMemberStatusAttribute.temporaryActive>
  {
    public temporaryActive()
      : base("TEMPA")
    {
    }
  }

  public class temporaryInactive : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WorkgroupMemberStatusAttribute.temporaryInactive>
  {
    public temporaryInactive()
      : base("TEMPI")
    {
    }
  }

  public class adHoc : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WorkgroupMemberStatusAttribute.adHoc>
  {
    public adHoc()
      : base("ADHOC")
    {
    }
  }
}
