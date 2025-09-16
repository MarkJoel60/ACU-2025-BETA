// Decompiled with JetBrains decompiler
// Type: PX.TM.SubordinateAndWingmenOwnerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;

#nullable disable
namespace PX.TM;

/// <summary>
/// Allows to show contacts which are subordinated or wingman for current logged in employee.
/// </summary>
/// <example>[SubordinateAndWingmenOwner]</example>
public class SubordinateAndWingmenOwnerAttribute(
  bool validateValue = false,
  bool inquiryMode = true,
  System.Type[] fieldList = null,
  string[] headerList = null) : OwnerAttribute((System.Type) null, typeof (Search5<OwnerAttribute.Owner.contactID, LeftJoin<BAccount, On<BAccount.defContactID, Equal<OwnerAttribute.Owner.contactID>>>, Where<OwnerAttribute.Owner.contactID, Equal<Current<AccessInfo.contactID>>, Or<OwnerAttribute.Owner.contactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<BAccount.bAccountID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.timeEntries>>>>, Aggregate<GroupBy<OwnerAttribute.Owner.contactID>>>), validateValue, inquiryMode, fieldList, headerList)
{
}
